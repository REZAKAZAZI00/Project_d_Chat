using Project_d_Chat.Core.Security;

namespace Project_d_Chat.Core.Services
{
#nullable enable
    public class UserService : IUserService
    {
        private readonly ChatDbContext _context;
        public UserService(ChatDbContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public void DeleteUser(int userId)
        {
            /*TODO delete User
            _context.Update(user);*/
            _context.SaveChanges();
        }


        public User? GetUserById(int id)
        {
          return  _context.Users.Find(id);
        }

        public User? GetUserByName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserNmae == userName);
        }

      

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserNmae == userName).UserId;
        }

       

        public bool IsExistUsername(string userName)
        {
            return _context.Users.Any(u => u.UserNmae==userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordSHA1(login.Password);

#pragma warning disable CS8603 // Possible null reference return.
            return _context.Users.SingleOrDefault(u => u.UserNmae==login.Username && u.Password == hashPassword);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
