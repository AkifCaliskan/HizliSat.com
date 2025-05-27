using AutoMapper;
using Sahibinden.Business.Concrete.Services;
using Sahibinden.Business.DTO_s;
using Sahibinden.Business.Model.Advert;
using Sahibinden.Business.Model.AdvertDetail;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advert, AdvertListModel>().ReverseMap();
            CreateMap<AdvertAddModel, Advert>();
            CreateMap<AdvertEditModel, Advert>();
            CreateMap<ImageAddModel, Image>();
            CreateMap<AdvertDetail, AdvertDetailAdd>();
            CreateMap<UserDto, User>();
                    }
    }
}
