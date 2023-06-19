using AutoMapper;
using ExpenseTrackerAPI.Application.Dtos.Auth;
using ExpenseTrackerAPI.Application.Dtos.User;
using ExpenseTrackerAPI.Application.Features.User.Command.CreateUser;
using ExpenseTrackerAPI.Application.Features.User.Command.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Mapping.AutoMapper
{
    public class ConversionsProfile : Profile
    {
        public ConversionsProfile()
        {
            CreateMap<CreateUserCommandRequest, CreateUserDto>();
            CreateMap<CreateUserResponseDto, CreateUserCommandResponse>();
            CreateMap<LoginUserResponseDto, LoginUserCommandResponse>();
        }
    }
}
