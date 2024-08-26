using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities.Concrate;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
            CreateMap<ToDoItemInsertDto, ToDoItem>();
        }

    }
}
