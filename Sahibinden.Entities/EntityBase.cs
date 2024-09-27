using Sahibinden.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Entities
{
    public class EntityBase : IEntity
    {
        public int Id { get ; set; }
        public DateTime RecordDate { get ; set ; }
    }
}
