using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class SubCategoryDAL
    {
        private ApplicationDBConnect _context;

        public SubCategoryDAL()
        {
            _context = new ApplicationDBConnect();
        }

        public List<ProductSubCategory> GetAllSubCategory(string keyword, string statusFilter, int skip, int limit)
        {
            try
            {
                var query = _context.ProductSubCategory
                    .Include("ProductCategory")
                    .Where(p => !p.Deleted);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(p => p.SubCategoryName.Contains(keyword));
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(p => p.Status == statusFilter);
                }

                return query
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

            }
            catch (Exception ex)
            {
                return new List<ProductSubCategory>();
            }
        }

        public int Count(string keyword, string status)
        {
            try
            {
                var query = _context.ProductSubCategory.AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                    query = query.Where(p => p.SubCategoryName.Contains(keyword));

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(p => p.Status == status);
                }

                query = query.Where(p => !p.Deleted);
                return query.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<ProductCategory> GetAllCategory()
        {
            try
            {
                return _context.ProductCategory.Where(p => p.Deleted == false).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return new List<ProductCategory>();
            }
        }

        public bool IsLockedCategory(int id)
        {
            return _context.ProductCategory.Any(p => p.Uid == id && p.Status == "Inactive" && !p.Deleted);
        }

        public bool Add(ProductSubCategory subcategory)
        {
            try
            {
                if (subcategory.SubCategoryName != null)
                {
                    subcategory.SubCategoryName = subcategory.SubCategoryName.Trim();
                }

                _context.ProductSubCategory.Add(subcategory);

                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"Trường '{x.PropertyName}' bị lỗi: {x.ErrorMessage}");

                var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);

                MessageBox.Show($"Lỗi Kiểm tra Hợp lệ (EF):{Environment.NewLine}{fullErrorMessage}", "LỖI DỮ LIỆU BẮT BUỘC");
                return false;
            }
            catch (DbUpdateException ex)
            {
                string error = ex.InnerException?.InnerException?.Message
                            ?? ex.InnerException?.Message
                            ?? ex.Message;

                MessageBox.Show(error, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public ProductSubCategory GetSubCategoryById(int id)
        {
            try
            {
                return _context.ProductSubCategory.SingleOrDefault(p => p.Deleted == false && p.Uid == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(ProductSubCategory subcategoryEntity)
        {
            try
            {
                var entityToUpdate = _context.ProductSubCategory.Find(subcategoryEntity.Uid);

                if (entityToUpdate == null)
                {
                    return false;
                }

                entityToUpdate.SubCategoryName = subcategoryEntity.SubCategoryName;
                entityToUpdate.CategoryUid = subcategoryEntity.CategoryUid;
                entityToUpdate.Description = subcategoryEntity.Description;
                entityToUpdate.Slug = subcategoryEntity.Slug;
                entityToUpdate.Status = subcategoryEntity.Status;
                entityToUpdate.UpdatedAt = subcategoryEntity.UpdatedAt;
                entityToUpdate.UpdatedBy = subcategoryEntity.UpdatedBy;

                int rowsAffected = _context.SaveChanges();
                return rowsAffected > 0;
            }
            catch (DbUpdateException ex)
            {
                string error = ex.InnerException?.InnerException?.Message
                            ?? ex.InnerException?.Message
                            ?? ex.Message;

                MessageBox.Show(error, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var product = _context.Product.Count(p => p.SubCategoryUid == id && !p.Deleted);
                if (product > 0)
                {
                    return product;
                }
                else
                {
                    var productSubCategory = _context.ProductSubCategory.SingleOrDefault(p => p.Uid == id && !p.Deleted);

                    productSubCategory.Deleted = true;
                    productSubCategory.UpdatedAt = DateTime.Now;
                    productSubCategory.UpdatedBy = Environment.UserName;
                    _context.SaveChanges();

                    return 0;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<(string name, int count)> updateChangeMulti(string status, List<int> uids)
        {
            try
            {
                var subcategries = _context.ProductSubCategory.Where(p => uids.Contains(p.Uid)).ToList();
                List<(string name, int count)> errorList = new List<(string name, int count)>();
                switch (status)
                {
                    case "Active":
                    case "Inactive":
                        foreach (var p in subcategries)
                        {
                            p.Status = status;
                            p.UpdatedAt = DateTime.Now;
                            p.UpdatedBy = Environment.UserName;
                        }
                        _context.SaveChanges();
                        return errorList;
                    case "Delete":

                        foreach (var subcategory in subcategries)
                        {
                            var product = _context.Product.Count(p => p.SubCategoryUid == subcategory.Uid && !p.Deleted);
                            if (product > 0)
                            {
                                errorList.Add((subcategory.SubCategoryName, product));
                            }
                            else
                            {
                                subcategory.Deleted = true;
                                subcategory.UpdatedAt = DateTime.Now;
                                subcategory.UpdatedBy = Environment.UserName;
                            }
                        }

                        _context.SaveChanges();
                        return errorList;
                    default:
                        return errorList;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool checkSubCategoryName(int id, string name)
        {
            try
            {
                var normalizedName = name.Trim().ToLower();
                if (id > 0)
                {
                    return _context.ProductSubCategory.Any(p => p.Uid != id && p.SubCategoryName.ToLower() == normalizedName && !p.Deleted);
                }
                else
                {
                    return _context.ProductSubCategory.Any(p => p.SubCategoryName.ToLower() == normalizedName && !p.Deleted);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
