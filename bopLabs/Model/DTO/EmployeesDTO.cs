using System.Numerics;

namespace bpoLabs.Model.DTO
{
    public class EmployeesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalAddress { get; set; }
        public int Phone { get; set; }
        public string WorkingStartDate { get; set; }
        public string Picture { get; set; }
        public int Rol { get; set; }
        public string NameRol { get; set; }
        public string Salary { get; set; }
    }
}
