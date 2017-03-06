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
using VirtualPolice.VPServiceReference;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para UI_Login.xaml
    /// </summary>
    public partial class UI_Login : Window
    {
        VPServiceClient cliente;
        AgenteWS user;
        
        public UI_Login()
        {
            InitializeComponent();
            cliente = new VPServiceClient();
            pass.IsEnabled = false;
            tb_user.Focus();
        }
        
        private void login(object sender, RoutedEventArgs e)
        {

            if(cliente.ComprobarPassword(tb_user.Text, pass.Password))
            {
                user = cliente.GetAgente(tb_user.Text);

                MainWindow menu = new MainWindow(user);
                this.Close();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos, vuelva a intentarlo.", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_user.GotFocus += on_tb_user;

        }

        private void on_tb_user(object sender, EventArgs e)
        {
            tb_user.Text = "";
            pass.IsEnabled = true;
            pass.Password = "";
            tb_user.Foreground = Brushes.Black;
             
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                login(sender, e);
            }
        }
    }
}
