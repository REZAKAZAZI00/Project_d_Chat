
namespace Project_d_Chat.Core.Services.InterFace
{
    public interface IUserService
    {

        bool IsExistUsername(string userName);
       
        int AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User LoginUser(LoginViewModel login);
        User? GetUserByName(string userName);
       
        User? GetUserById(int id);
        int GetUserIdByUserName(string userName);
    }
}
