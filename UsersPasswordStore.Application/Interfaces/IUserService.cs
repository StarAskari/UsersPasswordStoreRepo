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
        /// <summary>
        /// insert a user to memory list with user name key specification
        /// </summary>
        /// <param name="user"></param>
        void InsertNewUser(UsersPassword user);


        /// <summary>
        /// get all passwords of username key
        /// </summary>
        /// <param name="username"></param>
        /// <returns>all passwords of a username</returns>
        List<string>? GetAllPasswordOfUser(string username);

        /// <summary>
        /// get single or default of a user list based on username as a key
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a single userpassword object</returns>
        UsersPassword? GetSingleOrDefaultItem(string username, int id);

        /// <summary>
        /// get single or default of a user list with decrypted password based on username as a key
        /// </summary>
        /// <param name="username"></param>
        /// <returns>a single userpassword object with decrypted password</returns>
        UsersPassword? GetSingleOrDefaultItemWithDecryptedPassword(string username, int id);

        /// <summary>
        /// update a user object in user list
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if update done successfully and false for any error</returns>
        bool UpdateUser(UsersPassword user);

        /// <summary>
        /// delete a user object in user list
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if delete done successfully and false for any error</returns>
        bool DeleteUser(UsersPassword user);

    }
}