using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Practica2._2
{
    public partial class Window1 : Window
    {
        bool azul = false; // utilizado por el ContextMenu
        Reserva ultimaR; // objeto donde se almacenará la última reserva realizada
        List<Reserva> reservas = new List<Reserva>(); // lista en el que se almacenan todas las reservas

        // Constructor
        public Window1()
        {
            InitializeComponent();
        }

        // Método que habilitará o deshabilitará el modo oscuro en la inferfaz
        public void modoOscuro(object sender, RoutedEventArgs e)
        {
            if ((bool)oscurocheck.IsChecked)
            {
                fondoprincipal.Background = new SolidColorBrush(Colors.DimGray);
                menuae.Background = new SolidColorBrush(Colors.DimGray);
                menuae.Foreground = new SolidColorBrush(Colors.White);
                creareserva.Foreground = new SolidColorBrush(Colors.Black);
                abrirreservaitem.Foreground = new SolidColorBrush(Colors.Black);
                salirr.Foreground = new SolidColorBrush(Colors.Black);
                crearbutton.Background = new SolidColorBrush(Colors.DarkSlateGray);
                crearbutton.Foreground = new SolidColorBrush(Colors.White);
                abrirRbutton.Background = new SolidColorBrush(Colors.DarkSlateGray);
                abrirRbutton.Foreground = new SolidColorBrush(Colors.White);
                salirbutton.Background = new SolidColorBrush(Colors.DarkSlateGray);
                salirbutton.Foreground = new SolidColorBrush(Colors.White);
            } else
            {
                fondoprincipal.Background = new SolidColorBrush(Colors.White);
                menuae.Background = new SolidColorBrush(Colors.White);
                menuae.Foreground = new SolidColorBrush(Colors.Black);
                crearbutton.Background = new SolidColorBrush(Colors.LightGray);
                crearbutton.Foreground = new SolidColorBrush(Colors.Black);
                abrirRbutton.Background = new SolidColorBrush(Colors.LightGray);
                abrirRbutton.Foreground = new SolidColorBrush(Colors.Black);
                salirbutton.Background = new SolidColorBrush(Colors.LightGray);
                salirbutton.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        // Método que abrirá el formulario y guardará la reserva al finalizar
        public void crearFormulario(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            if (mw.ShowDialog() == true)
            {
                ultimaR = mw.getReserva();
                reservas.Add(ultimaR);
                reservasgrid.ItemsSource = null;
                reservasgrid.ItemsSource = reservas;
                abrirRbutton.IsEnabled = true;
                abrirreservaitem.IsEnabled = true;
            }
        }

        // Método que finaliza el programa de reservas
        public void salir(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // Método que despliega el ContextMenu cuando se realice click derecho
        // en el textblock de las reservas almacenadas.
        private void reservasClick(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        // Método utilizado por el ContextMenu que cambiará el color de texto
        // a azul o negro según el estado de la variable azul.
        private void cambiarColor(object sender, RoutedEventArgs e)
        {
            if (azul)
            {
                reservasgrid.Foreground = new SolidColorBrush(Colors.Black);
                azul = false;
            } else
            {
                reservasgrid.Foreground = new SolidColorBrush(Colors.Blue);
                azul = true;
            }
        }

        // Método que eliminará la celda seleccionada de la tabla de reservas.
        private void borrarReserva(object sender, RoutedEventArgs e)
        {
            if (reservasgrid.Items.IndexOf(reservasgrid.CurrentItem) == null)
            {
                var seleccionado = reservasgrid.Items.IndexOf(reservasgrid.CurrentItem);
                reservas.RemoveAt(seleccionado);
                reservasgrid.ItemsSource = null;
                reservasgrid.ItemsSource = reservas;
            }
        }

        // Método que abre la última reserva guardada desde la interfaz.
        private void abrirUltimaReserva(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.rellenarReserva(ultimaR);
        }
    }
}