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
using System.IO;
using Microsoft.Win32;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para UI_NuevoExpediente.xaml
    /// </summary>
    public partial class UI_NuevoExpediente : Window
    {
        VPServiceClient db;
        DenunciaWS denuncia;
        ExpedienteWS exp;
        CiudadanoWS ciu;
        AgenteWS user;
        String sexo = "";
        Boolean cerrar = false;
        int passwordErronea = 3;
        string imageName = null;

        public UI_NuevoExpediente(DenunciaWS d, String dni, AgenteWS u)
        {
            InitializeComponent();
            db = new VPServiceClient();
            denuncia = d;
            user = u;
            ciu = db.GetCiudadano(dni);
            sexo = ciu.Sexo;

            //Inicialización de la ventana
            tb_DNI.Text = ciu.DNI;
            tb_DNI.IsEnabled = false;
            tb_nombre.Text = ciu.Nombre;
            tb_nombre.IsEnabled = false;
            tb_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
            tb_apellidos.IsEnabled = false;
            tb_fechaNacimiento.Text = Convert.ToString(ciu.Fecha_Nacimiento);
            tb_fechaNacimiento.IsEnabled = false;
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

            tb_idDenuncia.Text = d.Id + "";
            tb_idDenuncia.IsEnabled = false;
            tb_tipoDenuncia.Text = d.Tipo;
            tb_tipoDenuncia.IsEnabled = false;
            tb_fechaDenuncia.Text = d.Fecha_hecho + "";
            tb_fechaDenuncia.IsEnabled = false;
            tb_descripcionDenuncia.Text = d.Descripcion_hecho;
            tb_descripcionDenuncia.IsEnabled = false;
        }

        private void bt_confirmarExpediente_Click(object sender, RoutedEventArgs e)
        {
          
            if (comprobarTelefono())
            {

                //Creación expediente
                exp = new ExpedienteWS();
                exp.DNI = tb_DNI.Text;
                if (tb_altura.Text != "") exp.Altura = Convert.ToDouble(tb_altura.Text);
                if (tb_peso.Text != "") exp.Peso = Convert.ToDouble(tb_peso.Text);
                if (cb_estadoCivil.SelectionBoxItem.ToString() != null) exp.Estado_Civil = cb_estadoCivil.SelectionBoxItem.ToString();
                exp.Telefono = tb_telefono.Text;
                
                if(imageName != null)
                {
                    System.Drawing.Image imageIn = new System.Drawing.Bitmap(imageName);
                    short type;
                    FotoWS imageWS = new FotoWS
                    {
                        Image = imageToByteArray(imageIn, out type),
                        Size = imageIn.Width * imageIn.Height,
                        Type = type
                    };

                    exp.Foto = imageWS;
                }

                inputBox ib = new inputBox("Introduzca su contraseña:",6);
                ib.ShowDialog();
                String password = inputBox.devolver(); 

                if (db.ComprobarPassword(user.NumPlaca, password))
                {
                    if (db.CrearExpediente(exp) && db.AsociarExpediente(denuncia.Id, exp.DNI))
                    {
                        cerrar = true;
                        MessageBoxResult result = MessageBox.Show("Expediente creado correctamente.", "¡Éxito!", MessageBoxButton.OK, MessageBoxImage.Information);
                        UI_VisualizarExpediente visualiza = new UI_VisualizarExpediente(exp, user);
                        this.Close();
                        visualiza.Show();
                    }
                    else MessageBox.Show("¡Error al crear el expediente!");
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
        }

        public bool comprobarTelefono()
        {
            String mas = "+";
            Boolean tlfOk = false;

            if (tb_telefono.Text.Length > 0)
            {
                String primero = tb_telefono.Text.Substring(0, 1);
                if (tb_telefono.Text.Length == 14 && primero == mas)
                {
                    try
                    {
                        Int64 x = Int64.Parse(tb_telefono.Text.Substring(1, 13));
                        tlfOk = true;
                    }
                    catch (FormatException)
                    {
                        tlfOk = false;
                        MessageBox.Show("No se ha podido procesar el teléfono, inténtalo de nuevo.");
                        tb_telefono.Text = "";
                        tb_telefono.BorderBrush = Brushes.Red;
                    }

                }
                else
                {
                    tlfOk = false;
                    MessageBox.Show("Introduzca el teléfono correctamente.");
                    tb_telefono.Text = "";
                    tb_telefono.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tlfOk = false;
                MessageBox.Show("Introduzca el teléfono correctamente.");
                tb_telefono.Text = "";
                tb_telefono.BorderBrush = Brushes.Red;
            }

            return tlfOk;
        }

        private void tb_telefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_telefono.BorderBrush = Brushes.Gray;
        }

        private void bt_cancelarExpediente_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!cerrar)
            {
                MessageBoxResult result = MessageBox.Show("¿Está seguro que quiere cancelar la creación del expediente?", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void examinar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Archivos de imágenes (*.bmp, *.jpg, *.gif; *.png)|*.bmp;*.jpg; *.gif; *.png";
            file.ShowDialog();
            if (file.FileName != "")
            {
                FileInfo info = new System.IO.FileInfo(file.FileName);


                if (info.Length < 114668)
                {
                    imageName = file.FileName;
                    i_fotoDelincuente.Source = new BitmapImage(new Uri(file.FileName));
                }
                else
                {
                    MessageBox.Show("Imagen demasiado grande", "Error de tamaño", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void borrarFoto_Click(object sender, RoutedEventArgs e)
        {
            imageName = null;
            i_fotoDelincuente.Source = null;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn, out short type)
        {

            MemoryStream ms = new MemoryStream();
            if (imageName.EndsWith("bmp"))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                type = 1;
            }
            else if (imageName.EndsWith("gif"))
            {
                type = 2;
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            }
            else if (imageName.EndsWith("png"))
            {
                type = 3;
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                type = 4;
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
        }
    }
}
