using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.Models
{
    public class PlantModel
    {

        public int Id { get; set; }

        [Display(Name = "Bitki Adı")]
        public string PlantName { get; set; }

        [Display(Name = "Tanımı")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Description { get; set; }

        [Display(Name = "Fiyatı")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public Decimal Price { get; set; }
        //public bool IsSmall { get; set; }
        public string CreationDateTime { get; set; }

        public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]

        [Display(Name = "Kategori Adı")]

        public CategoryModel Category { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }


        //[Display(Name = "Yorum Listesi")]
        public virtual List<CommentModel> Comments { get; set; }
        public PlantModel()
        {
            CreatedDate = DateTime.Now;
        }
        public virtual List<PlantImageModel> PlantImages { get; set; }




    }
}
