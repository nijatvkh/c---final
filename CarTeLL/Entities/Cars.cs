using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTeLL.Entities
{
    [Serializable]
    internal class Cars
    {
        private static int counter = 0;

        public Cars()
        {
            this.Id = ++counter;
        }
        static public void SetCounter(int counter)
        {
            Cars.counter = counter;
        }

        public int Id { get; set; }
        public int Brand { get; set; }
        public int Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }
        public double Engine { get; set; }
        public int HP { get; set; }
        public string Fuel { get; set; }
        public double Mileage { get; set; }
        public string Gearbox { get; set; }
        public string AllWheelDrive {get; set; }
        public bool New { get; set; }
        public double Price { get; set; }


      

        public override string ToString()
        {
            return $"{Id}. | {Brand} {Model}  | {Year} | {VIN} | {Color} | {Engine} | {HP} | {Fuel} | {Mileage} | {Gearbox} | {AllWheelDrive} | {New} | {Price}";
        }
    }
}
