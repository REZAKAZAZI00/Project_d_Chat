

using Project_d_Chat.DataLeaer.Entities.User;

namespace Project_d_Chat.DataLeaer.Entities.Chat
{
    public class ChatRoom
    {
        [Key]
        public int ChatId { get; set; }
        public string? Name { get; set; }

        #region Relations
        public List<ChatMassage>?  chatMassages { get; set; }
       
        #endregion
    }
}
