using Microsoft.AspNetCore.Mvc;
using bpoLabs.Business;

namespace bpoLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeesService;

        public EmployeesController(IEmployeeService employeesService)
        {
            _employeesService = employeesService;
        }

        /// <summary>
        /// List all user in the app
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listAllUsers")]
        public async Task<IActionResult> ListReportUsers()
        {
            var result = await _employeesService.ListUsers();
            return Ok(result);
        }

        [HttpPost]
        [Route("createUpadateUsers")]
        public async Task<IActionResult> CreateUpdateUsers(string name, string lastName, string email, string personalAddress, int phone, DateTime workingStartDate, string picture, int rol, decimal salary, int id=0)
        {
            var result = await _employeesService.CreateUpdateUsers(id, name, lastName, email, personalAddress, phone, workingStartDate, picture, rol, salary);
            var state = result == true ? "Error" : "Success";
            return Ok(state);
        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _employeesService.DeleteUser(userId);
            return Ok();
        }

        [HttpPost]
        [Route("upadateSalaries")]
        public async Task<IActionResult> UpadateSalaries()
        {
            var result = await _employeesService.UpadateSalaries();
            var state = result == true ? "Error" : "Success";
            return Ok(state);
        }

        [HttpGet]
        [Route("listAllIncreasesSalaries")]
        public async Task<IActionResult> ListAllIncreasesSalaries(int idUser)
        {
            var result = await _employeesService.ListAllIncreasesSalaries(idUser);
            return Ok(result);
        }
    }
}
