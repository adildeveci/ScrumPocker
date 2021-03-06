using System;
using System.Collections.Generic;

namespace ScrumPocker.Core.Models
{
    public class Room
    {
        public Room()
        {
            Users = new List<UserModel>();
            Votes = new List<VoteModel>();
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        } 
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpriDate
        {
            get
            {
                return CreatedDate.AddHours(HourExpireIn);
            }
        }
        public int HourExpireIn { get; set; }
        public bool IsPublic { get; set; }
        public string PasswordHash { get; set; }
        public string CreatedUserId { get; set; }
        public bool WasRevealed { get; set; }//puanlar gosterildi mi bilgisi
        public List<UserModel> Users { get; set; }
        public List<VoteModel> Votes { get; set; }
        public VotingDefinition VotingDefinition { get; set; }
    } 
}
