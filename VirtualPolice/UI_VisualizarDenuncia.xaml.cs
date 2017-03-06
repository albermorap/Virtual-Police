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
    /// Lógica de interacción para UI_VisualizarDenuncia.xaml
    /// </summary>
    public partial class UI_VisualizarDenuncia : Window
    {
        AgenteWS user;
        VPServiceClient cliente;
        DenunciaWS denuncia;
        
        public UI_VisualizarDenuncia(AgenteWS user, DenunciaWS den, bool notificacion)
        {
            InitializeComponent();
            this.user = user;
            this.denuncia = den;
            cliente = new VPServiceClient();

            if (notificacion)
            {
                tipo_denuncia.Content = "NOTIFICACIÓN - " + denuncia.Tipo;
                salir.Visibility = Visibility.Hidden;
                asociar.Visibility = Visibility.Hidden;
                confirmar.Visibility = Visibility.Visible;
                borrar.Visibility = Visibility.Visible;
                volver.Visibility = Visibility.Visible;
            }
            else
            {
                tipo_denuncia.Content = "INFORME DE DENUNCIA - " + denuncia.Tipo;
            }

            denunciaGenerica();

            switch (denuncia.Tipo)
            {
                case "Robo de vehiculo":
                    denunciaRoboVehiculo();
                    break;
                case "Secuestro":
                    denunciaSecuestro();
                    break;
                case "Desaparicion":
                    denunciaDesaparicion();
                    break;
                default:
                    extras.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void denunciaGenerica()
        {
            CiudadanoWS ciu = cliente.GetCiudadano(denuncia.DNI_Denunciante);

            denun_dni.Text = denuncia.DNI_Denunciante;

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

                hechos_descripcion.Text = denuncia.Descripcion_hecho;
                hechos_direccion.Text = denuncia.Direccion_hecho;
                hechos_fecha.Text = denuncia.Fecha_hecho.ToShortDateString();
                hechos_hora.Text = denuncia.Hora_hecho.ToString();
                hechos_naturaleza.Text = denuncia.Naturaleza_lugar_hecho;
            }
            else
            {
                MessageBox.Show("El denunciante no existe en la BD", "Denuncia inválida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void denunciaRoboVehiculo()
        {
            v_matricula.Text = denuncia.RoboCoche.Matricula;
            v_color.Text = denuncia.RoboCoche.Color;
            v_marca.Text = denuncia.RoboCoche.Marca;
            v_modelo.Text = denuncia.RoboCoche.Modelo;
            v_bastidor.Text = denuncia.RoboCoche.NumBastidor;

            vehiculo.Visibility = Visibility.Visible;
        }

        private void denunciaDesaparicion()
        {
            CiudadanoWS ciu = cliente.GetCiudadano(denuncia.Desaparicion.DNI_Desaparecido);

            desa_dni.Text = denuncia.Desaparicion.DNI_Desaparecido;

            if (ciu != null)
            {
                desa_dni.Text = ciu.DNI;
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
                desaparecido.Visibility = Visibility.Visible;
                desa_descripcion.Text = denuncia.Desaparicion.Descripcion_fisica;
                FotoWS image = denuncia.Desaparicion.Foto;
                desa_foto.Source = byteArrayToImage(image.Image, image.Type);
            }
            else
            {
                MessageBox.Show("El ciudadano desaparecido no existe en la BD", "Denuncia inválida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void denunciaSecuestro()
        {
            CiudadanoWS ciu = cliente.GetCiudadano(denuncia.Secuestro.DNI_Ciudadano);

            secu_dni.Text = denuncia.Secuestro.DNI_Ciudadano;

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

                secuestrado.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("El ciudadano secuestrado no existe en la BD", "Denuncia inválida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void salir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow(user);
            w.Show();
            this.Close();
        }

        private void volver_Click(object sender, RoutedEventArgs e)
        {
            bool existen;
            UI_NotificacionesDenuncia w = new UI_NotificacionesDenuncia(user, out existen);
            if (existen)
            {
                w.Show();
            }
            else
            {
                MainWindow mw = new MainWindow(user);
                mw.Show();
            }
            this.Close();
        }

        private void confirmar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("¿Está seguro de validar la notificación?\n", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
            {
                UI_CrearDenuncia w = new UI_CrearDenuncia(user, denuncia);
                w.Show();
                this.Close();
            }
        }

        private void borrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("¿Está seguro de eliminar la notificación?\n", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
            {
                if (cliente.DeleteDenuncia(denuncia.Id))
                {
                    MessageBox.Show("Notificación borrada con éxito", "Borrado correcto", MessageBoxButton.OK, MessageBoxImage.Information);
                    volver_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error durante el borrado de la notificación", "Borrado fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void asociar_Click(object sender, RoutedEventArgs e)
        {
            if (denuncia.DNI_expediente == null)
            {

                inputBox ib = new inputBox("Introduzca el DNI del culpable:", 9);
                ib.ShowDialog();
                String expediente = inputBox.devolver();

                if (cliente.GetCiudadano(expediente) != null)
                {
                    ExpedienteWS exp = cliente.GetExpediente(expediente);

                    if (exp == null)
                    {
                        UI_NuevoExpediente w = new UI_NuevoExpediente(denuncia, expediente, user);
                        w.ShowDialog();
                    }
                    else
                    {
                        UI_Expediente_Existente w = new UI_Expediente_Existente(denuncia, exp, user);
                        w.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No existe un ciudadano con DNI " + expediente, "DNI no existente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("La denuncia ya está vinculada al expediente " + denuncia.DNI_expediente, "DNI no existente", MessageBoxButton.OK, MessageBoxImage.Warning);
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
