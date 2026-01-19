using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF; // Assuming EF context is here
using DTO;

namespace DAL
{
    public class RoleDAL
    {
        private ApplicationDBConnect _context;

        public RoleDAL()
        {
            _context = new ApplicationDBConnect();
        }

        // ==========================================
        // PART 1: GET DATA FUNCTIONS
        // ==========================================

        // 1. Get Permission Types (View, Add, Edit...)
        public List<PermissionTypes> GetPermissionTypes()
        {
            return _context.PermissionTypes.AsNoTracking().OrderBy(p => p.Id).ToList();
        }

        public List<string> GetPermissionCodesByRole(int roleId)
        {
            // Join 3 tables to get "FunctionCode.PermissionCode" string
            var query = from p in _context.Permissions
                        join f in _context.Functions on p.FunctionId equals f.Uid
                        join t in _context.PermissionTypes on p.PermissionTypeId equals t.Id
                        where p.RoleId == roleId && p.Allowed == true
                        select f.Code + "." + t.Code;

            return query.ToList();
        }

        // 2. Get Active Functions (Products, Orders...)
        public List<Functions> GetActiveFunctions()
        {
            return _context.Functions.AsNoTracking()
                            .Where(f => f.Deleted == false)
                            .OrderBy(f => f.Name)
                            .ToList();
        }

        // 3. Get All Active Roles
        public List<Roles> GetAllRoles()
        {
            return _context.Roles.AsNoTracking()
                            .Where(r => r.Deleted == false)
                            .OrderByDescending(r => r.CreatedAt)
                            .ToList();
        }

        // 4. Get All Existing Permissions
        public List<Permissions> GetAllPermissions()
        {
            return _context.Permissions.AsNoTracking().ToList();
        }

        // ==========================================
        // PART 2: MANIPULATION FUNCTIONS (ADD, UPDATE, DELETE)
        // ==========================================

        // 5. Add New Role
        public bool AddRole(RoleDTO roleDto, out string error)
        {
            error = "";
            try
            {
                // Validate duplicate name for Add
                bool isDuplicate = _context.Roles.Any(r => r.RoleName == roleDto.RoleName.Trim() && r.Deleted == false);
                if (isDuplicate)
                {
                    error = "Role name already exists. Please choose another name.";
                    return false;
                }

                var roleEntity = new Roles
                {
                    RoleName = roleDto.RoleName.Trim(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Deleted = false
                };

                _context.Roles.Add(roleEntity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 6. Update Permission (For Matrix)
        public bool UpdatePermission(int roleId, int functionId, int permTypeId, bool allowed)
        {
            try
            {
                var perm = _context.Permissions
                                   .FirstOrDefault(p => p.RoleId == roleId &&
                                                        p.FunctionId == functionId &&
                                                        p.PermissionTypeId == permTypeId);

                if (perm != null)
                {
                    perm.Allowed = allowed;
                }
                else
                {
                    _context.Permissions.Add(new Permissions
                    {
                        RoleId = roleId,
                        FunctionId = functionId,
                        PermissionTypeId = permTypeId,
                        Allowed = allowed
                    });
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 7. Delete Role (Standard Logic: Prevent if in use)
        public bool DeleteRole(int roleId, out string error)
        {
            error = "";
            try
            {
                bool isInUse = _context.Users.Any(u => u.RoleUid == roleId && u.Deleted == false);

                if (isInUse)
                {
                    error = "Cannot delete this Role because it is currently assigned to one or more Users.";
                    return false;
                }

                // LOGIC 2: Soft Delete
                var role = _context.Roles.Find(roleId);
                // Hoặc nếu bạn dùng Uid: var role = _context.Roles.FirstOrDefault(r => r.Uid == roleId);

                if (role != null)
                {
                    role.Deleted = true;
                    role.UpdatedAt = DateTime.Now; // Nên cập nhật ngày xóa
                    _context.SaveChanges();
                    return true;
                }

                error = "Role not found.";
                return false;
            }
            catch (Exception ex)
            {
                error = "Delete error: " + ex.Message;
                return false;
            }
        }

        // 8. UPDATE ROLE (New Feature)
        public bool UpdateRole(RoleDTO roleDto, out string error)
        {
            error = "";
            try
            {
                if (string.IsNullOrWhiteSpace(roleDto.RoleName))
                {
                    error = "Role name cannot be empty.";
                    return false;
                }

                string newName = roleDto.RoleName.Trim();

                // Dùng Uid thay vì Id
                var roleEntity = _context.Roles.FirstOrDefault(r => r.Uid == roleDto.Uid);

                if (roleEntity == null || roleEntity.Deleted == true)
                {
                    error = "Role does not exist or has been deleted.";
                    return false;
                }

                // Check duplicate (Exclude current Uid)
                bool isDuplicate = _context.Roles.Any(r => r.RoleName == newName
                                                        && r.Uid != roleDto.Uid
                                                        && r.Deleted == false);

                if (isDuplicate)
                {
                    error = $"Role name '{newName}' already exists.";
                    return false;
                }

                roleEntity.RoleName = newName;
                roleEntity.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = "Update error: " + ex.Message;
                return false;
            }
        }


    }
}