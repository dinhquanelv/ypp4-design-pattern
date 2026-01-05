using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Strategy
{
    public enum LoginMethod
    { 
        Google,
        Apple,
        Facebook
    }

    public class UserAuth
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserAuth(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public interface IAuthStrategy
    {
        bool Authenticate(UserAuth user);
    }

    public class GoogleAuth : IAuthStrategy
    {
        public bool Authenticate(UserAuth user)
        {
            Console.WriteLine($"Authenticating {user.Username} via Google...");
            return true;
        }
    }

    public class AppleAuth : IAuthStrategy
    {
        public bool Authenticate(UserAuth user)
        {
            Console.WriteLine($"Authenticating {user.Username} via Apple...");
            return true;
        }
    }

    public class FacebookAuth : IAuthStrategy
    {
        public bool Authenticate(UserAuth user)
        {
            Console.WriteLine($"Authenticating {user.Username} via Facebook...");
            return true;
        }
    }
    public class AuthContext
    {
        private IAuthStrategy _authStrategy;
        public void SetStrategy(IAuthStrategy authStrategy)
        {
            _authStrategy = authStrategy;
        }
        public bool Authenticate(UserAuth user)
        {
            if (_authStrategy == null)
            {
                throw new InvalidOperationException("Authentication strategy not set.");
            }
            return _authStrategy.Authenticate(user);
        }
    }
}
