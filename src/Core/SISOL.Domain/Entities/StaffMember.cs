using SISOL.Domain.ValueObjects;

namespace SISOL.Domain.Entities;

public class StaffMember : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IdentityDocument Document { get; private set; }
    public Guid DepartmentId { get; private set; }
    public JobTitle Title { get; private set; }

    private StaffMember(string firstName, string lastName, IdentityDocument document, Guid departmentId, JobTitle jobTitle)
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        DepartmentId = departmentId;
        Title = jobTitle;
    }
}
