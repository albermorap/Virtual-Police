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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualPolice.VPServiceReference;


namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VPServiceClient cliente;
        AgenteWS user;
        ExpedienteWS exp;
        
        public MainWindow(AgenteWS ag)
        {
            InitializeComponent();

            cliente = new VPServiceClient();
            user = ag;

            if (user.Rango != "Comisario")
            {
                b_comisario.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                b_plan_agente.Visibility = Visibility.Hidden;
            }
            b_alta.Visibility = System.Windows.Visibility.Hidden;
            b_plan_comisario.Visibility = System.Windows.Visibility.Hidden;
            b_crear_d.Visibility = System.Windows.Visibility.Hidden;
            b_ver_d.Visibility = System.Windows.Visibility.Hidden;
            b_online.Visibility = System.Windows.Visibility.Hidden;
            
            e_expand.Header = user.Nombre + " " + user.Apellido1 + " " + user.Apellido2;
            l_numplaca.Content = "Nº Placa: " + user.NumPlaca;
            l_rango.Content = "Rango: " + user.Rango;

        }

        private void Cerrar_Sesion(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("¿Desea cerrar esta sesión?\n", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
            {
                UI_Login login = new UI_Login();
                this.Close();
                login.Show();
            }
            
        }

        private void b_denuncias_Click(object sender, RoutedEventArgs e)
        {
            b_crear_d.Visibility = System.Windows.Visibility.Visible;
            b_ver_d.Visibility = System.Windows.Visibility.Visible;
            b_online.Visibility = System.Windows.Visibility.Visible;
            b_alta.Visibility = System.Windows.Visibility.Hidden;
            b_plan_comisario.Visibility = System.Windows.Visibility.Hidden;
        }

        private void b_expediente_Click(object sender, RoutedEventArgs e)
        {
            inputBox ib = new inputBox("Introduce el DNI del expediente que quieres ver:", 9);
            ib.ShowDialog();
            String expediente = inputBox.devolver(); 
            
            ExpedienteWS exp = cliente.GetExpediente(expediente);

            if (exp != null)
            {
                UI_VisualizarExpediente v_exp = new UI_VisualizarExpediente(exp, user);
                v_exp.Show();
            }
            else
            {
                MessageBox.Show("No existe un expediente relacionado con ese DNI.");
            }
            
        }

        private void b_plan_agente_Click(object sender, RoutedEventArgs e)
        {
            UI_ConsultarPlanning plan1 = new UI_ConsultarPlanning(user, user);
            this.Close();
            plan1.Show();
        }

        private void b_comisario_Click(object sender, RoutedEventArgs e)
        {
            b_alta.Visibility = System.Windows.Visibility.Visible;
            b_plan_comisario.Visibility = System.Windows.Visibility.Visible;
            b_crear_d.Visibility = System.Windows.Visibility.Hidden;
            b_ver_d.Visibility = System.Windows.Visibility.Hidden;
            b_online.Visibility = System.Windows.Visibility.Hidden;
        }

        private void b_crear_d_Click(object sender, RoutedEventArgs e)
        {
            UI_CrearDenuncia crear_denuncia = new UI_CrearDenuncia(user, null);
            this.Close();
            crear_denuncia.Show();
        }
        
        private void b_ver_d_Click(object sender, RoutedEventArgs e)
        {
            bool existen;
            UI_ListaDenuncias w = new UI_ListaDenuncias(user, out existen);
            if (existen)
            {
                w.Show();
            this.Close();
            }
        }

        private void b_alta_Click(object sender, RoutedEventArgs e)
        {
            UI_AltaAgente crear_agente = new UI_AltaAgente(user);
            this.Close();
            crear_agente.Show();
        }

        private void b_plan_comisario_Click(object sender, RoutedEventArgs e)
        {
            UI_ListaAgentes plan1 = new UI_ListaAgentes(user);
            this.Close();
            plan1.Show();
        }

        private void b_online_Click(object sender, RoutedEventArgs e)
        {
            bool existen;
            UI_NotificacionesDenuncia w = new UI_NotificacionesDenuncia(user, out existen);
            if (existen)
            {
                w.Show();
            this.Close();
            }
        }

        private void data_user(object sender, RoutedEventArgs e)
        {
            //Nueva ventana
        }

        private void b_acercade_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("VIRTUALPOLICE© Versión 2.0\n\n Pablo\n Álvaro\n Alberto\n Santiago\n Francisco\n Carlos", "Acerca de VirtualPolice", MessageBoxButton.OK);
        }

        private void b_ayuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("Para cualquier tipo de duda, contacte con el administrador de sistemas informáticos de la comisaria.\n", "Ayuda de VirtualPolice", MessageBoxButton.OK);
        }
    }
}
