using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class CategoryDAL
    {
        private ApplicationDBConnect _context;

        public CategoryDAL()
        {
            _context = new ApplicationDBConnect();
        }

        public List<ProductCategory> GetAllCategory(string keyword, string statusFilter, string sort, int skip, int limit)
        {
            try
            {
                var query = _context.ProductCategory
                    .Where(p => !p.Deleted);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query = query.Where(p => p.CategoryName.Contains(keyword));
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(p => p.Status == statusFilter);
                }

                if (!string.IsNullOrEmpty(sort))
                {
                    var sortArray = sort.Split('-');
                    string sortKey = sortArray[0];
                    string sortValue = sortArray[1];

                    query = query.OrderBy($"{sortKey} {(sortValue == "desc" ? "descending" : "ascending")}");
                }
                else
                {
                    query = query.OrderByDescending(p => p.Position);
                }

                return query
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

            }
            catch (Exception ex)
            {
                return new List<ProductCategory>();
            }
        }

        public int Count(string keyword, string status)
        {
            try
            {
                var query = _context.ProductCategory.AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                    query = query.Where(p => p.CategoryName.Contains(keyword));

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

        public int countPosition()
        {
            return _context.Product.Count(p => !p.Deleted);
        }

        public bool Add(ProductCategory category)
        {
            try
            {
                if (category.CategoryName != null)
                {
                    category.CategoryName = category.CategoryName.Trim();
                }

                _context.ProductCategory.Add(category);

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

        public ProductCategory GetCategoryById(int id)
        {
            try
            {
                return _context.ProductCategory.SingleOrDefault(p => p.Deleted == false && p.Uid == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(ProductCategory categoryEntity)
        {
            try
            {
                var entityToUpdate = _context.ProductCategory.Find(categoryEntity.Uid);

                if (entityToUpdate == null)
                {
                    return false;
                }

                entityToUpdate.CategoryName = categoryEntity.CategoryName;
                entityToUpdate.Description = categoryEntity.Description;
                entityToUpdate.Position = categoryEntity.Position;
                entityToUpdate.Thumbnail = categoryEntity.Thumbnail;
                entityToUpdate.Slug = categoryEntity.Slug;
                entityToUpdate.Status = categoryEntity.Status;
                entityToUpdate.UpdatedAt = categoryEntity.UpdatedAt;
                entityToUpdate.UpdatedBy = categoryEntity.UpdatedBy;

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
                var productSubCategory = _context.ProductSubCategory.Count(p => p.CategoryUid == id && !p.Deleted);
                if (productSubCategory > 0)
                {
                    return productSubCategory;
                }
                else
                {
                    var productCategory = _context.ProductCategory.SingleOrDefault(p => p.Uid == id && !p.Deleted);

                    productCategory.Deleted = true;
                    productCategory.UpdatedAt = DateTime.Now;
                    productCategory.UpdatedBy = Environment.UserName;
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
                var categries = _context.ProductCategory.Where(p => uids.Contains(p.Uid)).ToList();
                List<(string name, int count)> errorList = new List<(string name, int count)>();
                switch (status)
                {
                    case "Active":
                    case "Inactive":
                        foreach (var p in categries)
                        {
                            p.Status = status;
                            p.UpdatedAt = DateTime.Now;
                            p.UpdatedBy = Environment.UserName;
                        }
                        _context.SaveChanges();
                        return errorList;
                    case "Delete":
                        
                        foreach (var category in categries)
                        {
                            var productSubCategory = _context.ProductSubCategory.Count(p => p.CategoryUid == category.Uid && !p.Deleted);
                            if (productSubCategory > 0)
                            {
                                errorList.Add((category.CategoryName, productSubCategory));
                            } else
                            {
                                category.Deleted = true;
                                category.UpdatedAt = DateTime.Now;
                                category.UpdatedBy = Environment.UserName;
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

        public bool checkCategoryName(int id, string name)
        {
            try
            {
                var normalizedName = name.Trim().ToLower();
                if (id > 0)
                {
                    return _context.ProductCategory.Any(p => p.Uid != id && p.CategoryName.ToLower() == normalizedName && !p.Deleted);
                }
                else
                {
                    return _context.ProductCategory.Any(p => p.CategoryName.ToLower() == normalizedName && !p.Deleted);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
