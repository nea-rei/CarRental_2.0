namespace Common.Interfaces;

public interface IPerson
{
    public int Id { get; }
    public string SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    void AssignId(int id);
}
