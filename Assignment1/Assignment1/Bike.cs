using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Bike : Vehicle
    {

        public enum BikeType { Scooter, TrailBike, Sports, Commuter, Tourer, Unlisted };

        public BikeType bikeType { get; set; }

        public Bike() { }
        public Bike(int ident,string make, string model, double price, string date, string color, double milage, string description, string img, string imgt, BikeType btype)
        {
            indentifier = 3;
            Make = make;
            Model = model;
            Price = price;
            Date = date;
            Colour = color;
            Mileage = milage;
            Description = description;
            ReferenceImage = img;
            imageType = "/images/motorcycle.png";
            bikeType = btype;
            
        }

        public override string desc
        {
            get
            {
                return base.desc + string.Format("\nBikeType:\t{0}\n", bikeType);
            }
        }
        public override string FileFormat()
        {
            return string.Format("3,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",  Make, Model, Price, Date, Colour, Mileage, Description, ReferenceImage,imageType, bikeType);
        }

    }//end class
}

