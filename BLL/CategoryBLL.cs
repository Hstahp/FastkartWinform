using CloudinaryDotNet.Actions;
using DAL;
using DAL.EF;
using DTO;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class CategoryBLL
    {
        private CategoryDAL _categoryDAL;
        private SlugHelper _slugHelper;

        public CategoryBLL()
        {
            _categoryDAL = new CategoryDAL();
            _slugHelper = new SlugHelper();
        }

        public List<ProductCategoryDTO> GetAllCategory(string keyword, string statusFilter, string sort, int skip, int limit)
        {
            var entities = _categoryDAL.GetAllCategory(keyword, statusFilter, sort, skip, limit);

            return entities.Select(p => new ProductCategoryDTO
            {
                Uid = p.Uid,
                CategoryName = p.CategoryName,
                Thumbnail = p.Thumbnail,
                Status = p.Status,
                Slug = p.Slug,
                Position = p.Position,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public int Count(string keyword, string statusFilter)
        {
            return _categoryDAL.Count(keyword, statusFilter);
        }

        public bool AddCategory(ProductCategoryDTO productCategoryDTO)
        {
            string cleanedProductName = productCategoryDTO.CategoryName.Trim();

            if (_categoryDAL.checkCategoryName(0, cleanedProductName))
            {
                MessageBox.Show($"Category name '{cleanedProductName}' already exists in this Subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string finalSlug = productCategoryDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(productCategoryDTO.CategoryName);
            }

            var cloudinaryService = new CloudinaryBLL();

            List<string> uploadedUrls = new List<string>();

            if (!string.IsNullOrEmpty(productCategoryDTO.Thumbnail))
            {
                string imageUrl = cloudinaryService.UploadProductThumbnail(productCategoryDTO.Thumbnail);

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
            if (productCategoryDTO.Position == null)
            {
                int maxPosition = _categoryDAL.countPosition();
                newPosition = maxPosition + 1;
            }
            else
            {
                newPosition = productCategoryDTO.Position.Value;
            }

            DAL.EF.ProductCategory categoryEntity = new DAL.EF.ProductCategory
            {
                CategoryName = productCategoryDTO.CategoryName,
                Thumbnail = jsonThumbnailArray,
                Status = productCategoryDTO.Status,
                Position = newPosition,
                Description = productCategoryDTO.Description,
                Slug = finalSlug,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false
            };

            return _categoryDAL.Add(categoryEntity);
        }

        public ProductCategoryDTO GetCategoryById(int categoryId)
        {

            ProductCategory categoryEntity = _categoryDAL.GetCategoryById(categoryId);

            if (categoryEntity == null)
            {
                return null;
            }
            ProductCategoryDTO categoryDTO = new ProductCategoryDTO
            {
                Uid = categoryEntity.Uid,
                CategoryName = categoryEntity.CategoryName,
                Position = categoryEntity.Position,
                Description = categoryEntity.Description,
                Thumbnail = categoryEntity.Thumbnail,
                Status = categoryEntity.Status,
            };

            return categoryDTO;
        }

        public bool Update(ProductCategoryDTO categoryDTO)
        {
            int currentCategoryId = categoryDTO.Uid;
            string cleanedName = categoryDTO.CategoryName.Trim();

            if (_categoryDAL.checkCategoryName(currentCategoryId, cleanedName))
            {
                MessageBox.Show($"Category name '{cleanedName}' already exists in this Subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string finalSlug = categoryDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(cleanedName);
            }

            var cloudinaryService = new CloudinaryBLL();
            List<string> finalUrls = new List<string>();

            if (categoryDTO.Thumbnail != null)
            {
                string thumbnailData = categoryDTO.Thumbnail;

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
                        throw new Exception($"System error when uploading Cloudinary: { ex.Message}", ex);
                    }
                }
            }

            string jsonThumbnailArray = Newtonsoft.Json.JsonConvert.SerializeObject(finalUrls);


            DAL.EF.ProductCategory categoryEntity = new DAL.EF.ProductCategory
            {
                Uid = currentCategoryId,

                CategoryName = cleanedName,
                Description = categoryDTO.Description,
                Position = categoryDTO.Position ?? 0,
                Thumbnail = jsonThumbnailArray,
                Slug = finalSlug,
                Status = categoryDTO.Status,

                UpdatedAt = categoryDTO.UpdatedAt,
                UpdatedBy = categoryDTO.UpdatedBy,
            };

            return _categoryDAL.Update(categoryEntity);
        }

        public int Delete(int id)
        {
            try
            {
                return _categoryDAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Business error when deleting ID category {id}. Detail: {ex.Message}");
            }
        }

        public List<(string name, int count)> updateChangeMulti(string status, List<int> uids)
        {
            return _categoryDAL.updateChangeMulti(status, uids);
        }
    }
}
