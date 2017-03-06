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
    /// Lógica de interacción para UI_ListaDenuncias.xaml
    /// </summary>
    public partial class UI_ListaDenuncias : Window
    {
        AgenteWS user;
        VPServiceClient cliente;
        DenunciaWS[] denuncias;

        public UI_ListaDenuncias(AgenteWS user, out bool existen)
        {
            InitializeComponent();

            this.user = user;
            cliente = new VPServiceClient();

            denuncias = cliente.GetDenuncias(null);

            if (denuncias == null)
            {
                MessageBox.Show("No existe ninguna denuncia disponibles.", "Sin notificaciones", MessageBoxButton.OK, MessageBoxImage.Information);
                existen = false;
            }
            else
            {
                existen = true;
                foreach (DenunciaWS notif in denuncias)
                {
                    listaNotificaciones.Items.Add(new
                    {
                        id=notif.Id,
                        fecha=notif.Fecha_creacion,
                        tipo=notif.Tipo
                    });
                }
            }
        }

        private void ver_Click(object sender, RoutedEventArgs e)
        {
            if (listaNotificaciones.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos una denuncia.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (listaNotificaciones.SelectedItems.Count > 1)
            {
                MessageBox.Show("Seleccione únicamente una denuncia.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DenunciaWS d = cliente.GetDenuncia(denuncias[listaNotificaciones.SelectedIndex].Id);
                UI_VisualizarDenuncia w = new UI_VisualizarDenuncia(user, d, false);
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
