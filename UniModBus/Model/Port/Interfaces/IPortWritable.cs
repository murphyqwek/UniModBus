using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.Port.Interfaces
{
    public interface IPortWritable
    {
        public void WriteData(byte[] data);
        public void WriteData(string data);
    }
}
