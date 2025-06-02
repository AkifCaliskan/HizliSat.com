using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class AdvertDetail : EntityBase

    {
        public bool Status { get; set; }
        public int AdvertId { get; set; }
        public string Value { get; set; }
        public Advert Advert { get; set; }
        public int CategoryFeatureId { get; set; }
        public CategoryFeature Feature { get; set; }
    }
}
