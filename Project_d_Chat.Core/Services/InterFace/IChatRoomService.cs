
using Project_d_Chat.DataLeaer.Entities.Chat;

namespace Project_d_Chat.Core.Services.InterFace
{
    public interface IChatRoomService
    {
        //Task<int> CreateRoom();
        Task<int> GetRoomForConnectionId(int chatId);
        Task SetRoomName(int roomId, string name);

        Task AddMessage(int roomId, ChatMassage message);
        Task<IEnumerable<ChatMassage>> GetMessageHistory(int roomId);

        Task<List<ChatRoom>> GetAllRooms();
    }
}
