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

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para confirmarContrasena.xaml
    /// </summary>
    public partial class inputBox : Window
    {
        static string cadena;

        public inputBox(string mensaje, int max)
        {
            InitializeComponent();
            lb_introducirDatos.Content = mensaje;
            tb_datos.MaxLength = max;
            tb_datos.Focus();
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (tb_datos.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Debes introducir un valor correcto.", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                cadena = tb_datos.Text;
                this.Close();
            }
        }

        public static string devolver()
        {
            return cadena;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tb_datos.Text == "")
            {
                cadena = "";
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Aceptar_Click(null, null);
            }
        }


    }
}
