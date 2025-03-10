﻿using DemoProject.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email Cannot contain more than 100 Characters")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password Must be between 8 and 20")]

 
        public string Password { get; set; }
        [Required]
        public UserRole userRole { get; set; } 
        public DateTime CreatedAt { get; set; }
        public User()
        {

        }

        public User(string name, string email, string password,UserRole userRole)
        {

            this.Id = new Random().Next(1000,9999);
            this.Name = name;
            this.Email = email;
            this.userRole = userRole;
            this.Password = password;
            this.CreatedAt = DateTime.Now;
        }
    }
}