using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.IposRoles
{
    public class IposRoleService : IIposRoleService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public IposRoleService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<IposRole>> GetIposRolesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            string[] defaultNames = { "IPOS_SUPPORT", "IPOS_AGENT", "IPOS_BASIC", "IPOS_SME",
                "IPOS_MANAGER_USER", "IPOS_ADMIN", "SYS_ADMIN" };

            var sql = @"SELECT * FROM IposRole WHERE Name NOT IN @DefaultNames";
            var roles = await _sqlConnection.QueryAsync<IposRole>(sql, new { DefaultNames = defaultNames });
            return roles;
        }

        public async Task InsertIposRoleAsync(IposRole role)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO IposRole (
            Id, Name, NormalizedName, ConcurrencyStamp, CreatedOn, ModifiedOn, 
            CreatedBy, ModifiedBy, IsInBuilt, Business_Id
        ) VALUES (
            @Id, @Name, @NormalizedName, @ConcurrencyStamp, @CreatedOn, @ModifiedOn, 
            @CreatedBy, @ModifiedBy, @IsInBuilt, @Business_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, role);
        }
    }
}
