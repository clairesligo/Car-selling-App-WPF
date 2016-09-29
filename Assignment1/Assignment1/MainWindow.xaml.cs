using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            lbxCars.ItemsSource = Data.AllVehicles; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReadFile("vehicles.txt");
            cbxFilter.Items.Add("-select-");
            cbxFilter.Items.Add("Price");
            cbxFilter.Items.Add("Mileage");
            cbxFilter.Items.Add("Make");
            cbxFilter.SelectedIndex = 0;
        }

        private void ReadFile(string path)
        {
            using (StreamReader File = new StreamReader(path))
            {
                string line;
                string[] info;
                while ((line = File.ReadLine()) != null)
                {
                    info = line.Split(',');
                    VehiclesFromFile(info[0].Trim(), info);
                }
            }
        }

        private static void VehiclesFromFile(string vehicle, string[] info)
        {
            switch (vehicle)
            {
                case "1":
                    Data.AllVehicles.Add(new Car(
                        Convert.ToInt32(info[0].Trim()),
                        info[1].Trim(),//make
                        info[2].Trim(),//model
                        Convert.ToDouble(info[3].Trim()),//price
                        info[4].Trim(),//date
                        info[5].Trim(),//color
                        Convert.ToDouble(info[6].Trim()),//milage
                        info[7].Trim(),//desc
                        info[8].Trim(),//img
                        info[9].Trim(),//imgtype
                        (Car.BodyType)Enum.Parse(typeof(Car.BodyType), info[10].Trim())//car type
                        ));
                    break;

                case "2":
                    Data.AllVehicles.Add(new Van(
                        Convert.ToInt32(info[0].Trim()),
                        info[1].Trim(),//make
                        info[2].Trim(), //model
                        Convert.ToDouble(info[3].Trim()), //price
                        info[4].Trim(), //date
                        info[5].Trim(), //color
                        Convert.ToDouble(info[6].Trim()), //mileage
                        info[7].Trim(), //desc
                        info[8].Trim(), //img
                        info[9].Trim(), //imgt
                        (Van.WheelBase)Enum.Parse(typeof(Van.WheelBase), info [10].Trim()), //wheelbase
                        (Van.VanType)Enum.Parse(typeof(Van.VanType), info[11].Trim()) //van type
                        ));
                    break;
                case "3":
                    Data.AllVehicles.Add(new Bike(
                        Convert.ToInt32(info[0].Trim()),
                        info[1].Trim(),//make
                        info[2].Trim(), //model
                        Convert.ToDouble(info[3].Trim()), //price
                        info[4].Trim(), //date
                        info[5].Trim(), //color
                        Convert.ToDouble(info[6].Trim()), //mileage
                        info[7].Trim(), //desc
                        info[8].Trim(), //img
                        info[9].Trim(), //img
                        (Bike.BikeType)Enum.Parse(typeof(Bike.BikeType), info[10].Trim()) //bikeType
                        ));
                    break;

            }//end switch

        }

        private void lbxCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxCars.SelectedIndex > -1)//updating image on slection in list box
            {
                Vehicle v = lbxCars.SelectedItem as Vehicle;
               imgDesc.Source =  new BitmapImage(new Uri(GetImageDirectory() + v.ReferenceImage, UriKind.Absolute));
                txtDesc.Text = v.desc;
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

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            if (btn.Content.ToString() != null)
                lbxCars.ItemsSource = SortedList(ref btn);
        }

        private ObservableCollection<Vehicle> SortedList(ref RadioButton btn)//radio button filtering
        {
            ObservableCollection<Vehicle> filteredList = new ObservableCollection<Vehicle>();
            
            if (btn.Content.Equals("Cars"))
            {
                foreach (Vehicle v in Data.AllVehicles)
                    if (v is Car)
                        filteredList.Add(v);
            }
            else if (btn.Content.Equals("Vans"))
            {
                foreach (Vehicle v in Data.AllVehicles)
                    if (v is Van)
                        filteredList.Add(v);
            }
            else if (btn.Content.Equals("Bikes"))
            {
                foreach (Vehicle v in Data.AllVehicles)
                {
                    if (v is Bike)
                        filteredList.Add(v);
                }
            }
            else
            {
                foreach (Vehicle v in Data.AllVehicles)
                    filteredList.Add(v);
            }
            return filteredList;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Data.AllVehicles.Clear();
            ReadFile("vehicles.txt");
            MessageBox.Show("All Details Loaded", "Loaded", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string[] database = new string[Data.AllVehicles.Count];
            Vehicle vc;

            for (int i = 0; i < Data.AllVehicles.Count; i++)
            {
                vc = Data.AllVehicles.ElementAt(i);
                database[i] = vc.FileFormat();
            }

            File.WriteAllLines("vehicles.txt", database);
            MessageBox.Show("All Details Saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddVehicle addVehicle = new AddVehicle();
            addVehicle.Owner = this;
            addVehicle.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditVehicle editVehicle = new EditVehicle();
            editVehicle.Owner = this;
            editVehicle.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Data.AllVehicles.Remove(lbxCars.SelectedItem as Vehicle);
        }

        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filtercbx = cbxFilter.SelectedItem.ToString();
            switch (filtercbx)
            {
                case "Price":
                    Data.AllVehicles = new ObservableCollection<Vehicle>(Data.AllVehicles.OrderBy(i => i.Price));
                    lbxCars.ItemsSource = Data.AllVehicles;
                    break;
                case "Mileage":
                    Data.AllVehicles = new ObservableCollection<Vehicle>(Data.AllVehicles.OrderBy(i => i.Mileage));
                    lbxCars.ItemsSource = Data.AllVehicles;
                    break;
                case "Make":
                    Data.AllVehicles = new ObservableCollection<Vehicle>(Data.AllVehicles.OrderBy(i => i.Make));
                    lbxCars.ItemsSource = Data.AllVehicles;
                    break;
            }
        }

        private void btnFinance_Click(object sender, RoutedEventArgs e)
        {
            Finance finance = new Finance();
            finance.Owner = this;
            finance.ShowDialog();
        }
    }//end class
}// end ns
