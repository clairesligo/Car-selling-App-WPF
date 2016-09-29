using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Van : Vehicle
    {
        
            public enum WheelBase { Short, Medium, Long, Unlisted };

            public WheelBase wheelBase { get; set; }

            public enum VanType { CombiVan, Dropside, PanelVan, Pickup, Tipper, Unlisted };
            public VanType vanType { get; set; }

            public Van() { }
        public Van(int ident,string make, string model, double price, string date, string color, double milage, string description, string img, string imgt, WheelBase wheel, VanType body)
        {
            indentifier = 2;
            Make = make;
            Model = model;
            Price = price;
            Date = date;
            Colour = color;
            Mileage = milage;
            Description = description;
            ReferenceImage = img;
            imageType = "/images/van.png";
            wheelBase = wheel;
            vanType = body;
        }
        public override string desc
            {
                get
                {
                    return base.desc + string.Format("\nWheel Base:\t{0}\nVan Type: \t{1}\n", wheelBase, vanType);
                }
            }
        public override string FileFormat()
        {
            return string.Format("2,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Make, Model, Price, Date, Colour, Mileage, Description, ReferenceImage,imageType, wheelBase,vanType);
        }

    }//end class
    }

