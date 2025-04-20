using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace UniModBus.Model.Port
{
    public class SerialPortWrapper : IPort
    {
        private readonly SerialPort _serialPort;

        public SerialPortWrapper(SerialPort serialPort)
        {
            serialPort = serialPort ?? throw new ArgumentNullException(nameof(serialPort));
        }

        public SerialPortWrapper(string portName, int baudRate) : this(new SerialPort(portName, baudRate))
        { }

        public void Close()
        {
            _serialPort.Close();
        }

        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public byte[] ReadAllData()
        {
            int bytesToRead = _serialPort.BytesToRead;

            byte[] data = new byte[bytesToRead];

            _serialPort.Read(data, 0, bytesToRead);

            return data;
        }

        public byte[] ReadData(int bytesToRead)
        {
            int bytes;
            do
            {
                try
                {
                    Thread.Sleep(5);
                    bytes = _serialPort.BytesToRead;
                }
                catch
                {
                    return new byte[0];
                }
            }
            while (_serialPort.IsOpen && bytes < bytesToRead);

            byte[] buffer = new byte[bytes];
            _serialPort.Read(buffer, 0, bytes);
            _serialPort.DiscardInBuffer();

            return buffer;
        }

        public byte[] ReadData(int bytesToRead, int timeout)
        {
            int bytes;
            int countTimeouts = 0;
            do
            {
                try
                {
                    Thread.Sleep(5);
                    bytes = _serialPort.BytesToRead;
                    countTimeouts++;
                }
                catch
                {
                    return new byte[0];
                }
            }
            while (_serialPort.IsOpen && bytes < bytesToRead && countTimeouts < timeout);

            if(countTimeouts >= timeout)
            {
                return new byte[0];
            }

            byte[] buffer = new byte[bytes];
            _serialPort.Read(buffer, 0, bytes);
            _serialPort.DiscardInBuffer();

            return buffer;
        }

        public void WriteData(byte[] data)
        {
            _serialPort.Write(data, 0, data.Length);
        }

        public void WriteData(string data)
        {
            _serialPort.Write(data);
        }
    }
}
