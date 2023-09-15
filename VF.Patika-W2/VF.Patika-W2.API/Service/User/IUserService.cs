namespace VF.Patika_W2.API.Service.User
{
    public interface IUserService
    {
        bool IsValidUser(string username, string password);
    }
}
