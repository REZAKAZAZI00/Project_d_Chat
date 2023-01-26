

using Project_d_Chat.DataLeaer.Entities.Chat;

namespace Project_d_Chat.Core.Services.InterFace
{
    public interface IChatService
    {
        Task<int> CreateRoom(int userId);

        ChatRoom GetRoomForUserId(int userId);  


    }
}
