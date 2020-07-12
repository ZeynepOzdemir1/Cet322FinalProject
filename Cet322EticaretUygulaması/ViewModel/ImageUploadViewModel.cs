using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet322EticaretUygulaması.ViewModel
{
    public class ImageUploadViewModel
    {
        public int PlantId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
