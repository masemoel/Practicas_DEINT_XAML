using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practica2._2
{
    public partial class MainWindow : Window
    {
        bool prerellenado = false; // Variable que se cambiará a true si se abrió la interfaz a raíz de una reserva hecha (evita excepciones con guardarReserva)

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            calendario.BlackoutDates.AddDatesInPast();
            calendario.SelectedDate = DateTime.Now.AddDays(1);
        }

        // Método que reinicia los valores del formulario a los de por defecto.
        private void limpiarInfo(object sender, RoutedEventArgs e)
        {
            nombrebox.Text = "";
            apellidosbox.Text = "";
            direccionbox.Text = "";
            datepickeruser.Text = "";
            hombrebutton.IsChecked = false;
            mujerbutton.IsChecked = false;
            slValue.Value = 1;
            calendario.SelectedDate = DateTime.Now.AddDays(1);
            MessageBox.Show("¡Información borrada con éxito!");
        }

        // Método utilizado por un botón para utilizar los datos rellenados
        // por el usuario en un archivo de texto.
        private void guardarArchivo(object sender, RoutedEventArgs e)
        {
            String sexo = "";
            if (hombrebutton.IsChecked == true)
            {
                sexo = "hombre";
            } else if (mujerbutton.IsChecked == true)
            {
                sexo = "mujer";
            }
            String periodo = calendario.SelectedDate.Value.ToString()+" - "+ listdias.Items[listdias.Items.Count - 1].ToString();
            String info = "Nombre: "+nombrebox.Text+"\nApellidos: "+apellidosbox.Text+"\nDirección: "+direccionbox.Text+"\nFecha de nacimiento: "+datepickeruser.Text+"\nSexo: "+sexo+"\nNúmero de acompañantes: "+slValue.Value+"\nDías reservados: "+periodo;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Guardar información de la reserva";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, info);
            }
        }

        // Método que cierra el formulario y ayuda a Window1
        // a almacenar los datos de la reserva.
        private void guardarReserva(object sender, RoutedEventArgs e)
        {
            if (prerellenado)
            {
                this.Hide();
            } else
            {
                this.DialogResult = true;
            }
        }

        // Método que se activa cada vez que el usuario rellena un campo
        // y que habilita los botones de guardar y reservar cuando todos
        // los campos han sido rellenados.
        private void activarRealizar(object sender, RoutedEventArgs e)
        {
            int nom=0, ap=0, dir=0, fn=0, sx=0, cont;
            if (nombrebox.Text != "")
            {
                nom = 1;
            }
            if (apellidosbox.Text != "")
            {
                ap = 1;
            }
            if (direccionbox.Text != "")
            {
                dir = 1;
            }
            if (datepickeruser.Text != "")
            {
                fn = 1;
            }
            if ((bool)(hombrebutton.IsChecked) || (bool)(mujerbutton.IsChecked))
            {
                sx = 1;
            }
            cont = nom + ap + dir + fn + sx;
            camposcont.Text = "Campos rellenados: "+(cont+2)+" de 7";
            camposprogress.Value = cont+2;
            if (cont == 5)
            {
                reservarbutton.IsEnabled = true;
                guardarbutton.IsEnabled = true;
            } else
            {
                reservarbutton.IsEnabled = false;
                guardarbutton.IsEnabled = false;
            }
        }

        // Método que automáticamente recoge el calendario cuando
        // el usuario ha seleccionado una fecha.
        private void desExpandir(object sender, RoutedEventArgs e)
        {
            expandir.IsExpanded = false;
        }

        // Método que devuelve todos los datos del usuario en un objeto.
        public Reserva getReserva()
        {
            Reserva reserva;
            String sexo = "";
            if (hombrebutton.IsChecked == true)
            {
                sexo = "hombre";
            }
            else if (mujerbutton.IsChecked == true)
            {
                sexo = "mujer";
            }
            String periodo = calendario.SelectedDate.Value.ToString() + " - " + listdias.Items[listdias.Items.Count - 1].ToString();
            reserva = new Reserva(nombrebox.Text, apellidosbox.Text, direccionbox.Text, datepickeruser.Text, sexo, (int)slValue.Value, periodo);
            return reserva;
        }

        // Método que permite abrir un formulario relleno
        // a raíz de datos introducidos previamente por el usuario.
        public void rellenarReserva(Reserva r)
        {
            nombrebox.Text = r.nombre;
            apellidosbox.Text = r.apellidos;
            direccionbox.Text = r.direccion;
            datepickeruser.Text = r.fecha_nac;
            if (r.sexo == "hombre")
            {
                hombrebutton.IsChecked = true;
            } else if (r.sexo == "mujer")
            {
                mujerbutton.IsChecked = true;
            }
            slValue.Value = r.num_acomp;
            calendario.SelectedDate = DateTime.Parse(r.periodo_reserva);
            prerellenado = true;
            reservarbutton.Content = "_Cerrar";
        }
    }
}