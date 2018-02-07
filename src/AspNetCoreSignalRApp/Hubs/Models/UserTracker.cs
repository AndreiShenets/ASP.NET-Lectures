using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AspNetCoreSignalRApp.Hubs.Models
{
    public class UserTracker : IUserTracker
    {
        protected ConcurrentDictionary<string, UserDetails> usersDictionary = new ConcurrentDictionary<string, UserDetails>();

        public void AddUser(UserDetails userDetails)
        {
            usersDictionary.AddOrUpdate(userDetails.ConnectionId, userDetails, (key, value) => userDetails);
        }

        public UserDetails GetUser(string connectionId)
        {
            usersDictionary.TryGetValue(connectionId, out UserDetails userDetails);
            return userDetails;
        }

        public IEnumerable<UserDetails> GetUsers()
        {
            return usersDictionary.Values;
        }

        public void RemoveUser(UserDetails userDetails)
        {
            usersDictionary.Remove(userDetails.ConnectionId, out UserDetails deletedUser);
        }
    }
}
