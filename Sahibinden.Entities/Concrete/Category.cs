using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class Category: EntityBase
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public List<Advert> Adverts{ get; set; }
        public List<CategoryFeature> Feature { get; set; }
    }
}
