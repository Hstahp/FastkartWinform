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

        public List<ProductDTO> GetAllProducts(string keyword, string statusFilter, string sort, int skip, int limit)
        {
           
            System.Diagnostics.Debug.WriteLine($"[BLL] GetAllProducts called with:");
            System.Diagnostics.Debug.WriteLine($"  keyword: '{keyword}'");
            System.Diagnostics.Debug.WriteLine($"  statusFilter: '{statusFilter}'");
            System.Diagnostics.Debug.WriteLine($"  sort: '{sort}'");
            System.Diagnostics.Debug.WriteLine($"  skip: {skip}, limit: {limit}");

            var entities = _productDAL.GetAllProduct(keyword, statusFilter, sort, skip, limit);

            System.Diagnostics.Debug.WriteLine($"[BLL] Products from DAL: {entities.Count}");

            // ✅ THÊM: Debug từng product
            foreach (var p in entities.Take(5))
            {
                System.Diagnostics.Debug.WriteLine($"  - Product: Uid={p.Uid}, SKU='{p.Sku}', Name={p.ProductName}, Stock={p.StockQuantity}, Deleted={p.Deleted}");
            }

            var result = entities.Select(p => new ProductDTO
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
                Sku = p.Sku, 
                CreatedAt = p.CreatedAt
            }).ToList();

            System.Diagnostics.Debug.WriteLine($"[BLL] Returned DTOs: {result.Count}");
            foreach (var dto in result.Take(3))
            {
                System.Diagnostics.Debug.WriteLine($"  [DTO] {dto.ProductName}: Price={dto.Price:N0}, Discount={dto.Discount}%");
            }
            return result;
        }

        public int Count(string keyword, string statusFilter)
        {
            return _productDAL.Count(keyword, statusFilter);
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

            // ✅ THÊM: Validate Quantity <= StockQuantity
            int quantity = productDTO.Quantity ?? 0;
            int stockQuantity = productDTO.StockQuantity ?? 0;
            
            if (quantity > stockQuantity)
            {
                MessageBox.Show(
                    $"Current quantity ({quantity}) cannot be greater than stock quantity ({stockQuantity}).",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
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

            // ✅ 2. THÊM: Generate QR Code (GIỐNG NHƯ UpdateProduct)
            string qrCodeUrl = null;
            try
            {
                var qrCodeService = new QRCodeBLL();
                string labelBase64 = qrCodeService.GenerateLabelBase64(
                    cleanedSku.ToUpper(),
                    cleanedProductName,
                    productDTO.Price ?? 0
                );

                if (!string.IsNullOrEmpty(labelBase64))
                {
                    qrCodeUrl = cloudinaryService.UploadProductThumbnail(labelBase64);
                    System.Diagnostics.Debug.WriteLine($"✅ QR Code generated for new product: {qrCodeUrl}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"⚠️ QR Code generation failed for SKU: {cleanedSku}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ QR Code generation error: {ex.Message}");
                // Không fail toàn bộ, chỉ log warning
            }

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
                QRCodeUrl = qrCodeUrl, // ✅ THÊM: Lưu QR Code URL
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

                QRCodeUrl = productEntity.QRCodeUrl,
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
            System.Diagnostics.Debug.WriteLine($"[BLL] GetProductById({productId}):");
            System.Diagnostics.Debug.WriteLine($"  Name: {productDTO.ProductName}");
            System.Diagnostics.Debug.WriteLine($"  Price: {productDTO.Price:N0} | Discount: {productDTO.Discount}%");

            return productDTO;
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            int currentProductId = productDTO.Uid;
            string cleanedProductName = productDTO.ProductName.Trim();
            string cleanedSku = productDTO.Sku.Trim().ToUpper(); 
            
            var existingProduct = _productDAL.GetProductById(currentProductId);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            string oldSku = existingProduct.Sku?.ToUpper();
            bool skuChanged = (oldSku != cleanedSku);
            
            // ✅ DEBUG: Log SKU change detection
            System.Diagnostics.Debug.WriteLine($"");
            System.Diagnostics.Debug.WriteLine($"🔍 ========== UPDATE PRODUCT ==========");
            System.Diagnostics.Debug.WriteLine($"   Product ID: {currentProductId}");
            System.Diagnostics.Debug.WriteLine($"   Product Name: {cleanedProductName}");
            System.Diagnostics.Debug.WriteLine($"   Old SKU: '{oldSku}'");
            System.Diagnostics.Debug.WriteLine($"   New SKU: '{cleanedSku}'");
            System.Diagnostics.Debug.WriteLine($"   SKU Changed: {skuChanged}");
            System.Diagnostics.Debug.WriteLine($"   Old QR URL: {existingProduct.QRCodeUrl}");
            System.Diagnostics.Debug.WriteLine($"=======================================");
            
            // ✅  Truyền currentProductId thay vì 0
            if (!_productDAL.IsSkuUnique(cleanedSku, currentProductId))
            {
                MessageBox.Show("SKU already exists, please re-enter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //  Truyền currentProductId thay vì 0
            if (!_productDAL.IsProductNameUnique(cleanedProductName, productDTO.SubCategoryUid, currentProductId))
            {
                MessageBox.Show($"The product name '{cleanedProductName}' already exists in this subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_productDAL.IsLockedSubcategory(productDTO.SubCategoryUid))
            {
                throw new Exception($"The subcategory is currently locked; you should switch to a different subcategory or reactivate the subcategory.");
            }
            
            int quantity = productDTO.Quantity ?? 0;
            int stockQuantity = productDTO.StockQuantity ?? 0;

            if (quantity > stockQuantity)
            {
                MessageBox.Show(
                    $"Current quantity ({quantity}) cannot be greater than stock quantity ({stockQuantity}).",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
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
                            throw new Exception("Cloudinary system error: Image upload failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"System error uploading to Cloudinary: {ex.Message}", ex);
                    }
                }
            }

            string jsonThumbnailArray = Newtonsoft.Json.JsonConvert.SerializeObject(finalUrls);
            string qrCodeUrl = existingProduct.QRCodeUrl; // Giữ nguyên URL cũ

            // ✅ SỬA: REGENERATE QR CODE KHI SKU THAY ĐỔI (VỚI DEBUG CHI TIẾT)
            if (skuChanged)
            {
                System.Diagnostics.Debug.WriteLine($"");
                System.Diagnostics.Debug.WriteLine($"🔄 ========== QR CODE REGENERATION ==========");
                System.Diagnostics.Debug.WriteLine($"   SKU changed detected! Starting regeneration...");
                
                try
                {
                    var qrCodeService = new QRCodeBLL();
                    
                    // ✅ 1. Generate Label Base64
                    System.Diagnostics.Debug.WriteLine($"   Step 1: Generating Label Base64...");
                    System.Diagnostics.Debug.WriteLine($"      SKU: '{cleanedSku}'");
                    System.Diagnostics.Debug.WriteLine($"      Name: '{cleanedProductName}'");
                    System.Diagnostics.Debug.WriteLine($"      Price: {productDTO.Price ?? 0:N0} VNĐ");
                    
                    string labelBase64 = qrCodeService.GenerateLabelBase64(
                        cleanedSku,
                        cleanedProductName,
                        productDTO.Price ?? 0
                    );

                    if (!string.IsNullOrEmpty(labelBase64))
                    {
                        System.Diagnostics.Debug.WriteLine($"   ✅ Label Base64 generated successfully!");
                        System.Diagnostics.Debug.WriteLine($"      Length: {labelBase64.Length} characters");
                        
                        // ✅ 2. Upload to Cloudinary
                        System.Diagnostics.Debug.WriteLine($"   Step 2: Uploading to Cloudinary...");
                        string newQrCodeUrl = cloudinaryService.UploadProductThumbnail(labelBase64);
                        
                        if (!string.IsNullOrEmpty(newQrCodeUrl))
                        {
                            qrCodeUrl = newQrCodeUrl; // ✅ CẬP NHẬT URL MỚI
                            System.Diagnostics.Debug.WriteLine($"   ✅✅✅ SUCCESS! QR Code uploaded!");
                            System.Diagnostics.Debug.WriteLine($"      Old URL: {existingProduct.QRCodeUrl}");
                            System.Diagnostics.Debug.WriteLine($"      NEW URL: {qrCodeUrl}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"   ❌ FAILED: Cloudinary upload returned NULL!");
                            System.Diagnostics.Debug.WriteLine($"      Keeping old QR URL: {qrCodeUrl}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"   ❌ FAILED: Label Base64 generation returned NULL!");
                        System.Diagnostics.Debug.WriteLine($"      Keeping old QR URL: {qrCodeUrl}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"   ❌❌❌ EXCEPTION during QR Code regeneration!");
                    System.Diagnostics.Debug.WriteLine($"      Message: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"      Stack: {ex.StackTrace}");
                    System.Diagnostics.Debug.WriteLine($"      Keeping old QR URL: {qrCodeUrl}");
                    // ⚠️ Không throw, để product vẫn được update
                }
                
                System.Diagnostics.Debug.WriteLine($"============================================");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"ℹ️ SKU unchanged, keeping old QR Code URL");
            }

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
                QRCodeUrl = qrCodeUrl,
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

        public string updateChangeMulti(string status, List<int> productUids)
        {
            return _productDAL.updateChangeMulti(status, productUids);
        }
        /// <summary>
        /// Tự động tạo QR Code cho tất cả products chưa có QRCodeUrl
        /// </summary>
        public int GenerateQRCodeForAllProducts()
        {
            var qrCodeService = new QRCodeBLL();
            var cloudinaryService = new CloudinaryBLL();

            // ✅ FIX: Lấy tất cả products chưa có QR Code VÀ có SKU hợp lệ
            var allProducts = _productDAL.GetAllProduct("", "", "Uid-asc", 0, 10000);
            var productsWithoutQR = allProducts
                .Where(p => string.IsNullOrEmpty(p.QRCodeUrl)
                            && !p.Deleted
                            && !string.IsNullOrWhiteSpace(p.Sku)) // ✅ THÊM: Chỉ lấy products có SKU
                .ToList();

            int successCount = 0;
            int failCount = 0;

            System.Diagnostics.Debug.WriteLine($"📋 Found {productsWithoutQR.Count} products need QR Code generation");

            foreach (var product in productsWithoutQR)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"🔄 Processing: {product.Sku} - {product.ProductName}");

                    // ✅ Generate QR Label with proper data
                    string labelBase64 = qrCodeService.GenerateLabelBase64(
                        product.Sku.Trim().ToUpper(), // ✅ Normalize SKU
                        product.ProductName,
                        product.Price ?? 0
                    );

                    if (!string.IsNullOrEmpty(labelBase64))
                    {
                        // Upload to Cloudinary
                        string qrUrl = cloudinaryService.UploadProductThumbnail(labelBase64);

                        if (!string.IsNullOrEmpty(qrUrl))
                        {
                            // ✅ Update database
                            product.QRCodeUrl = qrUrl;
                            product.UpdatedAt = DateTime.Now;
                            product.UpdatedBy = Environment.UserName; // ✅ THÊM: Track who updated

                            bool updated = _productDAL.UpdateProduct(product);
                            if (updated)
                            {
                                successCount++;
                                System.Diagnostics.Debug.WriteLine($"✅ Generated QR for: {product.Sku}");
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