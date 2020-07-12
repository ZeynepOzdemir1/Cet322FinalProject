using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class CustomerModel : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }

        public string AvatarFileName { get; set; }

    }
    public class PlantStoreRole : IdentityRole<int>
    {
        public bool CanEnterComment { get; set; }
        public bool CanDeleteComment { get; set; }

    }


}
