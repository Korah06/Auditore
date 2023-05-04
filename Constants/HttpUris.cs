using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Constants
{
    public static class HttpUris
    {
        #region TASKS
        public static string GetMyTasks = "http://localhost:3000/tasks/mytasks";
        public static string GetMyCategoryTasks = "http://localhost:3000/tasks/mytasksCategory";
        public static string DeleteTask = "http://localhost:3000/tasks/";
        public static string ModifyTask = "http://localhost:3000/tasks/";
        public static string UpdateMyTasks = "http://localhost:3000/tasks/updatetasks";
        public static string CreateTask = "http://localhost:3000/tasks/";
        #endregion

        #region CHRONOS
        public static string GetChronos = "http://localhost:3000/chrono/";
        public static string CreateChronos = "http://localhost:3000/chrono/";
        public static string DeleteChrono = "http://localhost:3000/chrono/";
        #endregion

        #region CATEGORIES
        public static string GetMyCategories = "http://localhost:3000/categories/mycategories";
        public static string CreateCategory = "http://localhost:3000/categories/";
        public static string DeleteCategory = "http://localhost:3000/categories/";
        #endregion

        #region USERS
        public static string Login = "http://localhost:3000/auth/login";
        public static string Register = "http://localhost:3000/auth/register"; 
        public static string getUser = "http://localhost:3000/users/myuser";
        public static string getUsers = "http://localhost:3000/users/allusers";
        public static string getRole = "http://localhost:3000/users/myrole";
        #endregion
    }
}
