using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Constants
{
    public static class HttpUris
    {
        #region BASIC URIS

#if ANDROID || IOS
        private static readonly string uri = "http://192.168.218.124:3000/";
#else
        private static readonly string uri = "http://localhost:3000/";
#endif


        private static readonly string auth = uri + "auth/";
        private static readonly string users = uri + "users/";
        private static readonly string tasks = uri + "tasks/";
        private static readonly string chronos = uri + "chrono/";
        private static readonly string categories = uri + "categories/";
        private static readonly string diagnostics = uri + "diagnostics/"; 
        #endregion

        #region TASKS
        public static string GetMyTasks = tasks+"mytasks";
        public static string GetMyCategoryTasks = tasks + "mytasksCategory";
        public static string DeleteTask = tasks;
        public static string ModifyTask = tasks;
        public static string UpdateMyTasks = tasks + "updatetasks";
        public static string CreateTask = tasks;
        #endregion

        #region CHRONOS
        public static string GetChronos = chronos;
        public static string CreateChronos = chronos;
        public static string DeleteChrono = chronos;
        #endregion

        #region CATEGORIES
        public static string GetMyCategories = categories+"mycategories";
        public static string CreateCategory = categories;
        public static string DeleteCategory = categories;
        #endregion

        #region USERS
        public static string Login = auth+"login";
        public static string Register = auth+"register"; 

        public static string getUser = users+"myuser";
        public static string getUsers = users + "allusers";
        public static string deleteUser = users;
        public static string modifyUser = users;
        public static string getRole = users + "myrole";
        #endregion

        #region DIAGNOSTICS
        public static string CreateDiagnostic = diagnostics + "create";
        public static string GetDiagnostics = diagnostics;
        public static string GetDiagnostic = diagnostics + "single";
        #endregion

    }
}
