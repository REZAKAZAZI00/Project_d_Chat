using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project_d_Chat.Core.Security;


namespace Project_d_Chat
{

#nullable enable
    [Authorize]
    public class AgentHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IUserService _userService;
        public AgentHub(IChatRoomService chatRoomService, IHubContext<ChatHub> chatHub, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _chatHub = chatHub;
            _userService = userService;
        }


        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ActiveRooms", await _chatRoomService.GetAllRooms());
            await base.OnConnectedAsync();
        }

        public async Task SendAgentMessage(int roomId, string text)
        {
            var message = new ChatMassage
            {
                SendAt = DateTimeOffset.UtcNow,
                SerderID =_userService.GetUserIdByUserName(Context.User.Identity.Name),
                Text = AlgorithmRSA.Decryption(text)
            };
           
            await _chatRoomService.AddMessage(1, message);

            await _chatHub.Clients.All
                .SendAsync("ReceiveMessage",Context.User.Identity.Name, message.SendAt, text);
        }

        public async Task LoadHistory(int roomId)
        {
            var history = await _chatRoomService.GetMessageHistory(roomId);
            await Clients.Caller.SendAsync("ReceiveMessages", history);
        }


        public async Task LoadHistoryAll()
        {
            var history = await _chatRoomService.GetMessageHistory(1);
            await Clients.Caller.SendAsync("ReceiveMessagesAll", history);
        }
    }
}

