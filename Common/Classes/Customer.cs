using Common.Interfaces;

namespace Common.Classes;

public class Customer : IPerson
{
    public int Id { get; private set; }
    public string SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    public void AssignId(int id) => Id = id;

    public Customer(int id, string ssn, string lastname, string firstname) =>
        (Id, SSN, LastName, FirstName) = (id, ssn, lastname, firstname);
    public Customer(string ssn, string lastname, string firstname) =>
        (SSN, LastName, FirstName) = (ssn, lastname, firstname);
}
