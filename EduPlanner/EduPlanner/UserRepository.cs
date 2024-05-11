using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner
{
    public class UserRepository
    {
        static string webAPIKey = "AIzaSyCNJq7uchArJnkH-83YPg_K2v38N6Skhzw";
        FirebaseAuthProvider authProvider;

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<bool> Register(string email, string name, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, name, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string> SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }
    }

}
