using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace DemoProject.Models
{

	public class Employee
	{
		public int EmployeeId { get; set; }
		[Required(ErrorMessage="Please Enter your name")]
		public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter Salary")]
		[Range(10000,100000)]
        public double Salary { get; set; }
        [Required(ErrorMessage ="Please Enter Department")]
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
	}
}