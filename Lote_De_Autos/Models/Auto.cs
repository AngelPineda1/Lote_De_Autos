using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lote_De_Autos.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set;}
        public string Version { get; set; }
        public ushort Anio { get; set; }
        public decimal Pecio { get; set; }
        public uint Kilometraje {  get; set; }
        public string Motor { get; set; }
        public string Transmision { get; set;}
        public string Carroceria { get; set; }
        public string Descripcion { get; set; }

    }
}
