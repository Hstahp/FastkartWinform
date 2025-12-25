using BLL;
using DAL;
using DAL.EF;
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

        public List<Product> GetAllProducts(string statusFilter, string sort, int skip, int limit)
        {
            var products = _productDAL.GetAllProduct(statusFilter, sort, skip, limit);

            return products;
        }

        public int Count(string statusFilter)
        {
            return _productDAL.Count(statusFilter);
        }

        public List<ProductCategory> GetAllCategoy()
        {
            return _productDAL.GetAllCategoy();
        }

        public List<ProductSubCategory> GetSubCategory(int id)
        {
            return _productDAL.GetSubCategory(id);
        }

        public List<Brand> getAllBrand()
        {
            return _productDAL.getAllBrand();
        }

        public List<Unit> GetAllUnit()
        {
            return _productDAL.GetAllUnit();
        }
        public List<StockStatus> GetAllStockStatus()
        {
            return _productDAL.GetAllStockStatus();
        }


        public bool AddProduct(DTO.Product productDTO)
        {
            string cleanedProductName = productDTO.ProductName.Trim();
            string cleanedSku = productDTO.Sku.Trim().ToUpper(); // ✅ UPPERCASE SKU

            if (!_productDAL.IsSkuUnique(cleanedSku, 0))
            {
                MessageBox.Show("SKU đã tồn tại, vui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_productDAL.IsProductNameUnique(cleanedProductName, productDTO.SubCategoryUid, 0))
            {
                MessageBox.Show($"Tên sản phẩm '{cleanedProductName}' đã tồn tại trong Danh mục phụ này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string finalSlug = productDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(productDTO.ProductName);
            }

            var cloudinaryService = new CloudinaryBLL();
            var qrCodeService = new QRCodeBLL(); 

            List<string> uploadedUrls = new List<string>();

            // Upload ảnh sản phẩm
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

          
            string qrCodeUrl = null;
            try
            {
                // Generate label với thông tin đầy đủ
                string labelBase64 = qrCodeService.GenerateLabelBase64(
                    cleanedSku,
                    cleanedProductName,
                    productDTO.Price ?? 0
                );

                if (!string.IsNullOrEmpty(labelBase64))
                {
                    // Upload lên Cloudinary
                    qrCodeUrl = cloudinaryService.UploadProductThumbnail(labelBase64);
                }
            }
            catch (Exception ex)
            {
             
                System.Diagnostics.Debug.WriteLine($"QR Code generation failed: {ex.Message}");
            }

            int maxPosition = _productDAL.countPosition();
            int newPosition = maxPosition + 1;

            DAL.EF.Product productEntity = new DAL.EF.Product
            {
                ProductName = productDTO.ProductName,
                SubCategoryUid = productDTO.SubCategoryUid,
                BrandUid = productDTO.BrandUid,
                UnitUid = productDTO.UnitUid,
                Price = productDTO.Price ?? 0m,
                Discount = productDTO.Discount ?? 0,
                Thumbnail = jsonThumbnailArray,
                QRCodeUrl = qrCodeUrl, 
                Status = productDTO.Status,
                Position = newPosition,
                Description = productDTO.Description,
                Weight = productDTO.Weight ?? 0.0,
                Slug = finalSlug,
                IsFeatured = productDTO.IsFeatured,
                Exchangeable = productDTO.Exchangeable,
                Refundable = productDTO.Refundable,
                Sku = cleanedSku,
                StockQuantity = productDTO.StockQuantity ?? 0,
                StockStatusUid = productDTO.StockStatusUid,

                CreatedAt = DateTime.Now,
                Deleted = false
            };

            return _productDAL.AddProduct(productEntity);
        }

        public DTO.Product GetProductById(int productId)
        {
            DAL.EF.Product productEntity = _productDAL.GetProductById(productId);

            if (productEntity == null)
            {
                return null;
            }
            
            DTO.Product productDTO = new DTO.Product
            {
                Uid = productEntity.Uid,
                ProductName = productEntity.ProductName,

                SubCategoryUid = productEntity.SubCategoryUid,
                BrandUid = productEntity.BrandUid,
                UnitUid = productEntity.UnitUid,
                StockStatusUid = productEntity.StockStatusUid,

                SubCategory = MapToSubCategoryDTO(productEntity.ProductSubCategory),
                Brand = MapToBrandDTO(productEntity.Brand),

                Price = productEntity.Price,
                Discount = productEntity.Discount,
                Weight = productEntity.Weight,
                Position = productEntity.Position,
                StockQuantity = productEntity.StockQuantity,

                // ✅ THÊM MAPPING CHO QRCODEURL
                QRCodeUrl = productEntity.QRCodeUrl,

                Description = productEntity.Description,
                Thumbnail = productEntity.Thumbnail, 
                Status = productEntity.Status,
                Sku = productEntity.Sku,
                IsFeatured = productEntity.IsFeatured,
                Exchangeable = productEntity.Exchangeable,
                Refundable = productEntity.Refundable,
                Slug = productEntity.Slug,

                CreatedAt = productEntity.CreatedAt,
                UpdatedAt = productEntity.UpdatedAt,
                CreatedBy = productEntity.CreatedBy,
                UpdatedBy = productEntity.UpdatedBy,
                Deleted = productEntity.Deleted
            };

            return productDTO;
        }

        private DTO.ProductSubCategory MapToSubCategoryDTO(ProductSubCategory entity)
        {
            if (entity == null) return null;
            return new DTO.ProductSubCategory
            {
                Uid = entity.Uid,
                SubCategoryName = entity.SubCategoryName,
                CategoryUid = entity.CategoryUid,
            };
        }

        private DTO.Brand MapToBrandDTO(DAL.EF.Brand entity)
        {
            if (entity == null) return null;
            return new DTO.Brand
            {
                Uid = entity.Uid,
                BrandName = entity.BrandName,
            };
        }

        public bool UpdateProduct(DTO.Product productDTO)
        {
            int currentProductId = productDTO.Uid;
            string cleanedProductName = productDTO.ProductName.Trim();
            string cleanedSku = productDTO.Sku.Trim().ToUpper();

            
            var existingProduct = _productDAL.GetProductById(currentProductId);
            if (existingProduct == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }

            string oldSku = existingProduct.Sku?.ToUpper();
            bool skuChanged = (oldSku != cleanedSku);

            if (!_productDAL.IsSkuUnique(cleanedSku, currentProductId))
            {
                throw new Exception($"SKU '{cleanedSku}' đã tồn tại.");
            }

            if (!_productDAL.IsProductNameUnique(cleanedProductName, productDTO.SubCategoryUid, currentProductId))
            {
                throw new Exception($"Tên sản phẩm '{cleanedProductName}' đã tồn tại trong Danh mục phụ này.");
            }

            string finalSlug = productDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(cleanedProductName);
            }

            var cloudinaryService = new CloudinaryBLL();
            List<string> finalUrls = new List<string>();

            // Upload ảnh mới (nếu có)
            if (!string.IsNullOrEmpty(productDTO.Thumbnail))
            {
                string thumbnailData = productDTO.Thumbnail;

                if (Uri.IsWellFormedUriString(thumbnailData, UriKind.Absolute))
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

            // ✅ TẠO LẠI QR CODE NẾU SKU THAY ĐỔI
            string qrCodeUrl = existingProduct.QRCodeUrl; // Giữ nguyên URL cũ

            if (skuChanged)
            {
                try
                {
                    var qrCodeService = new QRCodeBLL();
                    string labelBase64 = qrCodeService.GenerateLabelBase64(
                        cleanedSku,
                        cleanedProductName,
                        productDTO.Price ?? 0
                    );

                    if (!string.IsNullOrEmpty(labelBase64))
                    {
                        qrCodeUrl = cloudinaryService.UploadProductThumbnail(labelBase64);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"QR Code regeneration failed: {ex.Message}");
                }
            }

            DAL.EF.Product productEntity = new DAL.EF.Product
            {
                Uid = currentProductId,
                ProductName = cleanedProductName,
                SubCategoryUid = productDTO.SubCategoryUid,
                BrandUid = productDTO.BrandUid,
                UnitUid = productDTO.UnitUid,
                Price = productDTO.Price ?? 0m,
                Discount = productDTO.Discount ?? 0,
                Position = productDTO.Position ?? 0,
                Weight = productDTO.Weight ?? 0.0,
                StockQuantity = productDTO.StockQuantity ?? 0,
                StockStatusUid = productDTO.StockStatusUid,
                Thumbnail = jsonThumbnailArray,
                QRCodeUrl = qrCodeUrl, 
                Slug = finalSlug,
                Status = productDTO.Status,
                IsFeatured = productDTO.IsFeatured,
                Exchangeable = productDTO.Exchangeable,
                Refundable = productDTO.Refundable,
                Sku = cleanedSku,
                UpdatedAt = DateTime.Now,
                UpdatedBy = productDTO.UpdatedBy,
                Deleted = productDTO.Deleted
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
        /// <summary>
        /// Tự động tạo QR Code cho tất cả products chưa có QRCodeUrl
        /// </summary>
        public int GenerateQRCodeForAllProducts()
        {
            var qrCodeService = new QRCodeBLL();
            var cloudinaryService = new CloudinaryBLL();

            // Lấy tất cả products chưa có QR Code
            var allProducts = _productDAL.GetAllProduct("", "Uid-asc", 0, 10000);
            var productsWithoutQR = allProducts
                .Where(p => string.IsNullOrEmpty(p.QRCodeUrl) && !p.Deleted)
                .ToList();

            int successCount = 0;
            int failCount = 0;

            foreach (var product in productsWithoutQR)
            {
                try
                {
                    // Generate QR Label
                    string labelBase64 = qrCodeService.GenerateLabelBase64(
                        product.Sku,
                        product.ProductName,
                        product.Price ?? 0
                    );

                    if (!string.IsNullOrEmpty(labelBase64))
                    {
                        // Upload to Cloudinary
                        string qrUrl = cloudinaryService.UploadProductThumbnail(labelBase64);

                        if (!string.IsNullOrEmpty(qrUrl))
                        {
                            // Update database
                            product.QRCodeUrl = qrUrl;
                            product.UpdatedAt = DateTime.Now;

                            bool updated = _productDAL.UpdateProduct(product);
                            if (updated)
                            {
                                successCount++;
                                System.Diagnostics.Debug.WriteLine($"✅ Generated QR for: {product.Sku} - {product.ProductName}");
                            }
                            else
                            {
                                failCount++;
                                System.Diagnostics.Debug.WriteLine($"❌ Failed to update DB for: {product.Sku}");
                            }
                        }
                        else
                        {
                            failCount++;
                            System.Diagnostics.Debug.WriteLine($"❌ Cloudinary upload failed for: {product.Sku}");
                        }
                    }
                    else
                    {
                        failCount++;
                        System.Diagnostics.Debug.WriteLine($"❌ QR generation failed for: {product.Sku}");
                    }
                }
                catch (Exception ex)
                {
                    failCount++;
                    System.Diagnostics.Debug.WriteLine($"❌ Exception for {product.Sku}: {ex.Message}");
                }
            }

            System.Diagnostics.Debug.WriteLine($"\n📊 SUMMARY: Success={successCount}, Failed={failCount}, Total={productsWithoutQR.Count}");
            return successCount;
        }
    }
}
