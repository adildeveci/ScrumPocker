using ScrumPocker.Core.Dto.Voting;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Core.StaticDb;
using System.Collections.Generic;
using System.Linq;

namespace ScrumPocker.Services
{
    public interface IVotingService
    {
        BaseResponse<List<VotingDefinition>> GetVotingDefinitions();
        BaseResponse Vote(VoteRequestDto request);
    }

    public class VotingService : IVotingService
    {
        public BaseResponse<List<VotingDefinition>> GetVotingDefinitions()
        {
            var data = new List<VotingDefinition>()
            {
                new VotingDefinition(){
                    Name = "Fibonacci",
                    Values={ "0", "1", "2", "3", "5", "8", "13", "21", "34", "55", "89", "?"}
                },
                new VotingDefinition(){
                     Name = "Modified Fibonacci",
                     Values={ "0", "½", "1","2", "3", "5", "8", "13", "20", "40", "100", "?"}
                },
                new VotingDefinition(){
                     Name = "T-shirts",
                     Values={ "XXS", "XS", "S","M", "L", "XL", "XXL","?"}
                },
                new VotingDefinition(){
                     Name = "Powers of 2",
                    Values={ "0", "1", "2", "4", "8", "16", "32", "64", "?"}
                },
            };
            return BaseResponse<List<VotingDefinition>>.Success(data);

        }

        public BaseResponse Vote(VoteRequestDto request)
        {
            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return BaseResponse<BaseResponse>.Fail("Kullanıcı bulunamadı", 404);

            var room = StaticDbContext.Rooms.FirstOrDefault(x => x.Id == request.RoomId);
            if (room == null)
                return BaseResponse.Fail("Oda bulunamadı", 404);

            if (!room.Users.Any(x => x.Id == user.Id))
                return BaseResponse.Fail("Puan verebilmek için odaya giriş yapmalısınız");

            if (!room.VotingDefinition.Values.Any(x => x == request.Value))
                return BaseResponse.Fail("Verilen puan oda tanımlarında yok");

            var currentVote = room.Votes.FirstOrDefault(x => x.UserId == request.UserId);
            if (currentVote != null)
            {
                //update
                currentVote.Value = request.Value;
            }
            else
            {
                //insert
                var model = ObjectMapper.Mapper.Map<VoteMoel>(request);
                room.Votes.Add(model);
            } 

            return BaseResponse.Success();
        }
    }
}
