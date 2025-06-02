using Sahibinden.Business.Model.Image;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Abstract
{
    public interface IImageService
    {
        Task<Image> Add(ImageAddModel imageAddModel);
    }
}
