﻿using Microsoft.EntityFrameworkCore;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.DTO_s;
using Sahibinden.Business.Model.User;
using Sahibinden.DataAccess.UnitOfWork;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.User;

namespace Sahibinden.Business.Concrete.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, ICacheService cacheService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _tokenService = tokenService;
        }

        public async Task<UserDto?> Authenticate(UserLoginDetailModel userLoginDetailModel)
        {
            if (!string.IsNullOrWhiteSpace(userLoginDetailModel.Email) && !string.IsNullOrWhiteSpace(userLoginDetailModel.Password) && userLoginDetailModel.Email.Length > 3 && userLoginDetailModel.Password.Length > 3)
            {
                var user = await _unitOfWork.GetRepository<User>().Query().FirstOrDefaultAsync(u => u.Email == userLoginDetailModel.Email);

                if (user == null)
                {
                    return null;
                }
                var storedPassword = user.Password;

                var isPasswordValid = PasswordHelper.VerifyPassword(userLoginDetailModel, storedPassword);

                if (isPasswordValid)
                {
                    var token = _tokenService.GenerateJwtToken(user);
                    var userDto = new UserDto
                    {

                        Id = user.Id,
                        Email = user.Email,
                    };

                    return userDto;
                }
            }
            return null;
        }
        public async Task<bool> Register(UserRegisterModel userRegisterModel)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var existingUser = (await userRepo.GetAllAsync()).FirstOrDefault(u => u.Email == userRegisterModel.Email);
            if (existingUser != null)
            {
                return false;
            }
            string hashedPassword = PasswordHelper.HashPassword(userRegisterModel.Password);
            var newUser = new User
            {
                Email = userRegisterModel.Email,
                Password = hashedPassword,
                Address = userRegisterModel.Address,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Phone = userRegisterModel.Phone,
                RecordDate = userRegisterModel.RecordDate
            };

            await userRepo.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
            var cacheKey = $"user_{newUser.Id}";
            _cacheService.SetToCache(cacheKey, newUser, TimeSpan.FromHours(1));
            return true;
        }
    }
}