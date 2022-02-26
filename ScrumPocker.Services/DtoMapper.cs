using AutoMapper;
using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Models;

namespace ScrumPocker.Services
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<Room, RoomSummaryDto>().ReverseMap();
        }
    }
}