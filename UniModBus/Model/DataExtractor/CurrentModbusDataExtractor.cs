using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniModBus.Model.DataStorage;
using UniModBus.Model.Port.Interfaces;

namespace UniModBus.Model.DataExtractor
{
    /// <summary>
    /// Класс для получения данных из порта и загрузка их в CurrentModbusDataStorage в многопоточном режиме
    /// </summary>
    public class CurrentModbusDataExtractor
    {
        private IPortReadableWritable port;
        private CurrentModbusDataStorage dataStorage;

        public CurrentModbusDataExtractor(IPortReadableWritable port, CurrentModbusDataStorage dataStorage)
        {
            this.port = port;
            this.dataStorage = dataStorage;
        }

    }
}
