using Project_d_Chat.DataLeaer.Context;
using Project_d_Chat.DataLeaer.Entities.Chat;
using Project_d_Chat.DataLeaer.Entities.User;
using ProjectdChat.DataLeaer.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project_d_Chat.Core.Services
{
#nullable enable
    public class ChatRoomService : IChatRoomService
    {
        private readonly ChatDbContext _ChatDbContext;
        private readonly IUserService _UserService;
        private readonly Dictionary<int, ChatRoom> _roomInfo = new Dictionary<int, ChatRoom>();
        private readonly Dictionary<int, List<ChatMassage>> _messageHistory = new Dictionary<int, List<ChatMassage>>();

        public ChatRoomService(ChatDbContext chatDbContext,IUserService userService)
        {
            _ChatDbContext = chatDbContext;
            _UserService = userService;
        }

        public Task AddMessage(int roomId, ChatMassage message)
        {
            if (!_messageHistory.ContainsKey(roomId))
            {
                _messageHistory[1] = new List<ChatMassage>();
            }
            _messageHistory[1].Add(message);
            _ChatDbContext.ChatMassages.Add(message);
            _ChatDbContext.SaveChanges();

            return Task.CompletedTask;
        }

        //public Task<int> CreateRoom()
        //{
        //    ChatRoom room = new ChatRoom();
        //    room.Name = Guid.NewGuid().ToString();
        //    _ChatDbContext.ChatRooms.Add(room);
        //    _ChatDbContext.SaveChanges();
        //    return Task.FromResult(room.ChatId);
        //}

        public Task<List<ChatRoom>> GetAllRooms()
        {
            return Task.FromResult(_ChatDbContext.ChatRooms.ToList());
        }

        public Task<IEnumerable<ChatMassage>> GetMessageHistory(int roomId)
        {
            _messageHistory.TryGetValue(1, out var messages);

            messages = messages ?? new List<ChatMassage>();
            var sortedMessages = messages.OrderBy(x => x.SendAt)
                .AsEnumerable();
            //var UserId = _UserService.GetUserIdByUserName(User  User.Identity.Name);
            var chat=_ChatDbContext.ChatMassages.Where(c=>c.ChatId==1).AsEnumerable();

            return Task.FromResult(chat);
        }

        public Task<int> GetRoomForConnectionId(int chatId)
        {
            //var foundRoom = _roomInfo.FirstOrDefault(x => x.Value == userId);
            var foundroom=_ChatDbContext.ChatRooms.FirstOrDefault(c=>c.ChatId == chatId);


            if (foundroom==null)
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(foundroom.ChatId);
        }

        public Task SetRoomName(int roomId, string name)
        {
            if (!_roomInfo.ContainsKey(roomId))
            {
                throw new ArgumentException("Invalid Room ID");
            }

            _roomInfo[roomId].Name = name;
            var chat=_ChatDbContext.ChatRooms.Find(roomId);
            chat.Name=name;
            _ChatDbContext.ChatRooms.Update(chat);
            _ChatDbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
