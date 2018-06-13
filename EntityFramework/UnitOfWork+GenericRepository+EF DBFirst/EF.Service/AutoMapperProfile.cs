using AutoMapper;
using EF.Data;
using EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<ClientModel.UserCredential, ValidateCredentialResultModel>().ForMember(
            //            dest => dest.UserType,
            //            opt => opt.MapFrom(src => (UserType)Enum.Parse(typeof(UserType), src.UserType, true)));
            // CreateMap<EASvcModel.EmployeeInformationExt, EmployeeDetailModel>();
            // CreateMap<Authorization.Profile, UserProfileModel>();
            // CreateMap<ProfileModel, EditViewModel>();
            // CreateMap<ProfileModel, RLC.Enterprise.Security.Authorization.Services.AuthorizationRestServices.Models.GetProfileResponse>();
            //CreateMap<T_Book, Book>();
            CreateMap<Book, T_Book>().ForMember(
                dest => dest.CurrencyTypeID,
                opt => opt.MapFrom(src=>src.CurrencyType));

        }
    }
}
