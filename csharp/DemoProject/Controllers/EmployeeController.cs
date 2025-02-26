using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Models;

namespace DemoProject.Controllers
{
    public class EmployeeController : Controller
    {
        // Create Employee - GET
        [HttpGet]
        [Route("create")]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        // Create Employee - POST
        [HttpPost]
        [Route("create/{employee}")]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EmployeeContext())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }

            }

            return RedirectToAction("GetAllEmployees");
        }

        // Get All Employees
        [HttpGet]
        [Route("employees")]
        public ActionResult GetAllEmployees()
        {
            List<Employee> employees;
            using (var context = new EmployeeContext())
            {
                employees = context.Employees.ToList();
            }
            ViewBag.Employees = employees;
            return View();
        }

        // Delete Employee
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new EmployeeContext())
            {
                var existingEmployee = context.Employees.Find(id);
                if (existingEmployee != null)
                {
                    context.Employees.Remove(existingEmployee);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("GetAllEmployees");
        }

        // Edit Employee - GET
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Employee emp;
            using (var context = new EmployeeContext())
            {
                emp = context.Employees.Find(id);
            }

            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }

        // Edit Employee - POST
        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EmployeeContext())
                {
                    var existingEmployee = context.Employees.Find(id);
                    if (existingEmployee != null)
                    {
                        existingEmployee.Name = employee.Name;
                        existingEmployee.Department = employee.Department;
                        existingEmployee.Salary = employee.Salary;
                        context.SaveChanges();
                    }
                }
            }



            return RedirectToAction("GetAllEmployees");
        }
    }
}
