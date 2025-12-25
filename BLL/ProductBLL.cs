using BLL;
using CloudinaryDotNet.Actions;
using DAL;
using DAL.EF;
using DTO;
using Helpers;
using Slugify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class ProductBLL
    {
        private ProductDAL _productDAL;
        private SlugHelper _slugHelper;

        public ProductBLL()
        {
            _productDAL = new ProductDAL();
            _slugHelper = new SlugHelper();
        }

        public List<ProductDTO> GetAllProducts(string keyword,string statusFilter, string sort, int skip, int limit)
        {
            var entities = _productDAL.GetAllProduct(keyword, statusFilter, sort, skip, limit);

            return entities.Select(p => new ProductDTO
            {
                Uid = p.Uid,
                ProductName = p.ProductName,
                BrandUid = p.BrandUid,
                BrandName = p.Brand.BrandName,
                SubCategoryUid = p.SubCategoryUid,
                SubCategoryName = p.ProductSubCategory.SubCategoryName,
                UnitUid = p.UnitUid,
                UnitName = p.Unit.UnitName,
                Price = p.Price,
                Discount = p.Discount,
                Thumbnail = p.Thumbnail,
                Quantity = p.Quantity,
                StockQuantity = p.StockQuantity,
                StockStatusUid = p.StockStatusUid,
                StockStatusName = p.StockStatus.StockName,
                Status = p.Status,
                Slug = p.Slug,
                ManufactureDate = p.ManufactureDate,
                ExpiryDate = p.ExpiryDate,
                IsFeatured = p.IsFeatured,
                Exchangeable = p.Exchangeable,
                Refundable = p.Refundable,
                Position = p.Position,
                Description = p.Description,
                Weight = p.Weight,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public int Count(string keyword, string statusFilter)
        {
            return _productDAL.Count(keyword ,statusFilter);
        }

        public List<ProductCategoryDTO> GetAllCategory()
        {
            var entities = _productDAL.GetAllCategory();

            return entities.Select(x => new ProductCategoryDTO
            {
                Uid = x.Uid,
                CategoryName = x.CategoryName,
                Slug = x.Slug,
                Status = x.Status
            }).ToList();
        }

        public List<ProductSubCategoryDTO> GetSubCategory(int categoryUid)
        {
            var entities = _productDAL.GetSubCategory(categoryUid);

            return entities.Select(x => new ProductSubCategoryDTO
            {
                Uid = x.Uid,
                SubCategoryName = x.SubCategoryName,
                CategoryUid = x.CategoryUid,
                Slug = x.Slug,
                Status = x.Status
            }).ToList();
        }

        public List<BrandDTO> GetAllBrand()
        {
            var entities = _productDAL.GetAllBrand();

            return entities.Select(x => new BrandDTO
            {
                Uid = x.Uid,
                BrandName = x.BrandName,
                Slug = x.Slug,
                Status = x.Status
            }).ToList();
        }

        public List<UnitDTO> GetAllUnit()
        {
            var entities = _productDAL.GetAllUnit();

            return entities.Select(x => new UnitDTO
            {
                Uid = x.Uid,
                UnitName = x.UnitName,
                Status = x.Status
            }).ToList();
        }

        public List<StockStatusDTO> GetAllStockStatus()
        {
            var entities = _productDAL.GetAllStockStatus();

            return entities.Select(x => new StockStatusDTO
            {
                Uid = x.Uid,
                StockName = x.StockName
            }).ToList();
        }


        public bool AddProduct(ProductDTO productDTO)
        {
            string cleanedProductName = productDTO.ProductName.Trim();
            string cleanedSku = productDTO.Sku.Trim();

            if (!_productDAL.IsSkuUnique(cleanedSku, 0))
            {
                MessageBox.Show("SKU already exists, please re-enter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_productDAL.IsProductNameUnique(cleanedProductName, productDTO.SubCategoryUid, 0))
            {
                MessageBox.Show($"The product name '{cleanedProductName}' already exists in this subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_productDAL.IsLockedSubcategory(productDTO.SubCategoryUid))
            {
                throw new Exception($"The subcategory is currently locked; you should switch to a different subcategory or reactivate the subcategory.");
            }

            string finalSlug = productDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(productDTO.ProductName);
            }

            var cloudinaryService = new CloudinaryBLL();

            List<string> uploadedUrls = new List<string>();

            if (!string.IsNullOrEmpty(productDTO.Thumbnail))
            {
                string imageUrl = cloudinaryService.UploadProductThumbnail(productDTO.Thumbnail);

                if (imageUrl != null)
                {
                    uploadedUrls.Add(imageUrl); 
                }
                else
                {
                    return false;
                }
            }

            string jsonThumbnailArray = Newtonsoft.Json.JsonConvert.SerializeObject(uploadedUrls);

            
            int newPosition;
            if (productDTO.Position == null)
            {
                int maxPosition = _productDAL.countPosition();
                newPosition = maxPosition + 1;
            }
            else
            {
                newPosition = productDTO.Position.Value;
            }

            DAL.EF.Product productEntity = new DAL.EF.Product
            {
                ProductName = productDTO.ProductName,
                SubCategoryUid = productDTO.SubCategoryUid,
                BrandUid = productDTO.BrandUid,
                UnitUid = productDTO.UnitUid,
                Price = productDTO.Price ?? 0m,
                Discount = productDTO.Discount ?? 0,
                Thumbnail = jsonThumbnailArray,
                Status = productDTO.Status,
                Position = newPosition,
                Description = productDTO.Description,
                Weight = productDTO.Weight ?? 0.0,
                Slug = finalSlug,
                IsFeatured = productDTO.IsFeatured,
                Exchangeable = productDTO.Exchangeable,
                Refundable = productDTO.Refundable,
                Sku = productDTO.Sku,
                Quantity = productDTO.Quantity ?? 0,
                StockQuantity = productDTO.StockQuantity ?? 0,
                StockStatusUid = productDTO.StockStatusUid,
                ManufactureDate = productDTO.ManufactureDate,
                ExpiryDate = productDTO.ExpiryDate,

                CreatedAt = DateTime.Now,
                Deleted = false
            };

            return _productDAL.AddProduct(productEntity);
        }

        public ProductDTO GetProductById(int productId)
        {
            
            Product productEntity = _productDAL.GetProductById(productId);

            if (productEntity == null)
            {
                return null;
            }
            ProductDTO productDTO = new ProductDTO
            {
                Uid = productEntity.Uid,
                ProductName = productEntity.ProductName,
                CategoryUid = productEntity.ProductSubCategory.CategoryUid,
                SubCategoryUid = productEntity.SubCategoryUid,
                BrandUid = productEntity.BrandUid,
                UnitUid = productEntity.UnitUid,
                StockStatusUid = productEntity.StockStatusUid,
                Price = productEntity.Price,
                Discount = productEntity.Discount,
                Weight = productEntity.Weight,
                Position = productEntity.Position,
                Quantity = productEntity.Quantity,
                StockQuantity = productEntity.StockQuantity,

                // Dữ liệu String/Bool
                Description = productEntity.Description,
                Thumbnail = productEntity.Thumbnail,
                Status = productEntity.Status,
                Sku = productEntity.Sku,
                IsFeatured = productEntity.IsFeatured,
                Exchangeable = productEntity.Exchangeable,
                Refundable = productEntity.Refundable,
                Slug = productEntity.Slug,

                ManufactureDate = productEntity.ManufactureDate,
                ExpiryDate = productEntity.ExpiryDate,
                // Metadata
                CreatedAt = productEntity.CreatedAt,
                UpdatedAt = productEntity.UpdatedAt,
                CreatedBy = productEntity.CreatedBy,
                UpdatedBy = productEntity.UpdatedBy,
                Deleted = productEntity.Deleted
            };

            return productDTO;
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            int currentProductId = productDTO.Uid;
            string cleanedProductName = productDTO.ProductName.Trim();
            string cleanedSku = productDTO.Sku.Trim();

            if (!_productDAL.IsSkuUnique(cleanedSku, 0))
            {
                MessageBox.Show("SKU already exists, please re-enter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_productDAL.IsProductNameUnique(cleanedProductName, productDTO.SubCategoryUid, 0))
            {
                MessageBox.Show($"The product name '{cleanedProductName}' already exists in this subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_productDAL.IsLockedSubcategory(productDTO.SubCategoryUid))
            {
                throw new Exception($"The subcategory is currently locked; you should switch to a different subcategory or reactivate the subcategory.");
            }

            string finalSlug = productDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(cleanedProductName);
            }

            var cloudinaryService = new CloudinaryBLL();
            List<string> finalUrls = new List<string>();

            if (productDTO.Thumbnail != null)
            {
                string thumbnailData = productDTO.Thumbnail;

                if (thumbnailData.StartsWith("http://") || thumbnailData.StartsWith("https://"))
                {
                    finalUrls.Add(thumbnailData);
                }
                else
                {
                    try
                    {
                        string imageUrl = cloudinaryService.UploadProductThumbnail(thumbnailData);

                        if (imageUrl != null)
                        {
                            finalUrls.Add(imageUrl);
                        }
                        else
                        {
                            throw new Exception("Lỗi hệ thống Cloudinary: Tải ảnh lên thất bại.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Lỗi hệ thống khi upload Cloudinary: {ex.Message}", ex);
                    }
                }
            }

            string jsonThumbnailArray = Newtonsoft.Json.JsonConvert.SerializeObject(finalUrls);


            DAL.EF.Product productEntity = new DAL.EF.Product
            {
                Uid = currentProductId,

                ProductName = cleanedProductName,
                SubCategoryUid = productDTO.SubCategoryUid,
                BrandUid = productDTO.BrandUid,
                UnitUid = productDTO.UnitUid,
                Description = productDTO.Description,

                Price = productDTO.Price ?? 0m,
                Discount = productDTO.Discount ?? 0,
                Position = productDTO.Position ?? 0,
                Weight = productDTO.Weight ?? 0.0,
                Quantity = productDTO.Quantity ?? 0,
                StockQuantity = productDTO.StockQuantity ?? 0,
                StockStatusUid = productDTO.StockStatusUid,

                Thumbnail = jsonThumbnailArray,
                Slug = finalSlug,
                Status = productDTO.Status,
                IsFeatured = productDTO.IsFeatured,
                Exchangeable = productDTO.Exchangeable,
                Refundable = productDTO.Refundable,
                Sku = cleanedSku,
                ManufactureDate = productDTO.ManufactureDate,
                ExpiryDate = productDTO.ExpiryDate,

                UpdatedAt = DateTime.Now,
                UpdatedBy = productDTO.UpdatedBy, 
            };

            return _productDAL.UpdateProduct(productEntity);
        }


        public bool DeleteProduct(int productId)
        {
            string deletedByUser = Environment.UserName;


            try
            {
                return _productDAL.SoftDeleteProduct(productId, deletedByUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi nghiệp vụ khi xóa sản phẩm ID {productId}. Chi tiết: {ex.Message}");
            }
        }

        public string updateChangeMulti(string status, List<int> productUids)
        {
            return _productDAL.updateChangeMulti(status, productUids);
        }
    }
}
