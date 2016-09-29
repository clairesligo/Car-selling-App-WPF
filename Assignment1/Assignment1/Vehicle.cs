using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public abstract class Vehicle
    {
        public int indentifier { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string Colour { get; set; }
        public double Mileage { get; set; }
        public string Description { get; set; }
        public string imageType { get; set; }

        private string _protectedImage = "/images/car.jpg";
        public string ReferenceImage { get; set; }
        

        public Vehicle(){}
        public Vehicle(string make, string model, double price, string date, string color, double milage, string description, string img,string imgt)
        {
            Make = make;
            Model = model;
            Price = price;
            Date = date;
            Colour = color;
            Mileage = milage;
            Description = description;
            _protectedImage = img;
            imageType = imgt;
        }
        public Vehicle(string make)
        {
            Make = make;
        }
        public Vehicle(string make, string model)
        {
            Make = make;
            Model = model;
        }

        public virtual string desc
        {
            get
            {
                return String.Format("Make: \t\t{0}\nModel: \t\t{1}\nPrice: \t\t{2}\nYear: \t\t{3}\nMilage: \t\t{4}\nDescription: \t{5}", Make,Model,Price,Date,Mileage,Description );
            }
        }

        public override string ToString()
        {
            return string.Format("Make: {0}\tModel: {1}\nPrice: {2}\tMilage: \t{3}",Make,Model,Price,Mileage);
        }
        public virtual string FileFormat()
        {
            return string.Format("1,{0},{1},{2},{3},{4},{5},{6},{7},", Make, Model, Price, Date,Colour, Mileage, Description,ReferenceImage);
        }
    }//end class
}//end NS
