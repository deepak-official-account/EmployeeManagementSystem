using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DemoProject.enums;
using DemoProject.Models;

namespace DemoProject.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class EmployeeController : Controller
    {
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login([Bind(Include = "Email,Password")] User user)
        {
            using (var context = new UserContext())
            {
                var validUser = context.Users
                    .FirstOrDefault(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password));

                if (validUser != null)
                {
                    //FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["UserRole"] = validUser.userRole.ToString();

                    //FormsAuthentication.SetAuthCookie(user.Email, false);

                    var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, validUser.userRole.ToString());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return RedirectToAction("GetAllEmployees");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View();
        }

        [AllowAnonymous]
        [Route("signup")]
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new UserContext()) {
              
                    context.Users.Add(user);
                    context.SaveChanges();
                }
              
            }

            return RedirectToAction("Login");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // Create Employee - GET
        [Authorize(Roles="Admin")]
        //[OverrideAuthorization]
        [HttpGet]
        [Route("create")]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        // Create Employee - POST
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    using (var context = new EmployeeContext())
                    {
                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }

                }
            }

            return RedirectToAction("GetAllEmployees");
        }

        // Get All Employees
 
        [HttpGet]
        [Route("employees")]
        public ActionResult GetAllEmployees()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Employee> employees;
                using (var context = new EmployeeContext())
                {
                    employees = context.Employees.ToList();
                }
                ViewBag.Employees = employees;

            }
            else
            {
                return RedirectToAction("Login");
            }
                return View();
        }


        // Delete Employee
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
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
            }
           

            return RedirectToAction("GetAllEmployees");
        }

        // Edit Employee - GET
        [Authorize(Roles="Admin")]
        [HttpGet]
        [Route("edit/{id:int}")]
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
        [Authorize(Roles="Admin")]
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
