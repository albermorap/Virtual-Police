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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace VirtualPolice
{
    /// <summary>
    /// Lógica de interacción para UI_AltaAgente.xaml
    /// </summary>
    public partial class UI_AltaAgente : Window
    {
        AgenteWS user;
        VPServiceClient db;
        int cont = 0;
         public UI_AltaAgente(AgenteWS user) 
        {
            InitializeComponent();
            db = new VPServiceClient();
            this.user = user;
        }

        private void bt_Comprobar_Click(object sender, RoutedEventArgs e)
       {
           
           if (tb_DNI.Text.Length == 9)
           {
               try
               {
                   string abecedario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                   int x = Int32.Parse(tb_DNI.Text.Substring(0, 8));
                   if(abecedario.Contains(tb_DNI.Text.Substring(8)))
                   {
                       CiudadanoWS c = db.GetCiudadano(tb_DNI.Text);

                       if (c == null)
                       {
                           MessageBox.Show("El DNI del ciudadano no existe");
                           tb_DNI.Text = "";
                       }

                       else
                       {
                           tb_nombre.Text = c.Nombre;
                           tb_nombre.IsEnabled = false;
                           tb_apellidos.Text = c.Apellido1 + " " + c.Apellido2;
                           tb_apellidos.IsEnabled = false;
                           tb_fechaNacimiento.Text = c.Fecha_Nacimiento.ToShortDateString();
                           tb_fechaNacimiento.IsEnabled = false;
                           if (c.Sexo == "H")
                           {
                               cb_sexoHombre.IsChecked = true;
                           }
                           else if (c.Sexo == "M")
                           {
                               cb_sexoMujer.IsChecked = true;
                           }
                           cb_sexoHombre.IsEnabled = false;
                           cb_sexoMujer.IsEnabled = false;
                           tb_calle.Text = c.Calle;
                           tb_calle.IsEnabled = false;
                           tb_numero.Text = c.Numero;
                           tb_numero.IsEnabled = false;
                           tb_piso.Text = c.Letra_Piso;
                           tb_piso.IsEnabled = false;
                           tb_localidad.Text = c.Localidad;
                           tb_localidad.IsEnabled = false;
                           tb_provincia.Text = c.Provincia;
                           tb_provincia.IsEnabled = false;
                           tb_cp.Text = c.CP;
                           tb_cp.IsEnabled = false;
                           tb_pais.Text = c.Nacionalidad;
                           tb_pais.IsEnabled = false;
                           
                       }
                   }
                   else
                   {
                       MessageBox.Show("Introduce bien el DNI");
                       tb_DNI.Text = "";
                   }
               }
               catch (FormatException)
               {
                   MessageBox.Show("Introduce bien el DNI");
                   tb_DNI.Text = "";
               }
           }
           else
           {
               MessageBox.Show("Introduce bien el DNI");
               tb_DNI.Text = "";
           }
               
        }

        private void confirmarAltaAgente_Click(object sender, RoutedEventArgs e)
        {
            bool tlfOk = false;
            String sexo = "";
            String rango = "";
            String ruta = "";
            ruta = tb_DNI.Text;
            String mas = "+";
            
            if (tb_telefono.Text.Length > 0)
            {
                String primero = tb_telefono.Text.Substring(0, 1);
                if (tb_telefono.Text.Length == 14 && primero == mas)
                {
                    try
                    {
                        Int64 x = Int64.Parse(tb_telefono.Text.Substring(1, 13));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Introduzca el telefono correctamente");
                        tb_telefono.Text = "";
                        tlfOk = true;
                    }

                }
                else
                {
                    MessageBox.Show("Introduzca el telefono correctamente");
                    tb_telefono.Text = "";
                    tlfOk = true;
                }
            }

                if (cb_sexoHombre.IsChecked == true)
                {
                    sexo = "Masculino";
                }
                else if (cb_sexoMujer.IsChecked == true)
                {
                    sexo = "Femenino";
                }
                if (rb_rangoAgente.IsChecked == true)
                {
                    rango = "Policia";
                }
                else if (rb_rangoComisario.IsChecked == true)
                {
                    rango = "Comisario";
                }
          
            if ((tb_numPlaca.Text.Length == 0) || (tb_email.Text.Length == 0) || tlfOk || (rb_rangoAgente.IsChecked == false && rb_rangoComisario.IsChecked == false) || (db.GetAgente(tb_numPlaca.Text) != null) || (!db.ComprobarCiuAgente(tb_numPlaca.Text, tb_DNI.Text)))
            {
                
                if ((tb_numPlaca.Text.Length == 0) || (tb_email.Text.Length == 0) || (rb_rangoAgente.IsChecked == false && rb_rangoComisario.IsChecked == false))
                {
                    MessageBox.Show("Rellene los campos obligatorios");
                }
                
                if ((db.GetAgente(tb_numPlaca.Text) != null) || (!db.ComprobarCiuAgente(tb_numPlaca.Text, tb_DNI.Text)))
                {
                    MessageBox.Show("El agente ya existe en la base de datos");
                    tb_DNI.Text = "";
                    tb_DNI.IsEnabled = true;
                    tb_nombre.Text = "";
                    tb_nombre.IsEnabled = true;
                    tb_apellidos.Text = "";
                    tb_apellidos.IsEnabled = true;
                    tb_fechaNacimiento.Text = "";
                    tb_fechaNacimiento.IsEnabled = true;
                    if (cb_sexoHombre.IsChecked == true)
                    {
                        cb_sexoHombre.IsChecked = false;
                    }
                    else if (cb_sexoMujer.IsChecked == true)
                    {
                        cb_sexoMujer.IsChecked = false;
                    }
                    cb_sexoHombre.IsEnabled = true;
                    cb_sexoMujer.IsEnabled = true;
                    tb_calle.Text = "";
                    tb_calle.IsEnabled = true;
                    tb_numero.Text = "";
                    tb_numero.IsEnabled = true;
                    tb_piso.Text = "";
                    tb_piso.IsEnabled = true;
                    tb_localidad.Text = "";
                    tb_localidad.IsEnabled = true;
                    tb_provincia.Text = "";
                    tb_provincia.IsEnabled = true;
                    tb_cp.Text = "";
                    tb_cp.IsEnabled = true;
                    tb_pais.Text = "";
                    tb_pais.IsEnabled = true;
                    tb_numPlaca.Text = "";
                    tb_numPlaca.IsEnabled = true;
                    tb_email.Text = "";
                    tb_email.IsEnabled = true;
                    if (rb_rangoAgente.IsChecked == true)
                    {
                        rb_rangoAgente.IsChecked = false;
                    }
                    else if (rb_rangoComisario.IsChecked == true)
                    {
                        rb_rangoComisario.IsChecked = false;
                    }
                    rb_rangoAgente.IsEnabled = true;
                    rb_rangoComisario.IsEnabled = true;
                    tb_telefono.Text = "";
                    tb_telefono.IsEnabled = true;
                    comboB_estadoCivil.SelectedItem = null;
                    comboB_estadoCivil.IsEnabled = true;
                    tb_observaciones.Text = "";
                    tb_observaciones.IsEnabled = true;
                }
            }
            
            else 
            {
                AgenteWS a = new AgenteWS
                {
                    DNI = tb_DNI.Text,
                    NumPlaca = tb_numPlaca.Text,
                    Email = tb_email.Text,
                    Telefono = tb_telefono.Text,
                    Estado_Civil = comboB_estadoCivil.SelectionBoxItem.ToString(),
                    Observaciones = tb_observaciones.Text
                };
                if (rb_rangoAgente.IsChecked == true)
                {
                    a.Rango = "Policia";
                }
                if (rb_rangoComisario.IsChecked == true)
                {
                    a.Rango = "Comisario";
                }

                
                inputBox ib = new inputBox("Introduzca su contraseña:", 6);
                ib.ShowDialog();
                String password = inputBox.devolver(); 

                if (cont == 3)
                {
                    
                    
                }
                else
                {
                    if (db.ComprobarPassword(user.NumPlaca, password))
                    {
                        cont = 0;
                        if (db.AddAgente(a))
                        {
                            MessageBox.Show("El agente ha sido dado de alta");
                            MessageBoxResult me = MessageBox.Show("¿Desea crear un fichero PDF?", "Crear PDF", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (me == MessageBoxResult.Yes)
                            {


                                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Alta_" + ruta + ".pdf", FileMode.Create));
                                doc.Open();
                                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("../../Images/FondoPDF.jpg");
                                jpg.Alignment = iTextSharp.text.Image.UNDERLYING;
                                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph("           ALTA AGENTE \n\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22));
                                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(" Nombre: " + tb_nombre.Text +
                                "\n Apellidos: " + tb_apellidos.Text +
                                "\n DNI: " + tb_DNI.Text +
                                "\n Fecha de nacimiento: " + tb_fechaNacimiento.Text +
                                "\n Sexo: " + sexo +
                                "\n\n\n Calle: " + tb_calle.Text +
                                "\n Numero: " + tb_numero.Text +
                                "\n Piso: " + tb_piso.Text +
                                "\n Localidad: " + tb_localidad.Text +
                                "\n Provincia: " + tb_provincia.Text +
                                "\n CP: " + tb_cp.Text +
                                "\n Pais: " + tb_pais.Text +
                                "\n\n\n Numero de Placa : " + tb_numPlaca.Text +
                                "\n Email: " + tb_email.Text +
                                "\n Rango: " + rango +
                                "\n Telefono: " + tb_telefono.Text +
                                "\n Estado Civil: " + comboB_estadoCivil.SelectionBoxItem.ToString() +
                                "\n Observaciones: " + tb_observaciones.Text +
                                "\n\n\n Fecha de creacion: " + DateTime.Now);
                                doc.Add(jpg);
                                doc.Add(p);
                                doc.Add(paragraph);
                                doc.Close();
                                this.Close();
                                string s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                                MessageBox.Show("El fichero se genero en: " + s);
                                Process.Start("Alta_" + tb_DNI.Text + ".pdf");


                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error al crear el agente");
                        }
                    }
                    else
                    {
                        cont++;
                    }
                    if (cont == 3)
                    {
                        MessageBox.Show("Ha superado el numero de intentos para introducir la contraseña");
                        this.Close();
                    }
                }
            }
        }

        

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MainWindow formMain = new MainWindow(user);
            formMain.Show();
        }

        private void cancelarAltaAgente_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea salir?", "Confirmación", MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

    }
}
