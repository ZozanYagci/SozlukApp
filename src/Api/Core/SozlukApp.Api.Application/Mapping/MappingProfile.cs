﻿using AutoMapper;
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

            CreateMap<UserDetailViewModel, User>()
                .ReverseMap();

            CreateMap<CreateEntryCommand, Entry>()
                .ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>()
                .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));

            CreateMap<CreateEntryCommentCommand, EntryComment>()    
                .ReverseMap();
        }
    }
}
