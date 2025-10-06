using OctobridgeCoreRestService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctobridgeCoreRestService.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        // TODO: Replace hard coded users with database users
        readonly List<User> _users = new()
        {
            new User { Id = 1, FirstName = "Tony", LastName = "Gyles", Username = "tonygyles", Password = "chelsea_1963" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password)).ConfigureAwait(true);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            // return users without passwords
            return await Task.Run(() => _users.Select(x => {
                x.Password = null;
                return x;
            })).ConfigureAwait(true);
        }
    }
}
