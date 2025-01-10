using Autofac;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.DataAccess.Abstract;
using Sahibinden.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.DependecyResolvers.AutoFac
{
    public class BusinessModule: Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<AdvertService>().As<IAdvertService>();
            builder.RegisterType<AdvertDetailService>().As<IAdvertDetailService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<CategoryFeatureService>().As<ICategoryFeaturesService>();

  


        }
    }
}
