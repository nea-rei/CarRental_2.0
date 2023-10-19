namespace Common.Interfaces;

public interface IPerson
{
    public int Id { get; }
    public string SSN { get; }
    public string LastName { get; }
    public string FirstName { get; }
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    void AssignId(int id);

}
