using Autofac;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.AutoMapper;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Business.Model.User;
using Sahibinden.DataAccess.Concrete;
using Sahibinden.DataAccess.Repositories;
using Sahibinden.DataAccess.UnitOfWork;
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

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AdvertService>().As<IAdvertService>().InstancePerLifetimeScope();
            builder.RegisterType<AdvertDetailService>().As<IAdvertDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ImageService>().As<IImageService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryFeatureService>().As<ICategoryFeaturesService>().InstancePerLifetimeScope();
            builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();
            builder.RegisterType<Context>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            // AutoMapper kaydı
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                
                cfg.AddProfile<MappingProfile>();
                cfg.AddMaps(ThisAssembly, typeof(UserRegisterModel).Assembly);
            }).CreateMapper())
            .As<IMapper>()
            .SingleInstance();
        }


    }
}

