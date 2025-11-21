using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class RoleBLL
    {
        private RoleDAL _roleDAL;

        public RoleBLL()
        {
            _roleDAL = new RoleDAL();
        }

        public bool AddNewRole(string roleName, out string error)
        {
            // 1. Kiểm tra nghiệp vụ (Validation)
            if (string.IsNullOrWhiteSpace(roleName))
            {
                error = "Tên vai trò không được để trống!";
                return false;
            }

            // 2. Đóng gói dữ liệu vào DTO
            RoleDTO newRole = new RoleDTO
            {
                RoleName = roleName
            };

            // 3. Gọi DAL xử lý
            return _roleDAL.AddRole(newRole, out error);
        }

        public List<RoleDTO> GetAllRoles(string keyword = "")
        {
            var roles = _roleDAL.GetAllRoles();

            if (!string.IsNullOrEmpty(keyword))
            {
                roles = roles.Where(r => r.RoleName.ToLower().Contains(keyword.ToLower())).ToList();
            }

            return roles.Select(r => new RoleDTO
            {
                Uid = r.Uid,
                RoleName = r.RoleName,
                CreatedAt = r.CreatedAt
            }).ToList();
        }
        public bool DeleteRole(int roleId)
        {
            return _roleDAL.DeleteRole(roleId);
        }

        
        public List<PermissionMatrixDTO> GetPermissionMatrix()
        {
            var functions = _roleDAL.GetActiveFunctions();
            var permTypes = _roleDAL.GetPermissionTypes();
            var allPerms = _roleDAL.GetAllPermissions(); 

            var matrixList = new List<PermissionMatrixDTO>();

            foreach (var func in functions)
            {
                foreach (var type in permTypes)
                {
                    var dto = new PermissionMatrixDTO
                    {
                        FunctionId = func.Uid,
                        FunctionName = func.Name,
                        PermissionTypeId = type.Id,
                        PermissionTypeName = type.Name
                    };

                    var roles = _roleDAL.GetAllRoles();
                    foreach (var role in roles)
                    {
                        var p = allPerms.FirstOrDefault(x => x.RoleId == role.Uid &&
                                                             x.FunctionId == func.Uid &&
                                                             x.PermissionTypeId == type.Id);

                        bool isAllowed = (p != null && p.Allowed == true);
                        dto.RolePermissions.Add(role.Uid, isAllowed);
                    }

                    matrixList.Add(dto);
                }
            }

            return matrixList;
        }

        public bool UpdatePermission(int roleId, int funcId, int typeId, bool value)
        {
            return _roleDAL.UpdatePermission(roleId, funcId, typeId, value);
        }
    }
}