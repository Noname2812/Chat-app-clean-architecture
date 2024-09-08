

using AutoMapper;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using System.Text.Json;
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
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<Message, MessageDTO>();
            //V2
        }
    }
}
