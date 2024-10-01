using AutoMapper;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Common.Models.Queries;
using SozlukApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateEntryCommand, Entry>()
                .ReverseMap();

            CreateMap<CreateEntryCommentCommand, EntryComment>()    
                .ReverseMap();
        }
    }
}
