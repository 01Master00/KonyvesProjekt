using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KonyvesProjekt
{
    internal class Konyv
    {
        private int id;
        private string szerzo;
        private string cim;
        private string kinel;
        private string mikortol;

        public Konyv(int id, string szerzo, string cim)
        {
            this.id = id;
            this.szerzo = szerzo;
            this.cim = cim;
            this.kinel = "9999";
            this.mikortol = null;
        }

        public Konyv(int id, string szerzo, string cim, string kinel, string mikortol)
        {
            this.id = id;
            this.szerzo = szerzo;
            this.cim = cim;
            this.kinel = kinel;
            this.mikortol = mikortol;
        }

        public int Id { get => id; set => id = value; }
        public string Szerzo { get => szerzo; set => szerzo = value; }
        public string Cim { get => cim; set => cim = value; }
        public string Kinel { get => kinel; set => kinel = value; }
        public string Mikortol { get => mikortol; set => mikortol = value; }


        public override string ToString()
        {
            return $"{id} {szerzo.PadRight(20)} {cim.PadRight(30)} {kinel.PadRight(20)} {mikortol}";
        }
    }
}
