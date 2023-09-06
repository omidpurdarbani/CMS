namespace CMS.Core.Services.Interfaces
{
    public interface ILoginService
    {
        bool IsExistUser(string username, string password);
    }
}
