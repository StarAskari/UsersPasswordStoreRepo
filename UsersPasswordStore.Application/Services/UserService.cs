﻿using Microsoft.Extensions.Configuration;
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
    public class UserService : IUserService
    {
        private readonly IMemoryCacheService _memoryCacheService;
        public UserService(IMemoryCacheService memoryCacheService)
        {

            _memoryCacheService = memoryCacheService;
        }

        public void InsertNewUser(UsersPassword user)
        {

            try
            {
                string key = user.UserName;

                var userList = _memoryCacheService.Get<UsersPassword>(key);
                if (userList is null)
                    userList = new List<UsersPassword>();
                user.EncryptedPassword = Encrypt(user.EncryptedPassword);
                userList.Add(user);
                _memoryCacheService.Set(key, userList);



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

        public List<string>? GetAllPasswordOfUser(string username)
        {

            try
            {
                string ret = string.Empty;

                var userList = _memoryCacheService.Get<UsersPassword>(username);
                if (userList is not null)
                    return userList.Select(x => x.EncryptedPassword).ToList();

            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return null;

        }

        public UsersPassword? GetSingleOrDefaultItem(string username,int id)
        {
            try
            {

                var userList = _memoryCacheService.Get<UsersPassword>(username);
                if (userList is not null)
                    return userList.SingleOrDefault(x=>x.Id==id);
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public UsersPassword? GetSingleOrDefaultItemWithDecryptedPassword(string username,int id)
        {
            try
            {
                UsersPassword? usersPassword = GetSingleOrDefaultItem(username,id);
                if (usersPassword is not null)
                {
                    usersPassword.DecryptedPassword = Decrypt(usersPassword.EncryptedPassword);
                    usersPassword.EncryptedPassword = usersPassword.DecryptedPassword;
                    return usersPassword;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateUser(UsersPassword user)
        {
            try
            {
                var userList = _memoryCacheService.Get<UsersPassword>(user.UserName);
                if (userList is not null){
                    var userForUpdate = userList.FirstOrDefault(x => x.Id == user.Id);
                    if (userForUpdate is
                        null)
                        return false;


                    userForUpdate.EncryptedPassword = Encrypt(user.EncryptedPassword);
                    _memoryCacheService.Reset(user.UserName);
                    _memoryCacheService.Set(user.UserName, userList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return true;

        }

        public bool DeleteUser(UsersPassword user)
        {
            try
            {
                string ret = string.Empty;

                var userList = _memoryCacheService.Get<UsersPassword>(user.UserName);
                if (userList is not null){
                    var userForDelete = userList.FirstOrDefault(x => x.Id == user.Id);
                    if (userForDelete is null)
                        return false;

                    userList.Remove(userForDelete);
                    _memoryCacheService.Reset(user.UserName);
                    _memoryCacheService.Set(user.UserName, userList);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        public string Encrypt(string password)
        {
            DataProtectionScope scope = DataProtectionScope.CurrentUser;
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, scope);
            return Convert.ToBase64String(encrypted);
        }


        private string Decrypt(string encryptedPass)
        {
            DataProtectionScope scope = DataProtectionScope.CurrentUser;
            if (encryptedPass == null) throw new ArgumentNullException("encryptedPass");

            //parse base64 string
            byte[] data = Convert.FromBase64String(encryptedPass);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, scope);
            return Encoding.Unicode.GetString(decrypted);
        }





    }
}