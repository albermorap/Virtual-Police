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
    /// Lógica de interacción para UI_ConsultarPlanning.xaml
    /// </summary>
    public partial class UI_ConsultarPlanning : Window
    {

        AgenteWS user, agente;
        VPServiceClient cliente;

        public UI_ConsultarPlanning(AgenteWS user, AgenteWS agente)
        {

            InitializeComponent();
            cliente = new VPServiceClient();
            this.user = user;

            if (agente.Equals(null)) {
                this.Close();
            }
            else{
                this.agente = agente;

                lb_dni.Content = agente.DNI;
                lb_nombre.Content = agente.Nombre + " " + agente.Apellido1 + " " + agente.Apellido2;
            }
        }
       

        private void MonthlyCalendar_SelectedDatesChanged(object sender,SelectionChangedEventArgs e)
        {
            Actualizar_Tabla(MonthlyCalendar.SelectedDate.Value);
        }
       

        private void Actualizar_Tabla(DateTime diaSeleccionado)
        {
            if (diaSeleccionado != null)
            {
                ListView1.Items.Clear();

                ActividadWS[] actividades = cliente.GetPlanning(agente.NumPlaca, diaSeleccionado, 7);

                if (actividades == null)
                {
                    MessageBox.Show(agente.Nombre+" "+agente.Apellido1+""+agente.Apellido2+" no tiene actividades programadas para el dia seleccionado y los próximos 6 dias.",
                        "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    foreach (ActividadWS actividad in actividades)
                    {
                        ListView1.Items.Add(new
                        {
                            Dia = actividad.Fecha.ToShortDateString(),
                            Horas = actividad.Hora_Inicio + " - " + actividad.Hora_Fin,
                            Tipo = actividad.Tipo,
                            Descripcion = actividad.Descripcion
                        });
                    }
                }
                /*
                try
                {
                    foreach (ActividadWS actividad in actividades)
                    {
                        ListView1.Items.Add(new { Dia = actividad.Fecha.ToShortDateString(),
                                                  Horas = actividad.Hora_Inicio + " - " + actividad.Hora_Fin,
                                                  Tipo = actividad.Tipo, 
                                                  Descripcion=actividad.Descripcion });
                    }
                }
                catch (NullReferenceException)
                {
                    
                }*/

            }

        }

        private void Button_Imprimir_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Imprimiendo...", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (user.Rango != "Comisario")
            {
                MainWindow mw = new MainWindow(user);
                mw.Show();
            }
        }
    }
}
