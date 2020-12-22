using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string PictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Nationality { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public double DistanceFromRiga { get; set; }
    }
}
