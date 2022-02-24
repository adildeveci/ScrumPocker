using System;
using System.Collections.Generic;

namespace ScrumPocker.Core.Models
{
    public class Room
    {
        public Room()
        {
            Users = new List<UserModel>();
            Guid = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            // ExpriDate = DateTime.Now.AddHours(HourExpireIn);
        }
        public int Id { get; set; }
        public Guid Guid { get; set; }
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
        public string Password { get; set; }
        public List<UserModel> Users { get; set; }
        public Voting Voiting { get; set; }
    }
}
