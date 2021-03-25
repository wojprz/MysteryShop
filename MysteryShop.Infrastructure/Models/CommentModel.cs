using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryShop.Infrastructure.Models
{
    public class CommentModel
    {
        public Guid UserID { get; set; }
        public string Content { get; set; }
        public Guid ProductID { get; set; }
    }
}
