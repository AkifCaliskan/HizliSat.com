using Sahibinden.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class CategoryFeature : EntityBase
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        
    }

}

