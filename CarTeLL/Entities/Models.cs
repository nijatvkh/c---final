using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTeLL.Entities
{
    [Serializable]
    internal class Models
    {
        private static int counter = 0;

        public Models()
        {
            this.Id = ++counter;
        }
        static public void SetCounter(int counter)
        {
            Models.counter = counter;
        }

        public int Id { get; set; }
        public int Brand { get; set; }
        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
