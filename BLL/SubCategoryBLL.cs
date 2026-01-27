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
    public class SubCategoryBLL
    {
        private SubCategoryDAL _subCategoryDAL;
        private SlugHelper _slugHelper;

        public SubCategoryBLL()
        {
            _subCategoryDAL = new SubCategoryDAL();
            _slugHelper = new SlugHelper();
        }

        public List<ProductSubCategoryDTO> GetAllSubCategory(string keyword, string statusFilter, int skip, int limit)
        {
            var entities = _subCategoryDAL.GetAllSubCategory(keyword, statusFilter, skip, limit);
            return entities.Select(p => new ProductSubCategoryDTO
            {
                Uid = p.Uid,
                SubCategoryName = p.SubCategoryName,
                CategoryName = p.ProductCategory.CategoryName,
                Status = p.Status,
                Slug = p.Slug,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public int Count(string keyword, string statusFilter)
        {
            return _subCategoryDAL.Count(keyword, statusFilter);
        }

        public List<ProductCategoryDTO> GetAllCategory()
        {
            var entities = _subCategoryDAL.GetAllCategory();

            return entities.Select(x => new ProductCategoryDTO
            {
                Uid = x.Uid,
                CategoryName = x.CategoryName,
                Slug = x.Slug,
                Status = x.Status
            }).ToList();
        }

        public bool AddSubCategory(ProductSubCategoryDTO productSubCategoryDTO)
        {
            string cleanedProductName = productSubCategoryDTO.SubCategoryName.Trim();

            if (_subCategoryDAL.checkSubCategoryName(0, cleanedProductName))
            {
                MessageBox.Show($"Subcategory name '{cleanedProductName}' already exists in this Subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_subCategoryDAL.IsLockedCategory(productSubCategoryDTO.CategoryUid))
            {
                MessageBox.Show($"The parent category is currently locked; you should switch to a different category or reactivate the parent category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string finalSlug = productSubCategoryDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(productSubCategoryDTO.SubCategoryName);
            }

            DAL.EF.ProductSubCategory subCategoryEntity = new DAL.EF.ProductSubCategory
            {
                SubCategoryName = productSubCategoryDTO.SubCategoryName,
                CategoryUid = productSubCategoryDTO.CategoryUid,
                Status = productSubCategoryDTO.Status,
                Description = productSubCategoryDTO.Description,
                Slug = finalSlug,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false
            };

            return _subCategoryDAL.Add(subCategoryEntity);
        }

        public ProductSubCategoryDTO GetSubCategoryById(int id)
        {

            ProductSubCategory subCategoryEntity = _subCategoryDAL.GetSubCategoryById(id);

            if (subCategoryEntity == null)
            {
                return null;
            }
            ProductSubCategoryDTO subCategoryDTO = new ProductSubCategoryDTO
            {
                Uid = subCategoryEntity.Uid,
                SubCategoryName = subCategoryEntity.SubCategoryName,
                CategoryUid = subCategoryEntity.CategoryUid,
                CategoryName = subCategoryEntity.ProductCategory.CategoryName,
                Description = subCategoryEntity.Description,
                Status = subCategoryEntity.Status,
            };

            return subCategoryDTO;
        }

        public bool Update(ProductSubCategoryDTO subCategoryDTO)
        {
            int currentSubCategoryId = subCategoryDTO.Uid;
            string cleanedName = subCategoryDTO.SubCategoryName.Trim();

            if (_subCategoryDAL.checkSubCategoryName(currentSubCategoryId, cleanedName))
            {
                MessageBox.Show($"Subcategory name '{cleanedName}' already exists in this Subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_subCategoryDAL.IsLockedCategory(subCategoryDTO.CategoryUid))
            {
                MessageBox.Show($"The parent category is currently locked; you should switch to a different category or reactivate the parent category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string finalSlug = subCategoryDTO.Slug;
            if (string.IsNullOrEmpty(finalSlug))
            {
                finalSlug = _slugHelper.GenerateSlug(cleanedName);
            }

            DAL.EF.ProductSubCategory subCategoryEntity = new DAL.EF.ProductSubCategory
            {
                Uid = currentSubCategoryId,
                SubCategoryName = subCategoryDTO.SubCategoryName,
                CategoryUid = subCategoryDTO.CategoryUid,
                Description = subCategoryDTO.Description,
                Slug = finalSlug,
                Status = subCategoryDTO.Status,

                UpdatedAt = subCategoryDTO.UpdatedAt,
                UpdatedBy = subCategoryDTO.UpdatedBy,
            };

            return _subCategoryDAL.Update(subCategoryEntity);
        }

        public int Delete(int id)
        {
            try
            {
                return _subCategoryDAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Business error when deleting ID category {id}. Detail: {ex.Message}");
            }
        }

        public List<(string name, int count)> updateChangeMulti(string status, List<int> uids)
        {
            return _subCategoryDAL.updateChangeMulti(status, uids);
        }
    }
}
