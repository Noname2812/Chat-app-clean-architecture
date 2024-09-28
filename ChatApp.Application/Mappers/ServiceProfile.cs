

using AutoMapper;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Mappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            //V1
            CreateMap<ConversationParticipant, ConversationParticipantRespone>().ReverseMap();
            CreateMap<RoomChat, RoomChatRespone>().ReverseMap();
            CreateMap<PageResult<RoomChat>, PageResult<RoomChatRespone>>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<AppUser, InfoMember>().ReverseMap();
            CreateMap<Message, MessageDTO>();
            CreateMap<PageResult<Message>, PageResult<MessageDTO>>();
            //V2
        }
    }
}
