using Project_d_Chat.DataLeaer.Context;
using Project_d_Chat.DataLeaer.Entities.Chat;

namespace Project_d_Chat.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _ChatDbContext;
        private readonly IUserService _userService;
        public ChatService(ChatDbContext chatDbContext, IUserService userService)
        {
            _ChatDbContext = chatDbContext;
            _userService = userService;
        }
        public Task<int> CreateRoom(int userId)
        {
            ChatRoom room= new ChatRoom();
            room.Name=Guid.NewGuid().ToString();
            _ChatDbContext.ChatRooms.Add(room);
            return Task.FromResult(room.ChatId);
        }

        public ChatRoom GetRoomForUserId(int userId)
        {
            return _ChatDbContext.ChatRooms.Single(c => c.ChatId == userId);
        }
    }
}
