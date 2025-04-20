using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.DataStorage
{
    /// <summary>
    /// Класс для хранения данных с каналов счетчика
    /// </summary>
    public class CurrentModbusDataStorage
    {
        public void AddNewValueToChannel(int channel, double value, double time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetAvailableChannels()
        {
            throw new NotImplementedException();
        }
    }
}
