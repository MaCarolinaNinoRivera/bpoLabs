using bopLabs.Model.DTO;
using bpoLabs.Model.DTO;
using bpoLabs.Repository;

namespace bpoLabs.Business
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeesDTO>> ListUsers();
        Task<bool?> CreateUpdateUsers(int id, string name, string lastName, string email, string personalAddress, int phone, DateTime workingStartDate, string picture, int rol, decimal salary);
        Task DeleteUser(int userId);
        Task<bool?> UpadateSalaries();
        Task<IEnumerable<SalariesDTO>> ListAllIncreasesSalaries(int idUser);
    }
    public class EmployeesService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeeRepository;
        public EmployeesService(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeesDTO>> ListUsers()
        {
            return await _employeeRepository.ListUsers();
        }

        public async Task<bool?> CreateUpdateUsers(int id, string name, string lastName, string email, string personalAddress, int phone, DateTime workingStartDate, string picture, int rol, decimal salary)
        {
            return await _employeeRepository.CreateUpdateUsers(id, name, lastName, email, personalAddress, phone, workingStartDate, picture, rol, salary);
        }

        public async Task DeleteUser(int userId)
        {
            await _employeeRepository.DeleteUser(userId);
        }

        public async Task<bool?> UpadateSalaries()
        {
            return await _employeeRepository.UpadateSalaries();
        }

        public async Task<IEnumerable<SalariesDTO>> ListAllIncreasesSalaries(int idUser)
        {
            return await _employeeRepository.ListAllIncreasesSalaries(idUser);
        }
    }
}
