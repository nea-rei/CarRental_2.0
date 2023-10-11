using Common.Interfaces;

namespace Common.Classes;

public class Person : IPerson
{
    public int SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    public Person(int ssn, string lastname, string firstname) =>
        (SSN, LastName, FirstName) = (ssn, lastname, firstname);
}
