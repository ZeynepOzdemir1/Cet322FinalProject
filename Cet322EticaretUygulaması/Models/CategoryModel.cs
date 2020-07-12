using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Başlık Alanı 2 ile 10 karakter arasında olmalıdır")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "Bitki Kategorisi")]
        public string Name { get; set; } 
       
        [Display(Name = "Bitki Listesi")]
        public virtual List<PlantModel> Plants { get; set; }





    }
}
