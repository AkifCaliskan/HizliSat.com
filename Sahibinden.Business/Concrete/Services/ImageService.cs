using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.Image;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Concrete.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Image> Add(ImageAddModel imageAddModel)
        {
            var image = _mapper.Map<Image>(imageAddModel);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);
            return image;
        }
    }
}
