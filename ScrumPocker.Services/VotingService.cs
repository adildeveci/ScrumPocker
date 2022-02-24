using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumPocker.Services
{
    public interface IVotingService
    {
        BaseResponse<List<Voting>> GetDefaultVoting();
    }
    public class VotingService : IVotingService
    {
        public BaseResponse<List<Voting>> GetDefaultVoting()
        {
            var data = new List<Voting>()
            {
                new Voting(){
                    Name = "Fibonacci",
                    Values={ "0", "1", "2", "3", "5", "8", "13", "21", "34", "55", "89", "?"}
                },
                new Voting(){
                     Name = "Modified Fibonacci",
                     Values={ "0", "½", "1","2", "3", "5", "8", "13", "20", "40", "100", "?"}
                },
                new Voting(){
                     Name = "T-shirts",
                     Values={ "XXS", "XS", "S","M", "L", "XL", "XXL","?"}
                },
                new Voting(){
                     Name = "Powers of 2",
                    Values={ "0", "1", "2", "4", "8", "16", "32", "64", "?"}
                },
            };
            return BaseResponse<List<Voting>>.Success(data);

        }
    }
}
