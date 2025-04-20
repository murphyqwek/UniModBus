using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.Port.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с портами - объектами, откуда берутся данные или записываются
    /// </summary>
    public interface IPort
    {
        public bool IsOpen();
        public void Close();
        public void Open();
    }
}
