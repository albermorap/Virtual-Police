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
    /// Lógica de interacción para UI_NotificacionesDenuncia.xaml
    /// </summary>
    public partial class UI_NotificacionesDenuncia : Window
    {
        AgenteWS user;
        VPServiceClient cliente;
        DenunciaWS[] notificaciones;

        public UI_NotificacionesDenuncia(AgenteWS user, out bool existen)
        {
            InitializeComponent();

            this.user = user;
            cliente = new VPServiceClient();

            notificaciones = cliente.GetNotificaciones();

            if (notificaciones == null)
            {
                MessageBox.Show("No existe ninguna notificación disponible.", "Sin notificaciones", MessageBoxButton.OK, MessageBoxImage.Information);
                existen = false;
            }
            else
            {
                existen = true;
                foreach (DenunciaWS notif in notificaciones)
                {
                    listaNotificaciones.Items.Add(new
                    {
                        id=notif.Id,
                        fecha=DateTime.Now.ToShortDateString(),
                        tipo=notif.Tipo
                    });
                }
            }
        }

        private void validar_Click(object sender, RoutedEventArgs e)
        {
            if (listaNotificaciones.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos una notificación.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (listaNotificaciones.SelectedItems.Count > 1)
            {
                MessageBox.Show("Seleccione únicamente una notificación.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DenunciaWS d = cliente.GetDenuncia(notificaciones[listaNotificaciones.SelectedIndex].Id);
                UI_VisualizarDenuncia w = new UI_VisualizarDenuncia(user, d, true);
                w.Show();
                this.Close();
            }
        }

        private void salir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(user);
            mw.Show();
            this.Close();
        }
    }
}
