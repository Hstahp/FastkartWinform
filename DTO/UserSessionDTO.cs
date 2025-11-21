using System.Collections.Generic;

namespace DTO
{
    public static class UserSessionDTO
    {
        public static UserDTO CurrentUser { get; set; }

        
        public static HashSet<string> Permissions { get; set; } = new HashSet<string>();

        
        public static bool HasPermission(string functionCode, string permissionCode)
        {
             if (CurrentUser.RoleUid == 1) return true;

            string key = $"{functionCode}.{permissionCode}".ToUpper();
            return Permissions.Contains(key);
        }

        public static void Clear()
        {
            CurrentUser = null;
            Permissions.Clear();
        }
    }
}