using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Models
{
    public class ProductModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserID { get; set; }
    }
}
