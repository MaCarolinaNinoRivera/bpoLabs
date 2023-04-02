namespace bopLabs.Model.DTO
{
    public class SalariesDTO
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal StartingSalary { get; set; }
        public int IncreaseByRol { get; set; }
        public DateTime LastUpdateSalary { get;set; }
        public decimal OldSalary { get; set; }
        public decimal NewSalary { get; set; }
    }
}
