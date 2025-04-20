using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniModBus.Exceptions;
using UniModBus.Model.DataStorage;
using UniModBus.Model.Modbus.Utils;
using UniModBus.Model.Port.Interfaces;

namespace UniModBus.Model.DataExtractor
{
    /// <summary>
    /// Класс для получения данных из порта и загрузка их в CurrentModbusDataStorage в многопоточном режиме
    /// </summary>
    public class CurrentModbusDataExtractor
    {
        private const int TIMEOUT = 500;
        private const int RESPONSE_LENGTH = 7;

        private IPortReadableWritable _port;
        private CurrentModbusDataStorage _dataStorage;
        private ModBusValueConverter _converter;

        private bool _isWorking;
        private byte _deviceAddress;

        private Thread _workingThread;

        private int _delay;

        public bool IsWorking { get => _isWorking; }

        public event Action stopByErrorEvent;

        public CurrentModbusDataExtractor(IPortReadableWritable port, CurrentModbusDataStorage dataStorage, byte deviceAddress, ModBusValueConverter valueConverter, int delay)
        {
            this._port = port;
            this._dataStorage = dataStorage;
            this._deviceAddress = deviceAddress;
            this._converter = valueConverter;
            this._delay = delay;

            _workingThread = new Thread(WorkingCycle);
        }

        public void SetDeviceAddress(byte newDeviceAddress)
        {
            _deviceAddress = newDeviceAddress;
        }

        public void SetDelay(int newDelay)
        {
            if(newDelay <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _delay = newDelay;
        }

        public void Start()
        {
            _isWorking = true;
            _port.Open();

            _workingThread = new Thread(WorkingCycle);

            _workingThread.Start();
        }

        public void Stop() 
        { 
            _isWorking = false;
            _workingThread?.Join();

            _port.Close();
        }

        private void WorkingCycle()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (_isWorking) 
            {
                int time = Convert.ToInt32(timer.ElapsedMilliseconds);
                foreach (int channel in _dataStorage.GetAvailableChannels())
                {
                    try
                    {
                        var rawData = ExtractRawDataFromChannel(channel);

                        double data = ParseData(rawData);

                        AddDataToDataStorage(data, channel, time);
                    }
                    catch(CouldnotReadDataException ex)
                    {
                        continue;
                    }
                    catch(Exception)
                    {
                        _isWorking = false;
                        stopByErrorEvent?.Invoke();
                        return;
                    }

                    Thread.Sleep(_delay);
                } 
            }
        }

        private byte[] ExtractRawDataFromChannel(int channel)
        {
            var readFormChannelCommandBytes = ModbusReadCommandFabric.GetReadChannelCommand(channel, _deviceAddress);

            _port.WriteData(readFormChannelCommandBytes);

            var rawData = _port.ReadData(RESPONSE_LENGTH, TIMEOUT);

            if(rawData.Length > RESPONSE_LENGTH)
            {
                byte[] trimmed = new byte[RESPONSE_LENGTH];
                Array.Copy(rawData, trimmed, RESPONSE_LENGTH);
                return trimmed;
            }

            if(rawData.Length < RESPONSE_LENGTH) 
            {
                throw new CouldnotReadDataException();
            }

            return rawData;
        }

        private double ParseData(byte[] data)
        {
            try
            {
                byte[] channelData = new byte[2];
                channelData[0] = data[3]; 
                channelData[1] = data[4];

                string ChannelDataString = BitConverter.ToString(channelData).Replace("-", "");

                return _converter.ConvertFromHexToDoubleFromChannelData(ChannelDataString);
            }
            catch
            {
                throw new CouldnotReadDataException();
            }
        }

        private void AddDataToDataStorage(double value, int channel, int time)
        {
            _dataStorage.AddNewValueToChannel(channel, value, time);
        }
    }
}
