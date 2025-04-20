using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniModBus.Store;

namespace UniModBus.Model.Modbus.Utils
{
    /// <summary>
    /// Класс для преобразования сырых данных из каналов
    /// </summary>
    public class ModBusValueConverter
    {
        private CoefficientStore _coefficients;

        public ModBusValueConverter(CoefficientStore coefficients)
        {
            this._coefficients = coefficients;
        }

        public double ConvertFromHexToDoubleFromChannelData(string ChannelData)
        {
            if (ChannelData == null)
                throw new ArgumentNullException(nameof(ChannelData));
            if (ChannelData.Length != 4)
                throw new ArgumentException("Channel Data must contains only 2 bytes");

            short Value = Convert.ToInt16("0x" + ChannelData, 16);
            double ConvertedValue = Value * _coefficients.KoeffValueChannel;
            double RoundedValue = Math.Round(ConvertedValue, 3);
            return RoundedValue;
        }


        public double ConvertToAmperValue(double Value) => Value * _coefficients.AmperKoeff;

        public double ConvertToVoltValue(double Value) => (Math.Abs(Value) - _coefficients.HolostMove) * _coefficients.VoltKoeff;
    }
}
