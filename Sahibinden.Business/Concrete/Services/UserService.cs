using AutoMapper;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Model.User;
using Sahibinden.Core.EntityFramework;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task Delete(int id)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var deleteditem = await repository.GetByIdAsync(id);
            if (deleteditem == null)
            {
                throw new Exception("Hata");
            }
            repository.DeleteAsync(deleteditem);
            await _unitOfWork.SaveChangesAsync();



        }

        public async Task<User> GetById(int id)
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> List(UserListModel userListModel)
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.GetAllAsync();
        }
    }
}
