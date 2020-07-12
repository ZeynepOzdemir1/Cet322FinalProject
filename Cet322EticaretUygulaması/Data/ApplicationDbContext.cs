using System;
using System.Collections.Generic;
using System.Text;
using Cet322EticaretUygulaması.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cet322EticaretUygulaması.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
      

        public DbSet<PlantImageModel> PlantImages { get; set; }
        public DbSet<PlantModel> Plants { get; set; }
        public DbSet<BasketResultModel> Baskets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

