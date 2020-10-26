using AutoMapper;
using Data.DataTransferObjects;
using Data.Models;
using Data.ViewModels;

namespace AccountOwnerServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerViewModel>();
            CreateMap<OwnerCreateDto, Owner>();
            CreateMap<OwnerUpdateDto, Owner>();

            CreateMap<Account, AccountViewModel>();
        }
    }
}
