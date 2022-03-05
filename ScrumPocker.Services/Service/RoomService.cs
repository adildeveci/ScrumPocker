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
        BaseResponse<RoomSummaryDto> CreateRoom(CreateRoomDto request);
        BaseResponse<List<RoomSummaryDto>> GetRooms();//TODO: filter for GetRooms
        BaseResponse<RoomDetailDto> GetRoomDetail(RoomDetailRequestDto request);
        BaseResponse JoinRoom(JoinRoomDto request);
        BaseResponse LeaveRoom(LeaveRoomDto request);
        BaseResponse DeleteRoom(DeleteRoomDto request);
    }
    public class RoomService : IRoomService
    {
        public BaseResponse<RoomSummaryDto> CreateRoom(CreateRoomDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<RoomSummaryDto>.Fail("Kullanıcı bulunamadı", 404);

            var room = ObjectMapper.Mapper.Map<Room>(request);

            room.PasswordHash = request.IsPublic ? null : Hashing.HashSHA512(request.Password);
            room.CreatedUserId = user.Id;
            room.Users.Add(user);

            StaticDbContext.Rooms.Add(room);

            var result = ObjectMapper.Mapper.Map<RoomSummaryDto>(room);
            return BaseResponse<RoomSummaryDto>.Success(result);
        }
        public BaseResponse<List<RoomSummaryDto>> GetRooms()
        {
            var rooms = StaticDbContext.Rooms;
            var result = ObjectMapper.Mapper.Map<List<RoomSummaryDto>>(rooms);
            return BaseResponse<List<RoomSummaryDto>>.Success(result);
        }
        public BaseResponse<RoomDetailDto> GetRoomDetail(RoomDetailRequestDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<RoomDetailDto>.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Id == request.RoomId);
            if (room == null)
                return BaseResponse<RoomDetailDto>.Fail("Oda bulunamadı", 404);

            if (!room.Users.Any(x => x.Id == user.Id))
                return BaseResponse<RoomDetailDto>.Fail("Kullanıcı odada değil");

            var roomDetail = ObjectMapper.Mapper.Map<RoomDetailDto>(room);
            roomDetail.UserAndVotes= (
                                            from u in room.Users
                                            join v in room.Votes on u.Id equals v.UserId into vt
                                            from v in vt.DefaultIfEmpty()
                                            select new UserAndVoteDto
                                            {
                                                Id = u.Id,
                                                Name = u.Name,
                                                SurName = u.SurName,
                                                VoteValue = v?.Value//nullable
                                            }
                                            ).ToList();

            return BaseResponse<RoomDetailDto>.Success(roomDetail);
        }
        public BaseResponse JoinRoom(JoinRoomDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Id == request.RoomId);
            if (room == null)
                return BaseResponse.Fail("Oda bulunamadı", 404);

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
                return BaseResponse.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Id == request.RoomId);
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
            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Id == request.RoomId);
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
