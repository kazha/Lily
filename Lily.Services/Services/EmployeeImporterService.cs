using Lily.Services.Configuration;
using Lily.Services.Entities;
using Lily.Services.Integrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services.Services
{
    public class EmployeeImporterService: IEmployeeImporterService
    {
        private readonly IRandomUserService _randomUserService;
        private readonly Coordinates _currentLocation;
        private readonly IEmployeeDataManager _employeeDataManager;

        public EmployeeImporterService(
            IOptions<ServiceConfiguration> options,
            IRandomUserService randomUserService,
            IEmployeeDataManager employeeDataManager)
        {
            _randomUserService = randomUserService;
            _currentLocation = options?.Value?.CurrentLocation;
            _employeeDataManager = employeeDataManager;
        }

        /// <summary>
        /// Gets external users and converts to local employee entity
        /// if any error occurs whole process is stopped
        /// could be improved by saving valid data and report just invalid data
        /// </summary>
        public async Task ImportFromRandomUser()
        {
            var externalEmployees = await _randomUserService.GetExternalEmployees();
            var employees = ConvertExternalEmployees(externalEmployees);
            await _employeeDataManager.SaveEmployees(employees);
        }

        private List<Employee> ConvertExternalEmployees(IList<ExternalEntities.Employee> employees)
        {
            var result = new List<Employee>();
            foreach (var externalEmployee in employees)
            {
                var id = Guid.Parse(externalEmployee.Login.Uuid);
                var employeeCoordinates = externalEmployee.Location.Coordinates;
                if (_currentLocation == null)
                {
                    throw new InvalidDataException("Curent location not set in configuration!");
                }
                var latitude = ParseCoordinates(employeeCoordinates.Latitude);
                var longitude = ParseCoordinates(employeeCoordinates.Longitude);

                var distanceFromRiga = Utility.DistanceFrom(
                    _currentLocation.Latitude,
                    _currentLocation.Longitude,
                    latitude,
                    longitude);

                var employee = new Employee
                {
                    Id = id,
                    Age = externalEmployee.Dob.Age,
                    Country = externalEmployee.Location.Country,
                    DateOfBirth = externalEmployee.Dob.Date,
                    FirstName = externalEmployee.Name.First,
                    LastName = externalEmployee.Name.Last,
                    Nationality = externalEmployee.Nat,
                    PictureUrl = externalEmployee.Picture.Medium,
                    DistanceFromRiga = distanceFromRiga
                };
                result.Add(employee);
            }
            return result;
        }

        /// <summary>
        /// Parses coordinates, throws format exception in case of an error
        /// </summary>
        /// <param name="coordinate">latitude or langtitude value</param>
        /// <returns>parsed value as double</returns>
        private double ParseCoordinates(string coordinate)
        {
            double result;
            if(!double.TryParse(coordinate,out result))
            {
                throw new FormatException($"Invalid coordinate value: {coordinate}");
            }
            return result;
        }
    }
}
