using AutoMapper;
using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Dto.Voting;
using ScrumPocker.Core.Models;

namespace ScrumPocker.Services
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CreateRoomDto, Room>();
            CreateMap<Room, RoomSummaryDto>().ReverseMap();
            CreateMap<VoteRequestDto, VoteModel>(); 
            CreateMap<Room, RoomDetailDto>();
        }
    }
}