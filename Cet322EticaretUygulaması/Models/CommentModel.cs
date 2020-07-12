using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
        public PlantModel Plant { get; set; }
        public int? Rating { get; set; }
        public int PlantStoreUserId { get; set; }
        public virtual CustomerModel PlantStoreUser { get; set; }
    }
}