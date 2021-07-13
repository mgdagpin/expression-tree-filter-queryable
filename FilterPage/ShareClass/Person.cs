using System;
namespace FilterPage
{
    public class Person
    {
        public Person()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }

    }
}
