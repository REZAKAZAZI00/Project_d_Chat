

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project_d_Chat.Core.Security;
using Project_d_Chat.DataLeaer.Entities.Chat;

namespace Project_d_Chat
{
    public class ChatHub:Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IHubContext<AgentHub> _agentHub;
        private readonly IUserService _userService;
        private readonly ChatDbContext _chatDbContext;
        
        public ChatHub(IChatRoomService chatRoomService, IHubContext<AgentHub> agentHub,IUserService userService,ChatDbContext chatDbContext)
        {
            _chatRoomService = chatRoomService;
            _agentHub = agentHub;
            _userService = userService;
            _chatDbContext = chatDbContext;
        }


        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await base.OnConnectedAsync();
                return;
            }

           // var roomId = await _chatRoomService.CreateRoom();
            await Groups.AddToGroupAsync(Context.ConnectionId, 1.ToString());

            await Clients.Caller.SendAsync("ReciveMessage",
                "Support",
                DateTimeOffset.UtcNow,
                "Hello ! What can we help you with today ?");

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string name, string text)
        {
            var roomId = await _chatRoomService.GetRoomForConnectionId(_userService.GetUserIdByUserName(Context.User.Identity.Name));
            var message = new ChatMassage
            {
                ChatId= 1,
                SerderID=_userService.GetUserIdByUserName(Context.User.Identity.Name),
                Text = AlgorithmRSA.Encryption(text),
                SendAt = DateTimeOffset.Now
            };

            await _chatRoomService.AddMessage(1, message);


            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message.SendAt, text);
        }

        public async Task SetName(string visitorName)
        {
            var roomName = $"Chat With {visitorName} from the web .";

            var roomId = await _chatRoomService.GetRoomForConnectionId(1);
            
            await _chatRoomService.SetRoomName(roomId, visitorName);
            
           
            await _agentHub.Clients.All.SendAsync("ActiveRooms", await _chatRoomService.GetAllRooms());
        }

        [Authorize]
        public async Task JoinRoom(int roomId)
        {
            if (roomId == null)
            {
                throw new ArgumentException("Invalid Room ID");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        [Authorize]
        public async Task LeaveRoom(int roomId)
        {
            if (roomId == 0)
            {
                throw new ArgumentException("Invalid Room ID");
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }

    }
}
