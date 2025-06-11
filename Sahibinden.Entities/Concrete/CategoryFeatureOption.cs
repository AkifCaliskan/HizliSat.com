using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities.Concrete
{
    public class CategoryFeatureOption:EntityBase
    {
        public int CategoryFeatureId { get; set; }

        [Required]
        [StringLength(100)]
        public string OptionText { get; set; }

        public int DisplayOrder { get; set; }

        [ForeignKey("CategoryFeatureId")]
        public virtual CategoryFeature CategoryFeature { get; set; }
    }
}
