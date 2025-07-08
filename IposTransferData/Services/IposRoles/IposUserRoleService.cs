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
    public class IposUserRoleService : IIposUserRoleService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public IposUserRoleService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<IposUserRole>> GetUserRolesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM IposUserRole 
                            WHERE NOT (
                                (UserId = '1989883F-4F99-43BF-A754-239BBBFEC00E' AND RoleId = 'A1B6B6B0-0825-4975-A93D-DF3DC86F8CC7') OR
                                (UserId = 'E8BA24E8-8FCC-4B64-B3E0-4C5B0B29E4D2' AND RoleId = 'FBB820ED-9BD8-4FC9-25A4-08DAAB83FC2F') OR
                                (UserId = 'DA326679-5D30-41B0-B7DA-9F29B6CAE1B5' AND RoleId = '3134FF36-F284-4634-A2D1-31F6DDAF2668') OR
                                (UserId = '973AF7A9-7F18-4E8B-ACD3-BC906580561A' AND RoleId = 'D7788F50-4F8F-40E1-9ABD-6BE7F2C38E79') OR
                                (UserId = '3FB897C8-C25D-4328-9813-CB1544369FBA' AND RoleId = '0718DDEF-4067-4F29-AAA1-98C1548C1807') OR
                                (UserId = '129712E3-9214-4DD3-9C03-CFC4EB9BA979' AND RoleId = '1E816D09-6D15-4229-A445-5D4E9A2ED515')
                            );";
            var userRoles = await _sqlConnection.QueryAsync<IposUserRole>(sql);
            return userRoles;
        }

        public async Task InsertUserRoleAsync(IposUserRole userRole)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"INSERT INTO IposUserRole (UserId, RoleId) VALUES (@UserId, @RoleId);";

            await _destinationConnection.ExecuteAsync(sql, userRole);
        }
    }
}
