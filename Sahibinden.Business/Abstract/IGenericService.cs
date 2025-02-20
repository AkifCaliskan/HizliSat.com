using Sahibinden.Business.Model.Advert;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Abstract
{
    public interface IGenericService<T> where T :class
    {
        Task<T> Add(T Tentity);
        Task<T> Update(T Tentity);
        Task<T> GetById(int id);
        Task Delete(int id);
        Task<IEnumerable<T>> List(T entity);
    }
}
