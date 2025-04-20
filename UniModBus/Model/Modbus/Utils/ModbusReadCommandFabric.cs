using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.Modbus.Utils
{
    /// <summary>
    /// Класс-фабрика для создания команд для чтения данных из канала
    /// </summary>
    public static class ModbusReadCommandFabric
    {

        //Команда на считывание данных с канала
        static private byte[] _readChannelCommandBase = new byte[]
        {
            0x02, //0 - адрес модуля
            0x04, //1
            0x00, //2
            0x00, //3 - номер канала
            0x00, //4
            0x01, //5
        };

        public static byte[] GetReadChannelCommand(int ChannelNumber, byte deviceAdress)
        {
            if (ChannelNumber < 0 || ChannelNumber > 7)
            {
                throw new ArgumentOutOfRangeException("ChannelNumber", "Неверный номер канала");
            }

            byte[] readChannelCommand = new byte[_readChannelCommandBase.Length];
            Array.Copy(_readChannelCommandBase, readChannelCommand, _readChannelCommandBase.Length);

            readChannelCommand[0] = deviceAdress;

            readChannelCommand[3] = Convert.ToByte(ChannelNumber);
            var crc = ModBusCRC.CalculateCRC(readChannelCommand); //Получаем контрольную сумму команды

            var readChannelCommandWithCrc = readChannelCommand.ToList();

            readChannelCommandWithCrc.Add(crc[0]);
            readChannelCommandWithCrc.Add(crc[1]);

            return readChannelCommandWithCrc.ToArray();
        }
    }
}
