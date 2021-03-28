using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Models
{
    public class RatingModel
    {
        public int Rate { get; set; }
        public Guid ProductID { get; set; }
        public Guid UserID { get; set; }
    }
}
