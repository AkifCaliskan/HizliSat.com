using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.User;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;

namespace Sahibinden.Business.Concrete.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        private readonly ICacheService _cacheService;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache memoryCache, ICacheService cacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _cacheService = cacheService;
        }

        public async Task<User> Add(UserRegisterModel model)
        {
            var user = _mapper.Map<User>(model);
            user.Password = PasswordHelper.HashPassword(model.Password);
            await _unitOfWork.GetRepository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;    
        }

        public async Task Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var deletedItem = await repository.GetByIdAsync(id);
            if (deletedItem == null)
            {
                ResultWrapperService<User>.FailureResult("Kullanıcı bulunamadı");
            }



            repository.DeleteAsync(deletedItem);
            await _unitOfWork.SaveChangesAsync();

            var cacheKey = $"user_{id}";
            _memoryCache.Remove(cacheKey);
        }

        public async Task<User> GetById(int id)
        {
            var cacheKey = $"user_{id}";
            var cachedUser = _memoryCache.Get<User>(cacheKey);

            if (cachedUser != null)
            {
                return cachedUser;
            }

            var repository = _unitOfWork.GetRepository<User>();
            var user = await repository.GetByIdAsync(id);
            if (user != null)
            {
                _memoryCache.Set(cacheKey, user, TimeSpan.FromHours(1));
            }

            return user;
        }
        public async Task<List<User>> List()
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.GetAllAsync();
        }
       
    }
}
