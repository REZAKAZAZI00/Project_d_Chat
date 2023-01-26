

using Project_d_Chat.DataLeaer.Entities.Chat;

namespace Project_d_Chat.DataLeaer.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserNmae { get; set; }
        public string? Password { get; set; } 
        public DateTime Created { get; set; }= DateTime.Now;
        public bool IsDelete { get; set; }= false;

        #region Relations
       
        #endregion
    }
}
