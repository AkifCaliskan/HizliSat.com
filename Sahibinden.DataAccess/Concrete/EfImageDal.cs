using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.Abstract;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess.Concrete
{
    public class EfImageDal:EfEntityRepositoryBase<Image, Context> , IImagesDal
    {
    }
}
