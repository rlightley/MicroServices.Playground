using System.ComponentModel.DataAnnotations;

namespace MicroServices.Playground.ServiceTwo.Api.Domain.Models;

public class Person : BaseEntity
{
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
