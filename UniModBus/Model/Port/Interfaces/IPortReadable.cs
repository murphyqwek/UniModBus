using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.Port.Interfaces
{
    public interface IPortReadable : IPort
    {
        public byte[] ReadAllData();

        public byte[] ReadData(int Timeout);

        public byte[] ReadData(int bytesToRead, int Timeout);
    }
}
