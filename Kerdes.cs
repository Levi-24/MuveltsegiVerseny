using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuveltsegiVerseny
{
    class Kerdes
    {
        public string MuveltsegiKerdes { get; set; }
        public int Valasz { get; set; }
        public int Pont { get; set; }
        public string Temakor { get; set; }

        public Kerdes(string sor)
        {
            var adat = sor.Split(';');
            this.MuveltsegiKerdes = adat[0];
            this.Valasz = int.Parse(adat[1]);
            this.Pont = int.Parse(adat[2]);
            this.Temakor = adat[3];
        }

        public override string ToString()
        {
            return MuveltsegiKerdes;
        }
    }
}
