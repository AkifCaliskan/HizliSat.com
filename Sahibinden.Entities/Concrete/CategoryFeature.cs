using Sahibinden.Entities.Enums;

namespace Sahibinden.Entities.Concrete
{
    public class CategoryFeature : EntityBase
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public InputType InputType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }

}

