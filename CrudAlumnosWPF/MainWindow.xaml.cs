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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrudAlumnosWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int indice = -1;
        ClassCrud crud = new ClassCrud();
        public MainWindow()
        {
            InitializeComponent();
            BtnModificar.IsEnabled = false;
            BtnBorrar.IsEnabled = false;
            crud.AddEstudiante(new Alumnos("Juan Pez", "JP@gmail.com",22));
            crud.AddEstudiante(new Alumnos("Ana Lopez", "AL@gmail.com", 25));
            crud.AddEstudiante(new Alumnos("Miguel Santos", "MS@gmail.com", 25));
            dgvDatos.ItemsSource = null;
            dgvDatos.ItemsSource = crud.ListarAlumnos();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Validadores de Datos 
            if (Validardatos() == false)
            {
                return;
            }
            if (ValidarCorreo() == false)
            {
                return;
            }
            if (ValidarEdad() == false)
            {
                return;
            }
            //Comenzr a guardar datos
            crud.AddEstudiante(new Alumnos(TxtNombre.Text, TxtCorreo.Text, int.Parse(TxtEdad.Text)));
            LimpiarCajas();
        }
        //Metodo para evitar datos vacios
        private bool ValidarCorreo()
        {
            if (string.IsNullOrEmpty(TxtCorreo.Text))
            {
                MessageBox.Show("No pueden existir datos en blanxo.");
                TxtCorreo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidarEdad()
        {
            int Edad;
            if (!int.TryParse(TxtEdad.Text, out Edad) || TxtEdad.Text == "")
            {
                MessageBox.Show("Solo se permiten datos numericos");
                TxtEdad.Clear();
                TxtEdad.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool Validardatos()
        {
            if (string.IsNullOrEmpty(TxtNombre.Text))
            {
                MessageBox.Show("No pueden existir datos en blanco.");
                TxtNombre.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        //Limpiar las cajas de texto
        private void LimpiarCajas()
        {
            TxtEdad.Clear();
            TxtCorreo.Clear();
            TxtNombre.Clear();
            TxtNombre.Focus();
            dgvDatos.ItemsSource = null;
            dgvDatos.ItemsSource = crud.ListarAlumnos();
            indice = -1;
           


        }
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos actualizados de las cajas de texto
            int edad = int.Parse(TxtEdad.Text);
            string nombre = TxtNombre.Text;
            string correo = TxtCorreo.Text;
            indice = dgvDatos.SelectedIndex;
            // Buscar el objeto correspondiente en la lista y actualizar sus propiedades
            crud.Actualizar(indice, nombre, edad, correo);
            //Indicamos que se realizo la tarea y desactivamos botones
            MessageBox.Show("Registro actualizado correctamente.");
            LimpiarCajas();
            BtnModificar.IsEnabled = false;
            BtnAgregar.IsEnabled = true;
            BtnBorrar.IsEnabled = false;
        }
        private void dgvDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Alumnos filaSeleccionada = (Alumnos)dgvDatos.SelectedItem;
            if (dgvDatos.SelectedIndex >= 0)
            {
                TxtNombre.Text = filaSeleccionada.Nombre;
                TxtCorreo.Text = filaSeleccionada.Email;
                TxtEdad.Text = filaSeleccionada.Edad.ToString();
                //Activar la opción de borrar
                BtnBorrar.IsEnabled = true;
                BtnModificar.IsEnabled = true;
                BtnAgregar.IsEnabled = false;
            }
            else
            {
               crud.ListarAlumnos();
            }
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            indice = dgvDatos.SelectedIndex;
            crud.Borrar(indice);
            MessageBox.Show("Se elimino el registros");
            LimpiarCajas();
            BtnModificar.IsEnabled = false;
            BtnAgregar.IsEnabled = true;
            BtnBorrar.IsEnabled = false;
        }
    }
}
