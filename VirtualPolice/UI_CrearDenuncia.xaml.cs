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
using Microsoft.Win32;
using System.IO;
using VirtualPolice.VPServiceReference;
using System.Drawing;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para UI_CrearDenuncia.xaml
    /// </summary>
    public partial class UI_CrearDenuncia : Window
    {
        AgenteWS user;
        VPServiceClient cliente;
        string tipo = "";
        string imageName;
        
        public UI_CrearDenuncia(AgenteWS user, DenunciaWS notificacion)
        {
            InitializeComponent();

            cliente = new VPServiceClient();
            this.user = user;
            if (notificacion != null)
            {
                rellenaNotificacion(notificacion);
            }

            seleccionDenuncia.SelectedIndex = 0;
            hechos_naturaleza.SelectedIndex = 0;
        }

        private void rellenaNotificacion(DenunciaWS denuncia)
        {
            tipo = denuncia.Tipo;

            seleccion.Visibility = Visibility.Hidden;
            tabs.Visibility = Visibility.Visible;

            denun_dni.Text = denuncia.DNI_Denunciante;
            hechos_descripcion.Text = denuncia.Descripcion_hecho;
            hechos_direccion.Text = denuncia.Direccion_hecho;
            hechos_fecha.Text = denuncia.Fecha_hecho.ToShortDateString();
            hechos_hora.Text = denuncia.Hora_hecho.ToString();
            hechos_naturaleza.Text = denuncia.Naturaleza_lugar_hecho;

            switch (tipo)
            {
                case "Robo de vehiculo":
                    vehiculo.Visibility = Visibility.Visible;
                    v_matricula.Text = denuncia.RoboCoche.Matricula;
                    v_color.Text = denuncia.RoboCoche.Color;
                    v_marca.Text = denuncia.RoboCoche.Marca;
                    v_modelo.Text = denuncia.RoboCoche.Modelo;
                    v_bastidor.Text = denuncia.RoboCoche.NumBastidor;
                    break;
                case "Secuestro":
                    secuestrado.Visibility = Visibility.Visible;
                    secu_dni.Text = denuncia.Secuestro.DNI_Ciudadano;
                    break;
                case "Desaparicion":
                    desaparecido.Visibility = Visibility.Visible;
                    desa_dni.Text = denuncia.Desaparicion.DNI_Desaparecido;
                    desa_descripcion.Text = denuncia.Desaparicion.Descripcion_fisica;
                    // FOTO
                    break;
                default:
                    confirmarHechos.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void continuar_Click(object sender, RoutedEventArgs e)
        {
            tipo = seleccionDenuncia.SelectionBoxItem.ToString();

            seleccion.Visibility = Visibility.Hidden;
            tabs.Visibility = Visibility.Visible;
            
            switch (tipo)
            {
                case "Robo de vehículo":
                    tipo = "Robo de vehiculo";
                    vehiculo.Visibility = Visibility.Visible;
                    break;
                case "Secuestro":
                    secuestrado.Visibility = Visibility.Visible;
                    break;
                case "Desaparición":
                    tipo = "Desaparicion";
                    desaparecido.Visibility = Visibility.Visible;
                    break;
                default:
                    confirmarHechos.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void salir_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("¿Desea cancelar la denuncia?\n", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow(user);
                mw.Show();
                this.Close();
            }
        }

        private void confirmar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarFormato())
            {
                if (comprobarObligatorios())
                {

                    TimeSpan hora = new TimeSpan(Int32.Parse(hechos_hora.Text), Int32.Parse(hechos_minutos.Text), 0);

                    inputBox ib = new inputBox("Introduzca su contraseña:", 6);
                    ib.ShowDialog();
                    String password = inputBox.devolver(); 

                    if (cliente.ComprobarPassword(user.NumPlaca, password))
                    {
                        DenunciaWS denuncia = new DenunciaWS
                        {
                            DNI_Denunciante = denun_dni.Text,
                            Direccion_hecho = hechos_direccion.Text,
                            Naturaleza_lugar_hecho = hechos_naturaleza.SelectionBoxItem.ToString(),
                            Fecha_hecho = hechos_fecha.SelectedDate.Value,
                            Hora_hecho = hora,
                            Descripcion_hecho = hechos_descripcion.Text,
                            NumPlaca = "1234K",
                            Tipo = tipo
                        };

                        if (tipo.Equals("Robo de vehiculo"))
                        {
                            RoboCocheWS roboCoche = new RoboCocheWS
                            {
                                Matricula = v_matricula.Text,
                                Color = v_color.Text,
                                Marca = v_marca.Text,
                                Modelo = v_modelo.Text,
                                NumBastidor = v_bastidor.Text
                            };

                            denuncia.RoboCoche = roboCoche;
                        }

                        if (tipo.Equals("Secuestro"))
                        {
                            SecuestroWS secu = new SecuestroWS
                            {
                                DNI_Ciudadano = secu_dni.Text
                            };

                            denuncia.Secuestro = secu;
                        }

                        if (tipo.Equals("Desaparicion"))
                        {
                            
                            System.Drawing.Image imageIn = new Bitmap(imageName);
                            short type;
                            FotoWS imageWS = new FotoWS
                            {
                                Image = imageToByteArray(imageIn, out type),
                                Size = imageIn.Width * imageIn.Height,
                                Type = type
                            };

                            DesaparicionWS desa = new DesaparicionWS
                            {
                                DNI_Desaparecido = desa_dni.Text,
                                Descripcion_fisica = desa_descripcion.Text,
                                Foto = imageWS
                            };

                            denuncia.Desaparicion = desa;
                        }

                        int id = cliente.AddDenuncia(denuncia);

                        if (id != -1)
                        {
                            MessageBox.Show("Denuncia creada con éxito", "Creación correcta", MessageBoxButton.OK, MessageBoxImage.Information);
                            UI_VisualizarDenuncia w = new UI_VisualizarDenuncia(user, cliente.GetDenuncia(id), false);
                            w.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error durante la creación de la denuncia", "Creación incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta", "Creación incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Campos obligatorios vacíos indicados en rojo", "Campos obligatorios vacíos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Formato incorrecto en los campos indicados en amarillo", "Formato incorrecto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void denun_dni_TextChanged(object sender, TextChangedEventArgs e)
        {
            denun_dni.Background = System.Windows.Media.Brushes.White;

            CiudadanoWS ciu = cliente.GetCiudadano(denun_dni.Text);

            if (ciu != null)
            {
                denun_nombre.Text = ciu.Nombre;
                denun_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
                denun_fecha.Text = ciu.Fecha_Nacimiento.ToShortDateString();
                if (ciu.Sexo == "H")
                {
                    denun_sexoHombre.IsChecked = true;
                }
                else if (ciu.Sexo == "M")
                {
                    denun_sexoMujer.IsChecked = true;
                }
                denun_calle.Text = ciu.Calle;
                denun_numero.Text = ciu.Numero;
                denun_piso.Text = ciu.Letra_Piso;
                denun_localidad.Text = ciu.Localidad;
                denun_provincia.Text = ciu.Provincia;
                denun_cp.Text = ciu.CP;
                denun_pais.Text = ciu.Nacionalidad;
            }
            else
            {
                denun_dni.Background = System.Windows.Media.Brushes.LightYellow;

                denun_nombre.Clear();
                denun_apellidos.Clear();
                denun_fecha.Clear();
                denun_sexoHombre.IsChecked = false;
                denun_sexoMujer.IsChecked = false;
                denun_calle.Clear();
                denun_numero.Clear();
                denun_piso.Clear();
                denun_localidad.Clear();
                denun_provincia.Clear();
                denun_cp.Clear();
                denun_pais.Clear();
            }
        }

        private void desa_dni_TextChanged(object sender, TextChangedEventArgs e)
        {
            desa_dni.Background = System.Windows.Media.Brushes.White;

            CiudadanoWS ciu = cliente.GetCiudadano(desa_dni.Text);

            if (ciu != null)
            {
                desa_nombre.Text = ciu.Nombre;
                desa_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
                desa_fecha.Text = ciu.Fecha_Nacimiento.ToShortDateString();
                if (ciu.Sexo == "H")
                {
                    desa_sexoHombre.IsChecked = true;
                }
                else if (ciu.Sexo == "M")
                {
                    desa_sexoMujer.IsChecked = true;
                }
                desa_calle.Text = ciu.Calle;
                desa_numero.Text = ciu.Numero;
                desa_piso.Text = ciu.Letra_Piso;
                desa_localidad.Text = ciu.Localidad;
                desa_provincia.Text = ciu.Provincia;
                desa_cp.Text = ciu.CP;
                desa_pais.Text = ciu.Nacionalidad;
            }
            else
            {
                desa_dni.Background = System.Windows.Media.Brushes.LightYellow;

                desa_nombre.Clear();
                desa_apellidos.Clear();
                desa_fecha.Clear();
                desa_sexoHombre.IsChecked = false;
                desa_sexoMujer.IsChecked = false;
                desa_calle.Clear();
                desa_numero.Clear();
                desa_piso.Clear();
                desa_localidad.Clear();
                desa_provincia.Clear();
                desa_cp.Clear();
                desa_pais.Clear();
            }
        }

        private void secu_dni_TextChanged(object sender, TextChangedEventArgs e)
        {
            secu_dni.Background = System.Windows.Media.Brushes.White;

            CiudadanoWS ciu = cliente.GetCiudadano(secu_dni.Text);

            if (ciu != null)
            {
                secu_nombre.Text = ciu.Nombre;
                secu_apellidos.Text = ciu.Apellido1 + " " + ciu.Apellido2;
                secu_fecha.Text = ciu.Fecha_Nacimiento.ToShortDateString();
                if (ciu.Sexo == "H")
                {
                    secu_sexoHombre.IsChecked = true;
                }
                else if (ciu.Sexo == "M")
                {
                    secu_sexoMujer.IsChecked = true;
                }
                secu_calle.Text = ciu.Calle;
                secu_numero.Text = ciu.Numero;
                secu_piso.Text = ciu.Letra_Piso;
                secu_localidad.Text = ciu.Localidad;
                secu_provincia.Text = ciu.Provincia;
                secu_cp.Text = ciu.CP;
                secu_pais.Text = ciu.Nacionalidad;
            }
            else
            {
                secu_dni.Background = System.Windows.Media.Brushes.LightYellow;

                secu_nombre.Clear();
                secu_apellidos.Clear();
                secu_fecha.Clear();
                secu_sexoHombre.IsChecked = false;
                secu_sexoMujer.IsChecked = false;
                secu_calle.Clear();
                secu_numero.Clear();
                secu_piso.Clear();
                secu_localidad.Clear();
                secu_provincia.Clear();
                secu_cp.Clear();
                secu_pais.Clear();
            }
        }

        private void v_matricula_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (v_matricula.Text.Length == 7)
                {
                    Int32.Parse(v_matricula.Text.Substring(0, 4));
                    string abecedario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    if (!abecedario.Contains(v_matricula.Text.Substring(4,1)) || !abecedario.Contains(v_matricula.Text.Substring(5,1)) || !abecedario.Contains(v_matricula.Text.Substring(6,1)))
                    {
                        throw new FormatException();
                    }
                    v_matricula.Background = System.Windows.Media.Brushes.White;
                }
                else
                {
                    v_matricula.Background = System.Windows.Media.Brushes.LightYellow;
                }

            }
            catch (FormatException)
            {
                v_matricula.Background = System.Windows.Media.Brushes.LightYellow;
            }
        }

        private bool comprobarFormato()
        {
            bool ok = true;

            if (denun_dni.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
            if (hechos_hora.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
            if (hechos_minutos.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
            if (secu_dni.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
            if (desa_dni.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
            if (tipo.Equals("Robo de vehiculo"))
            {
                if (v_matricula.Background.ToString().Equals(System.Windows.Media.Brushes.LightYellow.ToString())) ok = false;
                if (v_bastidor.Text.Length != 17)
                {
                    ok = false;
                    v_bastidor.Background = System.Windows.Media.Brushes.LightYellow;
                }
            }

            return ok;
        }

        private bool comprobarObligatorios()
        {
            bool ok = true;

            if (denun_dni.Text == "")
            {
                ok = false;
                denun_dni.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else denun_dni.Background = System.Windows.Media.Brushes.White;
            if (hechos_hora.Text == "")
            {
                ok = false;
                hechos_hora.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else hechos_hora.Background = System.Windows.Media.Brushes.White;
            if (hechos_minutos.Text == "")
            {
                ok = false;
                hechos_minutos.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else hechos_minutos.Background = System.Windows.Media.Brushes.White;
            if (hechos_descripcion.Text == "")
            {
                ok = false;
                hechos_descripcion.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else hechos_descripcion.Background = System.Windows.Media.Brushes.White;
            if (hechos_direccion.Text == "")
            {
                ok = false;
                hechos_direccion.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else hechos_direccion.Background = System.Windows.Media.Brushes.White;
            if (hechos_fecha.SelectedDate == null)
            {
                ok = false;
                hechos_fecha.Background = System.Windows.Media.Brushes.LightCoral;
            }
            else hechos_fecha.Background = System.Windows.Media.Brushes.White;

            if (tipo.Equals("Robo de vehiculo"))
            {
                if (v_marca.Text == "")
                {
                    ok = false;
                    v_marca.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else v_marca.Background = System.Windows.Media.Brushes.White;
                if (v_modelo.Text == "")
                {
                    ok = false;
                    v_modelo.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else v_modelo.Background = System.Windows.Media.Brushes.White;
                if (v_color.Text == "")
                {
                    ok = false;
                    v_color.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else v_color.Background = System.Windows.Media.Brushes.White;
                if (v_bastidor.Text == "")
                {
                    ok = false;
                    v_bastidor.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else v_bastidor.Background = System.Windows.Media.Brushes.White;
                if (v_matricula.Text == "")
                {
                    ok = false;
                    v_matricula.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else v_matricula.Background = System.Windows.Media.Brushes.White;
            }

            if (tipo.Equals("Secuestro"))
            {
                if (secu_dni.Text == "")
                {
                    ok = false;
                    secu_dni.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else secu_dni.Background = System.Windows.Media.Brushes.White;
            }

            if (tipo.Equals("Desaparicion"))
            {
                if (desa_dni.Text == "")
                {
                    ok = false;
                    desa_dni.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else desa_dni.Background = System.Windows.Media.Brushes.White;
                if (desa_descripcion.Text == "")
                {
                    ok = false;
                    desa_descripcion.Background = System.Windows.Media.Brushes.LightCoral;
                }
                else desa_descripcion.Background = System.Windows.Media.Brushes.White;
                if (imageName == null)
                {
                    ok = false;
                    cuadro_foto.Stroke = System.Windows.Media.Brushes.LightCoral;
                }
            }

            return ok;
        }

        private void hechos_hora_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Int32.Parse(hechos_hora.Text) < 0 || Int32.Parse(hechos_hora.Text) > 23)
                {
                    throw new FormatException();
                }
                hechos_hora.Background = System.Windows.Media.Brushes.White;
            }
            catch (FormatException)
            {
                hechos_hora.Background = System.Windows.Media.Brushes.LightYellow;
            }
        }

        private void hechos_minutos_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Int32.Parse(hechos_minutos.Text) < 0 || Int32.Parse(hechos_minutos.Text) > 59)
                {
                    throw new FormatException();
                }
                hechos_minutos.Background = System.Windows.Media.Brushes.White;
            }
            catch (FormatException)
            {
                hechos_minutos.Background = System.Windows.Media.Brushes.LightYellow;
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
                    cuadro_foto.Visibility = Visibility.Hidden;
                    desa_foto.Source = new BitmapImage(new Uri(file.FileName));

                }
                else
                {
                    MessageBox.Show("Imagen demasiado grande", "Error de tamaño", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una imagen", "Error de tamaño", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void poliDenunciante_Checked(object sender, RoutedEventArgs e)
        {
            denun_dni.Text = user.DNI;
            denun_dni.IsEnabled = false;
        }

        private void poliDenunciante_Unchecked(object sender, RoutedEventArgs e)
        {
            denun_dni.Clear();
            denun_dni.IsEnabled = true;
        }
    }
}
