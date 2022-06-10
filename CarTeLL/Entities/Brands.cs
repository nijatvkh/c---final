using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTeLL.Entities
{
    [Serializable]
    internal class Brands
    {
        private static int counter = 0;
        public Brands()
        {
            this.Id = ++counter;
        }
        static public void SetCounter(int counter)
        {
            Brands.counter = counter;
        }
        public int Id { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return $"{Id} {Name}";
        }


    }
}
