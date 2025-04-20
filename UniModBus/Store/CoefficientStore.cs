using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniModBus.Store
{
    public class CoefficientStore
    {
        public double HolostMove { get; set; }
        public double AmperKoeff { get; set; }
        public double VoltKoeff { get; set; }
        public double KoeffValueChannel { get; set; }

        public CoefficientStore(double HolostMove, double AmperKoeff, double VoltKoeff, double KoeffValueChannel)
        {
            this.HolostMove = HolostMove;
            this.AmperKoeff = AmperKoeff;
            this.VoltKoeff = VoltKoeff;
            this.KoeffValueChannel = KoeffValueChannel;
        }
        
    }
}
