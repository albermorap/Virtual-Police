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
using System.ComponentModel;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para Portada.xaml
    /// </summary>
    public partial class Portada : Window
    {
        //private BackgroundWorker worker;
        //int percentage = 0;

        public Portada()
        {
            InitializeComponent();
        }

        private void sb_Completed(object sender, EventArgs e)
        {
            try
            {
                VPServiceClient cliente = new VPServiceClient();
                if (cliente.ComprobarConexion()) // Comprueba la conexión con la BD y si es correcta permite acelerar posteriores consultas.
                {
                    UI_Login l = new UI_Login();
                    l.Show();
                }
                else
                {
                    MessageBox.Show("Imposible conectar con el servidor", "ERROR Web Service", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Imposible conectar con el servidor", "ERROR Web Service", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }

        /* Código para otro hilo
        private void StartWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += delegate(object s, DoWorkEventArgs e)
            {
                VPServiceClient cliente = new VPServiceClient();
                e.Result = cliente.ComprobarConexion();  // Comprueba la conexión con la BD y si es correcta permite acelerar posteriores consultas.
            };

            worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs e)
            {
                if (e.Result.Equals(false))
                {
                    MessageBox.Show("Imposible conectar con el servidor", "ERROR Web Service", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            };

            worker.RunWorkerAsync();
        }*/
    }
}
