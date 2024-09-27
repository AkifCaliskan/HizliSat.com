using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class Advert : EntityBase
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ImageId { get; set; }
        public int UserId { get; set; }
        public List<AdvertDetail> Details { get; set; }
    }
}
