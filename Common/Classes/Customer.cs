using Common.Interfaces;

namespace Common.Classes;

public class Customer : IPerson
{
    public int Id { get; set; } = default;
    public string SSN { get; set; }= string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    public void AssignId(int id) => Id = id;
    public Customer() { }
    public Customer(int id, string ssn, string lastname, string firstname) =>
        (Id, SSN, LastName, FirstName) = (id, ssn, lastname, firstname);
    public Customer(string ssn, string lastname, string firstname) =>
        (SSN, LastName, FirstName) = (ssn, lastname, firstname);
}
