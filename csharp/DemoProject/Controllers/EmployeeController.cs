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


        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            using (var context = new EmployeeContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();


            }


            return RedirectToAction("GetAllEmployees");
        }


        [HttpGet]
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



        [HttpPost]
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




        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee emp;
            using (var context = new EmployeeContext())
            {
                emp = context.Employees.Find(id);

            }

            ViewBag.Employee = emp;
            return View(emp);
        }



        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            using (var context = new EmployeeContext())
            {
                var existingEmployee = context.Employees.Find(employee.EmployeeId);
                if (existingEmployee != null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Department = employee.Department;
                    existingEmployee.Salary = employee.Salary;
                    context.SaveChanges();
                }
            }

            return RedirectToAction("GetAllEmployees");
        }





    }
}