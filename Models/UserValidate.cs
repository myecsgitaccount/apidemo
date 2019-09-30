using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDemo.Models
{
    public class UserValidate
    {
        //This method is used to check the user credentials
        public static bool Login(string username, string password)
        {
            UsersBL userBL = new UsersBL();
            var UserLists = userBL.GetUsers();
            return UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password);
        }
    }

}