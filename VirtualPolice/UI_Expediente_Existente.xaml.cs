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
using System.Windows.Shapes;
using VirtualPolice.VPServiceReference;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para UI_Expediente.xaml
    /// </summary>
    public partial class UI_Expediente_Existente : Window
    {
        VPServiceClient db;
        DenunciaWS denuncia;
        ExpedienteWS exp;
        CiudadanoWS ciu;
        AgenteWS user;
        Boolean cerrar = false;
        int passwordErronea = 3;

        public UI_Expediente_Existente(DenunciaWS d, ExpedienteWS e, AgenteWS u)
        {
            InitializeComponent();
            db = new VPServiceClient();
            denuncia = d;
            exp = e;
            ciu = db.GetCiudadano(exp.DNI);
            user = u;
            
            //Inicialización de la ventana
            tb_DNI.Text = e.DNI;
            tb_DNI.IsEnabled = false;
            tb_nombre.Text = ciu.Nombre;
            tb_nombre.IsEnabled = false;
            tb_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
            tb_apellidos.IsEnabled = false; 
            tb_idDenuncia.Text = d.Id.ToString();
            tb_idDenuncia.IsEnabled = false;
            tb_tipoDenuncia.Text = d.Tipo;
            tb_tipoDenuncia.IsEnabled = false;
            tb_fechaDenuncia.Text = d.Fecha_hecho.ToShortDateString();
            tb_fechaDenuncia.IsEnabled = false;
            tb_descripcionDenuncia.Text = d.Descripcion_hecho;
            tb_descripcionDenuncia.IsEnabled = false;

            FotoWS image = e.Foto;

            if (image != null)
            {
                i_fotoDelincuente.Source = byteArrayToImage(image.Image, image.Type);
            }
        }

        private void bt_confirmarExpediente_Click(object sender, RoutedEventArgs e)
        {

            inputBox ib = new inputBox("Introduzca su contraseña (quedan " + passwordErronea + " intentos)", 6);
            ib.ShowDialog();
            String password = inputBox.devolver(); 

            if (db.ComprobarPassword(user.NumPlaca, password))
            {
                if (db.AsociarExpediente(denuncia.Id, exp.DNI))
                {
                    cerrar = true;
                    MessageBoxResult result = MessageBox.Show("Denuncia asociada correctamente.", "¡Éxito!", MessageBoxButton.OK, MessageBoxImage.Information);
                    UI_VisualizarExpediente visualiza = new UI_VisualizarExpediente(exp, user);
                    this.Close();
                    visualiza.Show();
                }
                else MessageBox.Show("¡Error al asociar la denuncia!");
            }
            else
            {
                MessageBox.Show("Ha introducido la contraseña erróneamente.");
                passwordErronea--;
            }

            if (passwordErronea == 0)
            {
                cerrar = true;
                MessageBoxResult result = MessageBox.Show("Número de intentos permitidos para introducir la contraseña superado.", "¡Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void bt_cancelarExpediente_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!cerrar) { 
                MessageBoxResult result = MessageBox.Show("¿Está seguro que quiere salir sin asociar la denuncia?", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public System.Windows.Media.Imaging.BitmapImage byteArrayToImage(byte[] byteArrayIn, short type)
        {
            System.Windows.Media.Imaging.BitmapImage bImg = new System.Windows.Media.Imaging.BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(byteArrayIn);
            bImg.EndInit();
            return bImg;
        }
    }
}
