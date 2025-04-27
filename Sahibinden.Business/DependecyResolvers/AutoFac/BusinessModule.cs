using Autofac;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.AutoMapper;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Business.Model.Advert;
using Sahibinden.DataAccess.Concrete;
using Sahibinden.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.DependecyResolvers.AutoFac
{
    public class BusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<AdvertService>().As<IAdvertService>();
            builder.RegisterType<AdvertDetailService>().As<IAdvertDetailService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<CategoryFeatureService>().As<ICategoryFeaturesService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
            builder.RegisterType<Context>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCache>().As<IMemoryCache>();
            builder.RegisterType<AuthService>().As<IAuthService>();
         
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            // AutoMapper kaydı
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                // Tüm Mapping Profilleri burada eklenir
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper())
            .As<IMapper>()
            .SingleInstance();
        }


    }
}

