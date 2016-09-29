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

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for EditVehicle.xaml
    /// </summary>
    public partial class EditVehicle : Window
    {
        public EditVehicle()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            cbxVehicleType.Items.Add("Car");
            cbxVehicleType.Items.Add("Van");
            cbxVehicleType.Items.Add("Motorbike");
            try
            {
                Vehicle v = main.lbxCars.SelectedItem as Vehicle;
                int type = v.indentifier;


                switch (type)
                {
                    case 1:
                        Car c = main.lbxCars.SelectedItem as Car;
                        cbxWheelBase.IsEnabled = false;
                        cbxBodyType.ItemsSource = Enum.GetNames(typeof(Car.BodyType));
                        cbxBodyType.SelectedItem = c.bodyType.ToString();
                        cbxVehicleType.SelectedItem = "Car";
                        break;
                    case 2:
                        Van van = main.lbxCars.SelectedItem as Van;
                        cbxWheelBase.IsEnabled = true;
                        cbxWheelBase.ItemsSource = Enum.GetNames(typeof(Van.WheelBase));
                        cbxBodyType.ItemsSource = Enum.GetNames(typeof(Van.VanType));
                        cbxBodyType.SelectedValue = van.vanType.ToString();
                        cbxWheelBase.SelectedValue = van.wheelBase.ToString();
                        cbxVehicleType.SelectedItem = "Van";
                        break;
                    case 3:
                        Bike b = main.lbxCars.SelectedItem as Bike;
                        cbxWheelBase.IsEnabled = false;
                        cbxBodyType.ItemsSource = Enum.GetNames(typeof(Bike.BikeType));
                        cbxBodyType.SelectedValue = b.bikeType.ToString();
                        cbxVehicleType.SelectedItem = "Motorbike";
                        break;
                }

                txtMake.Text = v.Make;
                txtModel.Text = v.Model;
                txtPrice.Text = v.Price.ToString();
                txtYear.Text = v.Date;
                txtColor.Text = v.Colour;
                txtMileage.Text = v.Mileage.ToString();
                txtDesc.Text = v.Description;
                txtImage.Text = v.ReferenceImage;
            }
            catch
            {
                MessageBox.Show("Please select a vehicle from the list", "OK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }
        }

        private void cbxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            txtMake.Clear();
            txtModel.Clear();
            txtPrice.Clear();
            txtYear.Clear();
            txtColor.Clear();
            txtMileage.Clear();
            txtDesc.Clear();
            txtImage.Clear();

            string selItem = cbxVehicleType.SelectedItem.ToString();
            switch (selItem)
            {
                case "Car":
                    cbxWheelBase.Text = null;
                    cbxWheelBase.IsEnabled = false;
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Car.BodyType));                  
                    break;
                case "Van":
                    cbxWheelBase.IsEnabled = true;
                    cbxWheelBase.ItemsSource = Enum.GetNames(typeof(Van.WheelBase));
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Van.VanType));
                    break;
                case "MotorBike":
                    cbxWheelBase.Text = null;
                    cbxWheelBase.IsEnabled = false;
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Bike.BikeType));                    
                    break;

            }//end switch
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            Data.AllVehicles.Remove(main.lbxCars.SelectedItem as Vehicle);

            string selItem = cbxVehicleType.SelectedItem.ToString();
            switch (selItem)
            {
                case "Car":
                    Data.AllVehicles.Add(new Car(
                        1,
                        txtMake.Text,//make
                        txtModel.Text,//model
                        Convert.ToDouble(txtPrice.Text),//price
                        txtYear.Text,//date
                        txtColor.Text,//color
                        Convert.ToDouble(txtMileage.Text),//milage
                        txtDesc.Text,//desc
                        txtImage.Text,//img
                        "/images/cars.png",//imgtype
                        (Car.BodyType)Enum.Parse(typeof(Car.BodyType), cbxBodyType.Text)//car type
                        ));
                    break;

                case "Van":
                    Data.AllVehicles.Add(new Van(
                        2,
                         txtMake.Text,//make
                         txtModel.Text,//model
                         Convert.ToDouble(txtPrice.Text),//price
                         txtYear.Text,//date
                         txtColor.Text,//color
                         Convert.ToDouble(txtMileage.Text),//milage
                         txtDesc.Text,//desc
                         txtImage.Text,//img
                         "/images/van.png",//imgtype
                         (Van.WheelBase)Enum.Parse(typeof(Van.WheelBase), cbxWheelBase.Text), //wheelbase
                         (Van.VanType)Enum.Parse(typeof(Van.VanType), cbxBodyType.Text) //van type
                         ));
                    break;
                case "MotorBike":
                    Data.AllVehicles.Add(new Bike(
                        3,
                        txtMake.Text,//make
                        txtModel.Text,//model
                        Convert.ToDouble(txtPrice.Text),//price
                        txtYear.Text,//date
                        txtColor.Text,//color
                        Convert.ToDouble(txtMileage.Text),//milage
                        txtDesc.Text,//desc
                        txtImage.Text,//img
                        "/images/motorcycle.png",//imgtype
                        (Bike.BikeType)Enum.Parse(typeof(Bike.BikeType), cbxBodyType.Text) //bikeType
                        ));

                    break;

            }//end switch
            Close();
        }

        private void btnSelImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Images (*.JPG;*.JPEG;*.PNG) | *.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;

                string imageDirectory = GetImageDirectory();

                string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1);

                string newFile = imageDirectory + shortFileName;

                if (!File.Exists(newFile))
                    File.Copy(filename, newFile);

                else
                    MessageBox.Show("This image already exists", "Done", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                txtImage.Text = shortFileName;
            }
        }

        private string GetImageDirectory()
        {
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo parent = Directory.GetParent(currentDir);
            DirectoryInfo grandParent = Directory.GetParent(parent.FullName);
            string imageDirectory = grandParent + "\\images\\";

            return imageDirectory;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
