using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DemoProject.Models
{

	public class Employee
	{
		public int EmployeeId { get; set; }
		public string Name { get; set; }

		public double Salary { get; set; }

		public string Department { get; set; }

		public Employee()
		{

		}
		public Employee(string Name, string department, double Salary)
		{
			Random random = new Random();
			this.EmployeeId = random.Next(10000);
			this.Name = Name;
			this.Department = department;
			this.Salary = Salary;

		}

        public override string ToString()
        {
			return $"EmployeeId:{EmployeeId}, Name: {Name}, Department: {Department}, Salary:{Salary}";
        }


        public class EmployeeDBContext : DbContext
		{
			public DbSet<Employee> Employees { get; set; }


		}
	}
}