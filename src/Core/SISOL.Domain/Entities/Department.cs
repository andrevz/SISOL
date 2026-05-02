namespace SISOL.Domain.Entities;

public class Department : BaseEntity
{
    private readonly List<Department> _subDepartments = [];

    public Guid? ParentDepartmentId { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyCollection<Department> SubDepartments => _subDepartments.AsReadOnly();
    public Department? ParentDepartment { get; set; }

    private Department()
    {
        Name = string.Empty;
    }

    private Department(string name, Guid? parentDepartmentId)
    {
        Name = name;
        ParentDepartmentId = parentDepartmentId;
    }
}
