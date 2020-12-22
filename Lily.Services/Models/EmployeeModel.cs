using Lily.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Models
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PictureUrl { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public double DistanceFromRiga { get; set; }

        public static implicit operator EmployeeModel(Employee employee)
        {
            return new EmployeeModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Country = employee.Country,
                DateOfBirth = employee.DateOfBirth,
                DistanceFromRiga = employee.DistanceFromRiga,
                Nationality = employee.Nationality,
                PictureUrl = employee.PictureUrl
            };
        }
    }
}
