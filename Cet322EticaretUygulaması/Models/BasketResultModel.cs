using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class BasketResultModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int PlantId { get; set; }
        public PlantModel Plant { get; set; }
        public string CustomerName { get; set; }
        public string PlantName { get; set; }
        public int PlantPrice { get; set; }
        public int Quantity { get; set; }



    }
}
