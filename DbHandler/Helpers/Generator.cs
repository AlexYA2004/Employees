using DbHandler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Helpers
{
    public static class Generator
    {
        public static async Task<IEnumerable<Employee>> GenerateRandomEmployeesAsync(int count)
        {
            var rand = new Random();

            var genders = new[] { "Male", "Female" };

            var employees = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                string fullName = $"{RandomString(rand, 5)} {RandomString(rand, 7)} {RandomString(rand, 9)}";

                DateTime birthDate = new(rand.Next(1950, 2010), rand.Next(1, 12), rand.Next(1, 28));

                string gender = genders[rand.Next(genders.Length)];

                employees.Add(new Employee
                {
                    FullName = fullName,

                    DateOfBirth = birthDate,

                    Gender = gender
                });
            }
            
            return employees.AsEnumerable();
        }

        public static async Task<IEnumerable<Employee>> GenerateSpecialEmployeesAsync(int count)
        {
            var rand = new Random();

            var employees = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                string fullName = $"F{RandomString(rand, 4)} {RandomString(rand, 7)} {RandomString(rand, 9)}";

                DateTime birthDate = new(rand.Next(1950, 2010), rand.Next(1, 12), rand.Next(1, 28));

                employees.Add(new Employee
                {
                    FullName = fullName,

                    DateOfBirth = birthDate,

                    Gender = "Male"
                });
            }

            return employees.AsEnumerable();
        }


        private static string RandomString(Random rand, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
