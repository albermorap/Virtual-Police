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
    /// Lógica de interacción para UI_VisualizarExpediente.xaml
    /// </summary>
    public partial class UI_VisualizarExpediente : Window
    {
        VPServiceClient cliente;
        ExpedienteWS exp;
        CiudadanoWS ciu;
        DenunciaWS[] denuncias;
        AgenteWS user;

        public UI_VisualizarExpediente(ExpedienteWS e, AgenteWS u)
        {
            InitializeComponent();
            cliente = new VPServiceClient();
            exp = e;
            user = u;
            ciu = cliente.GetCiudadano(exp.DNI);

            //Inicialización de la ventana
            tb_DNI.Text = ciu.DNI;
            tb_DNI.IsEnabled = false;
            tb_nombre.Text = ciu.Nombre;
            tb_nombre.IsEnabled = false;
            tb_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
            tb_apellidos.IsEnabled = false;
            tb_fechaNacimiento.Text = ciu.Fecha_Nacimiento.ToShortDateString();
            tb_fechaNacimiento.IsEnabled = false;
            tb_edad.Text = exp.Edad.ToString();
            tb_edad.IsEnabled = false;
            if (ciu.Sexo.Equals("H"))
            {
                rb_sexoHombre.IsChecked = true;
            }
            else if (ciu.Sexo.Equals("M"))
            {
                rb_sexoMujer.IsChecked = true;
            }
            rb_sexoHombre.IsEnabled = false;
            rb_sexoMujer.IsEnabled = false;

            //Dirección postal
            tb_calle.Text = ciu.Calle;
            tb_calle.IsEnabled = false;
            tb_numero.Text = ciu.Numero;
            tb_numero.IsEnabled = false;
            tb_piso.Text = ciu.Letra_Piso;
            tb_piso.IsEnabled = false;
            tb_localidad.Text = ciu.Localidad;
            tb_localidad.IsEnabled = false;
            tb_provincia.Text = ciu.Provincia;
            tb_provincia.IsEnabled = false;
            tb_cp.Text = ciu.CP;
            tb_cp.IsEnabled = false;
            tb_pais.Text = ciu.Nacionalidad;
            tb_pais.IsEnabled = false;

            //Datos adicionales
            tb_altura.Text = exp.Altura.ToString();
            tb_altura.IsEnabled = false;
            tb_peso.Text = exp.Peso.ToString();
            tb_peso.IsEnabled = false;
            tb_telefono.Text = exp.Telefono;
            tb_telefono.IsEnabled = false;
            tb_estadoCivil.Text = exp.Estado_Civil;
            tb_estadoCivil.IsEnabled = false;

            FotoWS image = e.Foto;

            if (image != null)
            {
                i_fotoDelincuente.Source = byteArrayToImage(image.Image, image.Type);
            }
            
            //Lista de delitos
            denuncias = cliente.GetDenuncias(exp.DNI);

            if (denuncias != null)
            {
                foreach (DenunciaWS d in denuncias)
                {
                    lv_listaDelitos.Items.Add(new
                    {
                        ID = d.Id,
                        FECHA = d.Fecha_hecho.ToShortDateString(),
                        TIPO = d.Tipo
                    });
                }
            }
        }

        private void bt_volverDeExpediente_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
