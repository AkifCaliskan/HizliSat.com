using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess.Repositories
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateTime { get; set; }
    }
}
