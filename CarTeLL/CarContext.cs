using System;
using CarTeLL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTeLL
{
    [Serializable]
    internal class CarContext
    {
       public GenericStore<Brands> Brands { get; set; }
       public GenericStore<Models> Models { get; set; }
       public GenericStore<Cars> Cars { get; set; }
    }


}
