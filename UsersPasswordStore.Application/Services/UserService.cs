using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UsersPasswordStore.Application.Interfaces;
using UsersPasswordStore.Application.Interfaces.ICache;
using UsersPasswordStore.Domain.Model;
using static System.Formats.Asn1.AsnWriter;

namespace UsersPasswordStore.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly IConfiguration _configuration;
        public UserService(IMemoryCacheService memoryCacheService, IConfiguration configuration) { 
        
            _memoryCacheService = memoryCacheService;
            _configuration = configuration;
        }

        public List<UsersPassword> InsertNewUser(List<UsersPassword> users)
        {
            
            try
            {
                var userInfoKey = "UsersPassword_{ID}";
                users.ForEach(user => user.EncryptedPassword = Encrypt(user.EncryptedPassword));
                
                //var serializedList = JsonConvert.SerializeObject(users);
                _memoryCacheService.Set(userInfoKey, users, DateTime.Now.AddDays(1).Minute, DateTime.Now.AddHours(1).Minute);
                var userList = _memoryCacheService.Get<UsersPassword>(userInfoKey);
                return userList;

            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public string Encrypt (string password)
        {
            DataProtectionScope scope = DataProtectionScope.CurrentUser;
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, scope);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedPass)
        {
            DataProtectionScope scope = DataProtectionScope.CurrentUser;
            if (encryptedPass == null) throw new ArgumentNullException("encryptedPass");

            //parse base64 string
            byte[] data = Convert.FromBase64String(encryptedPass);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, scope);
            return Encoding.Unicode.GetString(decrypted);
        }



        public List<UsersPassword> GetListItem(List<UsersPassword> usersPasswords)
        {
            var userInfoKey = "UsersPassword_{ID}";
            var cachedUserInfo = _memoryCacheService.Get<UsersPassword>(userInfoKey);
            if(cachedUserInfo.Count > 0)
            {
                return cachedUserInfo;
            }
            return null;
        }

        public List<UsersPassword> GetCachedArrayList(string cacheKey, out string cachedList)
        {
            if (_memoryCacheService.TryGetValue(cacheKey, out cachedList))
            {
                // Deserialize the JSON string back to a list
                return JsonConvert.DeserializeObject<List<UsersPassword>>(cachedList);
            }
            else
            {
                // The list is not found in the cache
                return null; // Or return an empty list, depending on your logic
            }
        }





        public List<string> GetPasswordList()
        {
            var userInfoKey = "UsersPassword_{ID}"; 
            List<string> passwordList = new List<string>();
            List<UsersPassword> usersPasswords = new List<UsersPassword>();
            var userInfo = _memoryCacheService.Get<UsersPassword>(userInfoKey);
            if (userInfo != null)
            {
                    passwordList=userInfo.Select(u=>u.EncryptedPassword).ToList();
                
            }
            return passwordList;
        }



        public UsersPassword GetSingleItem()
        {
            var userInfoKey = "UsersPassword_{ID}";
            UsersPassword userPass = new UsersPassword();
           
            var userInfo = _memoryCacheService.Get<UsersPassword>(userInfoKey);
            if (userInfo != null)
            {
                userPass = userInfo.FirstOrDefault();
                userPass.EncryptedPassword = Decrypt(userPass.EncryptedPassword);

            }
            return userPass;
           

        }

        public UsersPassword UpdatePassword(string newPass, UsersPassword usersPassword)
        {
            var userInfoKey = "UsersPassword_{ID}";
            var encryptedPassword = Encrypt(newPass);
            usersPassword.EncryptedPassword = encryptedPassword;


           
            _memoryCacheService.Set(userInfoKey, usersPassword, DateTime.Now.AddDays(1).Minute, DateTime.Now.AddHours(1).Minute);
            var userPass = _memoryCacheService.Get<UsersPassword>(userInfoKey).FirstOrDefault();
            return userPass;


        }

        public bool RemoveCache()
        {
            var userInfoKey = "UsersPassword_{ID}";
            _memoryCacheService.Remove(userInfoKey);
            var data = _memoryCacheService.Get<UsersPassword>(userInfoKey);
            if (data.Count == 0)
            {
                return true;
            }
            else return false;

        }


    }
}
