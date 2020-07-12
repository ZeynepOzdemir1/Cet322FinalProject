using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class PlantImageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public bool IsDefaultImage { get; set; }
        public virtual PlantModel Plant { get; set; }


    }
}
