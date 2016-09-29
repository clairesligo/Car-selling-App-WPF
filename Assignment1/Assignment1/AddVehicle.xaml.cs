using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        public AddVehicle()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            cbxVehicleType.Items.Add("Car");
            cbxVehicleType.Items.Add("Van");
            cbxVehicleType.Items.Add("Motorbike");
            cbxVehicleType.SelectedIndex = -1;
        }

        private void cbxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;

            string selItem = cbxVehicleType.SelectedItem.ToString();
            switch (selItem)
            {
                case "Car":
                    cbxWheelBase.IsEnabled = false;
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Car.BodyType));// populate combobox
                    break;
                case "Van":
                    cbxWheelBase.IsEnabled = true;
                    cbxWheelBase.ItemsSource = Enum.GetNames(typeof(Van.WheelBase));
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Van.VanType));
                    break;
                case "MotorBike":
                    cbxWheelBase.IsEnabled = false;
                    cbxBodyType.ItemsSource = Enum.GetNames(typeof(Bike.BikeType));
                    break;

            }//end switch
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            if (cbxVehicleType.SelectedIndex != -1)
            {
                string selItem = cbxVehicleType.SelectedItem.ToString();
                switch (selItem)// create new objects
                {
                    case "Car":
                        Data.AllVehicles.Add(new Car(
                           1,//identifier
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
                            2,//identifier
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
                            3,//identifier
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
            else
            {
                MessageBox.Show("Please select a type of vehicle ", "OK", MessageBoxButton.OK, MessageBoxImage.Asterisk);//error catching
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
