using CMS.Core.Services.Interfaces;
using CMS.DataLayer;
using System.Linq;

namespace CMS.Core.Services.Services
{
    public class LoginService : ILoginService
    {
        private CmsContext _context;

        public LoginService(CmsContext context)
        {
            _context = context;
        }
        public bool IsExistUser(string username, string password)
        {
            return _context.Logins.Any(p => p.UserName.ToLower() == username.ToLower() && p.Password == password);
        }
    }
}
