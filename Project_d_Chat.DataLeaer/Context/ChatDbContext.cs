using Project_d_Chat.DataLeaer.Entities.Chat;
using Project_d_Chat.DataLeaer.Entities.User;

namespace Project_d_Chat.DataLeaer.Context
{
    public class ChatDbContext:DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options):base(options)
        {

        }
        #region Users
        public DbSet<User> Users { get; set; }

        #endregion
        #region Chat
        public DbSet<ChatRoom>  ChatRooms{ get; set; }
        public DbSet<ChatMassage>  ChatMassages{ get; set; }
        #endregion
    }
}
