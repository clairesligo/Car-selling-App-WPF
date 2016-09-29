using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Car : Vehicle
    {
        public enum BodyType { Convertible, Hatchback, Coupe, Estate, MPV, SUV, Saloon, Unlisted };
         
        public BodyType bodyType {get; set; }
       

        public Car(){}
        public Car(int indent,string make, string model, double price, string date, string color, double milage, string description, string img, string imgt, BodyType body)
        {
            indentifier = 1;
            Make = make;
            Model = model;
            Price = price;
            Date = date;
            Colour = color;
            Mileage = milage;
            Description = description;
            ReferenceImage = img;
            imageType = "/images/cars.png";
            bodyType = body;
            
        }

        public override string desc
        {
            get
            {
                return base.desc + string.Format("\nBodyType:\t{0}\n", bodyType);
            }
        }
        public override string FileFormat()
        {
            return string.Format("1,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",  Make, Model, Price, Date, Colour, Mileage, Description, ReferenceImage,imageType, bodyType);
        }

    }//end class
}//end ns
