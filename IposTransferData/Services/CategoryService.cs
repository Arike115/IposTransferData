using Dapper;
using IposTransferData.DataAccess;
using IposTransferData.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDataDapperService _db;
        public CategoryService(IDataDapperService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategory()
        {
            var sql = @"select * From Category ca
                        WHERE ca.IsDeleted <> 1
                        ORDER BY ca.ModifiedOnUtc DESC";

            var category = await _db.GetData<CategoryDto>(sql, null);
            return category;

        }


        public async Task InsertCategoryData(Guid Id,string Title, string Description,  Guid? ParentCategory_Id, bool IsDeleted, DateTime? ModifiedOn, DateTime? CreatedOn, long? LogoFileSize)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id);
            parameter.Add("@Title", Title);
            parameter.Add("@Description", Description);
            parameter.Add("@ParentCategory_Id", ParentCategory_Id ?? null);
            parameter.Add("@IsDeleted", IsDeleted);
            parameter.Add("@ModifiedOn", ModifiedOn);
            parameter.Add("@CreatedOn", CreatedOn);
            parameter.Add("@LogoFileSize", LogoFileSize ?? 0);

            
            var sql = @"INSERT into Category(Id, Title, Description, ParentCategory_Id,IsDeleted, ModifiedOn, CreatedOn,LogoFileSize) 
                                      Values(@Id, @Title, @Description, @ParentCategory_Id, @IsDeleted, @ModifiedOn, @CreatedOn, @LogoFileSize)";

              await _db.SaveData(sql, parameter);
           
        }

      
    }
}
