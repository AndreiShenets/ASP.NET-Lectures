using System.Collections.Generic;

namespace AspNetCoreSignalRApp.Hubs.Models
{
    public interface IUserTracker
    {
        IEnumerable<UserDetails> GetUsers();

        void AddUser(UserDetails userDetails);

        void RemoveUser(UserDetails userDetails);

        UserDetails GetUser(string connectionId);
    }
}
