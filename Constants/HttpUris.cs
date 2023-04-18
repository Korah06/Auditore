using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Constants
{
    public static class HttpUris
    {
        public static string GetMyTasks = "http://localhost:3000/tasks/mytasks";
        public static string DeleteTask = "http://localhost:3000/tasks/";
        public static string ModifyTask = "http://localhost:3000/tasks/";
        public static string UpdateMyTasks = "http://localhost:3000/tasks/updatetasks";
        public static string CreateTask = "http://localhost:3000/tasks/";
        public static string GetMyCategories = "http://localhost:3000/categories/mycategories";
        public static string CreateCategory = "http://localhost:3000/categories/";
        public static string Login = "http://localhost:3000/auth/login";
        public static string Register = "http://localhost:3000/auth/register";
    }
}
