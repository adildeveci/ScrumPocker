using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Core.StaticDb;
using System.Collections.Generic;
using System.Linq;

namespace ScrumPocker.Services
{
    public interface IRoomService
    {
        BaseResponse<Room> CreateRoom(CreateRoomDto roomDto);
        BaseResponse<List<Room>> GetRooms();
        BaseResponse JoinRoom(JoinRoomDto request);
    }
    public class RoomService : IRoomService
    {
        public BaseResponse<Room> CreateRoom(CreateRoomDto roomDto)
        {
            var createdUser = StaticDbContext.Users.FirstOrDefault(x => x.Id == roomDto.CreatedUserId);
            if (createdUser == null)
                return BaseResponse<Room>.Fail("Oda oluşturmak isteyen kullanıcının bilgileri bulunamadı");

            //TODO: auto mapper kullan
            var room = new Room
            {
                Name = roomDto.Name,
                IsPublic = roomDto.IsPublic,
                HourExpireIn = roomDto.HourExpireIn,
                Password = roomDto.Password,
                Voiting = roomDto.Voiting,
            };
            room.Users.Add(createdUser);

            StaticDbContext.Rooms.Add(room);
            return BaseResponse<Room>.Success(room);
        }

        public BaseResponse<List<Room>> GetRooms()
        {
            var data = StaticDbContext.Rooms;
            return BaseResponse<List<Room>>.Success(data);
        }
        public BaseResponse JoinRoom(JoinRoomDto request)
        {
            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Guid == request.RoomGuid);
            if (room == null)
                return BaseResponse.Fail("Giriş yapılmak istenen oda bulunamadı", 404);

            return null;
        }
    }
}
