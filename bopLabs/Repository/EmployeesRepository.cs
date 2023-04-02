using Dapper;
using bpoLabs.DbContext;
using bpoLabs.Model.DTO;
using System.Data;
using Microsoft.Graph.Models;
using bopLabs.Model.DTO;

namespace bpoLabs.Repository
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<EmployeesDTO>> ListUsers();
        Task<bool?> CreateUpdateUsers(int id, string name, string lastName, string email, string personalAddress, int phone, DateTime workingStartDate, string picture, int rol, decimal salary);
        Task DeleteUser(int userId);
        Task<bool?> UpadateSalaries();
        Task<IEnumerable<SalariesDTO>> ListAllIncreasesSalaries(int idUser);
    }
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly DapperDbContext _context;
        public EmployeesRepository(DapperDbContext context) { _context = context; }

        public async Task<IEnumerable<EmployeesDTO>> ListUsers()
        {
            var sql = "SP_ListAllUsers";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EmployeesDTO>(sql, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool?> CreateUpdateUsers(int id, string name, string lastName, string email, string personalAddress, int phone, DateTime workingStartDate, string picture, int rol, decimal salary) 
        { 
            try
            {
                var sql = "SP_InsertUpdateEmployees";
                var parameters = new
                {
                    Id = id,
                    Name = name,
                    LastName = lastName,
                    Email = email,
                    PersonalAddress = personalAddress,
                    Phone = phone,
                    WorkingStartDate = workingStartDate,
                    Picture = picture,
                    Rol = rol,
                    Salary = salary
                };
                using var connection = _context.CreateConnection();
                var hasErrors = await connection.ExecuteScalarAsync<bool?>(sql, parameters, commandType: CommandType.StoredProcedure);
                return hasErrors;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task DeleteUser(int userId)
        {
            try
            {
                const string sql = "SP_DeleteUsers";
                var parameters = new
                {
                    userId
                };
                using var connection = _context.CreateConnection();
                await connection.ExecuteScalarAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool?> UpadateSalaries()
        {
            try
            {
                var sql = "SP_EstimateNewSalaries";
                using var connection = _context.CreateConnection();
                var hasErrors = await connection.ExecuteScalarAsync<bool?>(sql, commandType: CommandType.StoredProcedure);
                return hasErrors;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SalariesDTO>> ListAllIncreasesSalaries(int idUser)
        {
            var sql = "SP_Report_Increases";
            var parameters = new
            {
                idUser
            };
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<SalariesDTO>(sql, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
