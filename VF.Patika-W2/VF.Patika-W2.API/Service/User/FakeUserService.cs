namespace VF.Patika_W2.API.Service.User
{
    public class FakeUserService : IUserService
    {
        // Bu örnekte sadece tek bir kullanıcı var. Gerçekte bir veritabanı veya başka bir kaynakla kontrol edilir.
        public bool IsValidUser(string username, string password)
        {
            return username == "testuser" && password == "password123";
        }
    }
}
