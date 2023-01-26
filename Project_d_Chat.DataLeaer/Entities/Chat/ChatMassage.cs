
namespace Project_d_Chat.DataLeaer.Entities.Chat
{
    public class ChatMassage
    {
        [Key]
        public int ChatMassageId { get; set; }
        public int ChatId { get; set; }
        public int SerderID { get; set; }
        public string? Text { get; set; }
        public DateTimeOffset SendAt { get; set; }
        #region Relations

        public  ChatRoom? ChatRoom { get; set; }
        #endregion
    }
}
