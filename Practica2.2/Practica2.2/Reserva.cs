using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Practica2._2
{
    public class Reserva
    {
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public String direccion { get; set; }
        public String fecha_nac { get; set; }
        public String sexo { get; set; }
        public int num_acomp { get; set; }
        public String periodo_reserva { get; set; }

        public Reserva()
        {

        }

        public Reserva (string nombre, string apellidos, string direccion, string fecha_nac, String sexo, int num_acomp, string periodo_reserva)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.fecha_nac = fecha_nac;
            this.sexo = sexo;
            this.num_acomp = num_acomp;
            this.periodo_reserva = periodo_reserva;
        }
    }
}