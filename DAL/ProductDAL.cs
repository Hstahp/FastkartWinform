using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DAL
{
    public class ProductDAL
    {
        private ApplicationDBConnect _context;

        public ProductDAL() { 
            _context = new ApplicationDBConnect();
        }

        public List<Product> GetAllProduct(string statusFilter, string sort, int skip, int limit) {
            try
            {
                var query = _context.Product
                    .Include("ProductSubCategory")
                    .Where(p => !p.Deleted);

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
                } else
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
                return new List<Product>();
            }
        }

        public int Count(string statusFilter)
        {
            try
            {
                var query = _context.Product.AsQueryable();

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(p => p.Status == statusFilter);
                }

                query = query.Where(p => !p.Deleted);
                return query.Count();
            }catch(Exception ex)
            {
                return 0;
            }
        }

        public List<ProductCategory> GetAllCategoy()
        {
            try
            {
                // Kiểm tra context
                if (_context == null)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: DbContext is null in GetAllCategoy");
                    return new List<ProductCategory>();
                }

                // Kiểm tra DbSet
                if (_context.ProductCategory == null)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: ProductCategory DbSet is null");
                    return new List<ProductCategory>();
                }

                // Lấy dữ liệu
                var categories = _context.ProductCategory
                    .Where(c => !c.Deleted) // Lọc bỏ deleted
                    .OrderBy(c => c.Position)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"GetAllCategoy: Found {categories.Count} categories");

                return categories ?? new List<ProductCategory>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in GetAllCategoy: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                return new List<ProductCategory>();
            }
        }

        public List<ProductSubCategory> GetSubCategory(int id)
        {
            try
            {
                return _context.ProductSubCategory.Where(t => t.CategoryUid == id && t.Status == "Active" && !t.Deleted).AsNoTracking().ToList();
            }
            catch
            {
                return new List<ProductSubCategory>();
            }
        }

        public List<Brand> getAllBrand()
        {
            try
            {
                return _context.Brand.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return new List<Brand>();
            }
        }

        public List<Unit> GetAllUnit()
        {
            try
            {
                return _context.Unit.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return new List<Unit>();
            }
        }

        public List<StockStatus> GetAllStockStatus()
        {
            try
            {
                return _context.StockStatus.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                return new List<StockStatus>();
            }
        }

        public bool AddProduct(Product product)
        {
            try
            {
                if (product.ProductName != null)
                {
                    product.ProductName = product.ProductName.Trim();
                }
                if (product.Sku != null)
                {
                    product.Sku = product.Sku.Trim();
                }

                _context.Product.Add(product);

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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi DB chung: {ex.Message}", "LỖI");
                return false;
            }
        }

        public int countPosition()
        {
            return _context.Product.Count(p => !p.Deleted);
        }

        public bool IsProductNameUnique(string productName, int subCategoryUid, int currentProductId = 0)
        {
            return !_context.Product.Any(p =>
                p.ProductName.ToLower() == productName.ToLower() &&
                p.SubCategoryUid == subCategoryUid &&
                p.Uid != currentProductId);
        }

        public bool IsSkuUnique(string sku, int currentProductId = 0)
        {
            return !_context.Product.Any(p =>
                p.Sku.ToLower() == sku.ToLower() &&
                p.Uid != currentProductId);
        }

        public Product GetProductById(int productId)
        {
            return _context.Product.AsNoTracking().FirstOrDefault(p => p.Uid == productId && p.Deleted == false);
        }

        public ProductSubCategory GetSubCategoryById(int subCategoryUid)
        {
            return _context.ProductSubCategory
                           .Include(s => s.ProductCategory)
                           .FirstOrDefault(s => s.Uid == subCategoryUid);
        }

        public Brand GetBrandById(int brandUid)
        {
            return _context.Brand
                           .FirstOrDefault(b => b.Uid == brandUid);
        }

        public bool UpdateProduct(DAL.EF.Product productEntity)
        {
            try
            {
                var entityToUpdate = _context.Product.Find(productEntity.Uid);

                if (entityToUpdate == null)
                {
                    return false;
                }

                entityToUpdate.ProductName = productEntity.ProductName;
                entityToUpdate.Price = productEntity.Price;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.SubCategoryUid = productEntity.SubCategoryUid;
                entityToUpdate.BrandUid = productEntity.BrandUid;
                entityToUpdate.UnitUid = productEntity.UnitUid;

                entityToUpdate.Discount = productEntity.Discount;
                entityToUpdate.Position = productEntity.Position;
                entityToUpdate.Weight = productEntity.Weight;
                entityToUpdate.StockQuantity = productEntity.StockQuantity;
                entityToUpdate.StockStatusUid = productEntity.StockStatusUid;

                entityToUpdate.Thumbnail = productEntity.Thumbnail;
                entityToUpdate.Slug = productEntity.Slug;
                entityToUpdate.Status = productEntity.Status;
                entityToUpdate.IsFeatured = productEntity.IsFeatured;
                entityToUpdate.Exchangeable = productEntity.Exchangeable;
                entityToUpdate.Refundable = productEntity.Refundable;
                entityToUpdate.Sku = productEntity.Sku;

                int rowsAffected = _context.SaveChanges();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error during UpdateProduct: " + ex.Message, ex);

                return false;
            }
        }

    public bool SoftDeleteProduct(int productId, string deletedBy)
        {
            try
            {
                var productToModify = _context.Product.FirstOrDefault(p => p.Uid == productId);

                if (productToModify == null)
                {
                    return true; 
                }

                productToModify.Deleted = true;
                productToModify.UpdatedAt = DateTime.Now; 

                _context.Entry(productToModify).State = EntityState.Modified;

                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
