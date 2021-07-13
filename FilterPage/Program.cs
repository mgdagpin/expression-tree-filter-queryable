using System;
using System.Collections.Generic;
using System.Linq;

namespace FilterPage
{
    class Program
    {
        static List<Person> people = new List<Person>()
        {
            new Person
            {
                FirstName = "Mar",
                LastName = "Dagpin",
                Gender = Gender.Male,
                BirthDate = new DateTime(1992, 10, 30, 00, 00, 00),
                Height = 5.5d
            },
            new Person
            {
                FirstName = "Maika",
                LastName = "Rosell",
                Gender =  Gender.Female,
                BirthDate = new DateTime(1996, 4, 8, 00,00,00),
                Height = 5.4d
            }
        }; 

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var filter1 = new Filters
            {
                Filter = new Filter
                {
                    Field = "FirstName",
                    Operator = FilterOperator.Eq,
                    Value = "Mar"
                }
            };

            var filter2 =  new Filters
            {
                Logic = FilterLogic.And,
                Filter = new Filter
                {
                    Field = "LastName",
                    Operator = FilterOperator.Neq,
                    Value = "Da"
                }
            };

            var filter3 = new Filters
            {
                Logic = FilterLogic.Or,
                Filter = new Filter
                {
                    Field = "Gender",
                    Operator = FilterOperator.Neq,
                    Value = Gender.Female
                }
            };


            var result = people.AsQueryable().FilterablePage(filter1, filter2, filter3);



            Console.ReadLine();
        }
    }
}
