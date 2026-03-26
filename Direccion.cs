using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tarea2_de_navegador
{
    internal class Direccion
    {
        string url;
        DateTime fechaAcceso;
        int veces;

        public string Url { get => Url; set => Url = value; }
        public DateTime FechaAcceso { get => fechaAcceso; set => fechaAcceso = value; }
        public int Veces { get => veces; set => veces = value; }
    }
}
