using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategory();
        Task InsertCategoryData(Guid Id, string Title, string Description,  Guid? ParentCategoryId, bool IsDeleted,DateTime? ModifiedOn, DateTime? CreatedOn, long? LogoFileSize);
    }
}