namespace Common.Interfaces;

public interface IPerson
{
    public int Id { get; set; }
    public string SSN { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string FullName => $"{LastName} {FirstName} ({SSN})";

    void AssignId(int id);
}
