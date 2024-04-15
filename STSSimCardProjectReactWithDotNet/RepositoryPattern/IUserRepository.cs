using STSSimCardProjectReactWithDotNet.Models;

namespace STSSimCardProjectReactWithDotNet.RepositoryPattern
{
    public interface IUserRepository
    {
        bool IUniqueUser(string EMail);
        User Authenicate(string EMail, string Password);
        User Register(string EMail, string Password);
    }
}
