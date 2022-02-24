using Microsoft.AspNetCore.SignalR;
using ScrumPocker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPocker.API.Hubs
{
    public interface IScrumPockerHub
    {
        Task ReceiveRooms(List<Room> rooms);
        Task ReceiveError(string message);
    }
    public class ScrumPockerHub : Hub<IScrumPockerHub>
    { 
       
        public ScrumPockerHub()
        {
        }
        //public async Task GetRooms()
        //{
        //    await Clients.All.ReceiveRooms(Rooms);
        //}
        //public async Task CreateRoom(Room room)
        //{
        //    if (Rooms.Any(x => x.Name.Equals(room.Name)))
        //    {
        //        //  await Clients.Caller.SendAsync("Error", $"{room.Name} isimli takım zaten var");
        //        await Clients.Caller.ReceiveError($"{room.Name} isimli takım zaten var");
        //    }
        //    else
        //    {
        //        Rooms.Add(room);
        //        await Clients.All.ReceiveRooms(Rooms);
        //    }
        //}

    }
}
