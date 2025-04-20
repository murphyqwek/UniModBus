using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Model.Port
{
    public interface IPort
    {
        public bool IsOpen();
        public void Close();
        public void Open();

        public byte[] ReadData();

        public void WriteData(byte[] data);
        public void WriteData(string data);
    }
}
