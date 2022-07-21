namespace EmpSystemApp.Models
{
    public class Employees : Department
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }


    }
}
