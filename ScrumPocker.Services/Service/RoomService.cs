using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Helpers;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Core.StaticDb;
using System.Collections.Generic;
using System.Linq;

namespace ScrumPocker.Services
{
    public interface IRoomService
    {
        BaseResponse<Room> CreateRoom(CreateRoomDto request);
        BaseResponse<List<Room>> GetRooms();
        BaseResponse JoinRoom(JoinRoomDto request);
        BaseResponse LeaveRoom(LeaveRoomDto request);
        BaseResponse DeleteRoom(DeleteRoomDto request);
    }
    public class RoomService : IRoomService
    {
        public BaseResponse<Room> CreateRoom(CreateRoomDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<Room>.Fail("Kullanıcı bulunamadı", 404);

            var room = ObjectMapper.Mapper.Map<Room>(request);
            //TODO: auto mapper kullan
            //var room = new Room
            //{
            //    Name = request.Name,
            //    IsPublic = request.IsPublic,
            //    HourExpireIn = request.HourExpireIn,
            //    Voiting = request.Voiting,
            //};

            room.PasswordHash = request.IsPublic ? null : Hashing.HashSHA512(request.Password);
            room.CreatedUserId = user.Id;
            room.Users.Add(user);

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
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<Room>.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Guid == request.RoomGuid);
            if (room == null)
                return BaseResponse.Fail("Giriş yapılmak istenen oda bulunamadı", 404);

            if (room.Users.Any(x => x.Id == user.Id))
                return BaseResponse.Success();//zaten odada

            if (!room.IsPublic && room.CreatedUserId != user.Id && !Hashing.CheckHashSHA512(request.RoomPassword, room.PasswordHash))//oda herkese acik degilse kurucu haricindekilere password dogrula
                return BaseResponse.Fail("Oda şifresinini yanlış girdiniz");

            room.Users.Add(user);
            return BaseResponse.Success();
        }
        public BaseResponse LeaveRoom(LeaveRoomDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<Room>.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Guid == request.RoomGuid);
            if (room == null)
                return BaseResponse.Fail("Oda bulunamadı", 404);

            if (!room.Users.Any(x => x.Id == user.Id))
                return BaseResponse.Fail("Kullanıcı odada değil");

            if (room.Users.Remove(user))
            {
                return BaseResponse.Success();
            }
            else
            {
                return BaseResponse.Fail("Çıkış yapılamadı");
            }
        }
        public BaseResponse DeleteRoom(DeleteRoomDto request)
        {
            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Guid == request.RoomGuid);
            if (room == null)
                return BaseResponse.Fail("Oda bulunamadı", 404);

            if (room.CreatedUserId != request.UserId)
                return BaseResponse.Fail("Odayı sadece oluşturan kişi silebilir", 403);

            if (StaticDbContext.Rooms.Remove(room))
            {
                return BaseResponse.Success();
            }
            else
            {
                return BaseResponse.Fail("Oda silinemedi");
            }
        }
    }
}
