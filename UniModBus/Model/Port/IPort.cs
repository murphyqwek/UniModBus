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

        public byte[] ReadAllData();

        public byte[] ReadData(int Timeout);

        public byte[] ReadData(int bytesToRead, int Timeout);

        public void WriteData(byte[] data);
        public void WriteData(string data);
    }
}
