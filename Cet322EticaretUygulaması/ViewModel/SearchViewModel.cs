using Cet322EticaretUygulaması.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.ViewModel
{
    public class SearchViewModel
    {
        [Display(Name = "Arama Metni")]
        public string SearchText { get; set; }
     
        [Display(Name="Açıklamalarda Ara")]
        public bool SearchInDescription { get; set; }
        [Display(Name = "Kategoriler")]
        public int? CategoryId { get; set; }
        public List<Models.PlantModel> Results { get; set; }
    }
}
