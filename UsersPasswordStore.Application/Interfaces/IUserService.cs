using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersPasswordStore.Domain.Model;

namespace UsersPasswordStore.Application.Interfaces
{
    public interface IUserService
    {
        List<UsersPassword> InsertNewUser(List<UsersPassword> users);
        List<UsersPassword> GetCachedArrayList(string cacheKey, out string cachedList);
        List<string> GetPasswordList();
        UsersPassword GetSingleItem();
        List<UsersPassword> GetListItem(List<UsersPassword> lstUsers);
        void UpdatePassword(string newPass,string oldPass);
        bool RemoveCache();
    }
}
