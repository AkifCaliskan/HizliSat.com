using Microsoft.AspNetCore.Http;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Abstract
{
    public interface IAdvertService
    {
        Task<Advert> Add(AdvertAddModel advertAddModel, IFormFileCollection files);
        Task<Advert> Update(AdvertEditModel advertEditModel);
        Task<Advert> GetById(int id);
        Task Delete(int id);
        Task<List<AdvertListModel>> List();
    }
}
