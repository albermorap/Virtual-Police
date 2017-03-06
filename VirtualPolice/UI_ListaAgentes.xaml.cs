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
    /// Lógica de interacción para UI_ListaAgentes.xaml
    /// </summary>
    public partial class UI_ListaAgentes : Window
    {
        AgenteWS user;
        AgenteWS[] arrayAgentes;
        VPServiceClient cliente;

        public UI_ListaAgentes(AgenteWS user)
        {
            InitializeComponent();
            cliente = new VPServiceClient();
            this.user = user;
            Actualizar_Lista();
        }

        private void Button_Seleccionar_Click(object sender, RoutedEventArgs e) //seleccionar
        {
            AgenteWS agente = cliente.GetAgente(ListBox1.SelectedItem.ToString());
            UI_ConsultarPlanning consultaPlanning = new UI_ConsultarPlanning(user, agente);
            consultaPlanning.ShowDialog();
        }

        private void Button_Cancelar_Click(object sender, RoutedEventArgs e) // cancelar
        {
            MessageBoxResult r = MessageBox.Show("¿Quieres cerrar la lista?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (r == MessageBoxResult.Yes)
                this.Close();
        }

        private void Button_Actualizar_Click(object sender, RoutedEventArgs e) //actualizar
        {
            Actualizar_Lista();
        }

        public void Actualizar_Lista(){
                
            ListBox1.Items.Clear();

            arrayAgentes = cliente.GetAgentes();

            if (arrayAgentes == null)
            {
                MessageBox.Show("No existe ningún agente en la BD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (AgenteWS ag in arrayAgentes)
                {
                    ListBox1.Items.Add(ag.NumPlaca);
                } 
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow(user);
            mw.Show();
        }
    }
}
