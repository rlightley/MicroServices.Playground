namespace MicroServices.Playground.ServiceOne.Api.Domain.Models;

public class Person : BaseEntity
{
    public Person(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    public void Update(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
