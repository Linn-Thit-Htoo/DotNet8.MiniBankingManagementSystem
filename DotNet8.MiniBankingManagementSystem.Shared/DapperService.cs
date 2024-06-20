using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Dapper;

namespace DotNet8.MiniBankingManagementSystem.Shared
{
    public class DapperService
    {
        private readonly IConfiguration _configuration;

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<T> Query<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            List<T> lst = db.Query<T>(query, parameters, commandType: commandType).ToList();

            return lst;
        }

        public async Task<List<T>> QueryAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            var lst = await db.QueryAsync<T>(query, parameters, commandType: commandType);

            return lst.ToList();
        }

        public T? QueryFirstOrDefault<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            T? item = db.Query<T>(query, parameters, commandType: commandType).FirstOrDefault();

            return item;
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            var item = await db.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);

            return item;
        }

        public int Execute(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            return db.Execute(query, parameters);
        }

        public async Task<int> ExecuteAsync(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            return await db.ExecuteAsync(query, parameters);
        }
    }
}
