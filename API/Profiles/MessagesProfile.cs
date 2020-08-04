using API.Models;
using API.Dtos;
using AutoMapper;

namespace API.Proiles
{
  public class MessagesProfile : Profile
  {
    public MessagesProfile()
    {
        CreateMap<Message, MessageReadDto>();
        CreateMap<MessageCreateDto, Message>();
        CreateMap<MessageUpdateDto, Message>();
        CreateMap<Message, MessageUpdateDto>();
    }
  }
}