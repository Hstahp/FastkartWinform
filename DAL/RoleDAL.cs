using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
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
        // PHẦN 1: CÁC HÀM LẤY DỮ LIỆU (GET)
        // ==========================================

        // 1. Lấy danh sách Loại Quyền (Xem, Thêm, Sửa...)
        public List<PermissionTypes> GetPermissionTypes()
        {
            return _context.PermissionTypes.AsNoTracking().OrderBy(p => p.Id).ToList();
        }

        public List<string> GetPermissionCodesByRole(int roleId)
        {
            // Join 3 bảng để lấy ra chuỗi "FunctionCode.PermissionCode"
            var query = from p in _context.Permissions
                        join f in _context.Functions on p.FunctionId equals f.Uid
                        join t in _context.PermissionTypes on p.PermissionTypeId equals t.Id
                        where p.RoleId == roleId && p.Allowed == true // Chỉ lấy cái nào được phép
                        select f.Code + "." + t.Code;

            return query.ToList();
        }

        // 2. Lấy danh sách Chức năng (Sản phẩm, Đơn hàng...)
        public List<Functions> GetActiveFunctions()
        {
            return _context.Functions.AsNoTracking()
                           .Where(f => f.Deleted == false)
                           .OrderBy(f => f.Name)
                           .ToList();
        }

        // 3. Lấy tất cả danh sách Role (Chưa bị xóa)
        public List<Roles> GetAllRoles()
        {
            return _context.Roles.AsNoTracking()
                           .Where(r => r.Deleted == false)
                           .OrderByDescending(r => r.CreatedAt)
                           .ToList();
        }

        // 4. Lấy tất cả bảng Phân quyền hiện có
        public List<Permissions> GetAllPermissions()
        {
            return _context.Permissions.AsNoTracking().ToList();
        }


        // ==========================================
        // PHẦN 2: CÁC HÀM THAO TÁC (ADD, UPDATE, DELETE)
        // ==========================================

        // 5. Thêm Role mới (Chỉ tên)
        public bool AddRole(RoleDTO roleDto, out string error)
        {
            error = "";
            try
            {
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

        // 6. Cập nhật quyền (Dùng cho Matrix)
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
                    perm.Allowed = allowed; // Cập nhật
                }
                else
                {
                    // Thêm mới nếu chưa có
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

        // 7. Xóa Role (Xóa mềm)
        public bool DeleteRole(int roleId)
        {
            try
            {
                var role = _context.Roles.Find(roleId);
                if (role != null)
                {
                    role.Deleted = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}