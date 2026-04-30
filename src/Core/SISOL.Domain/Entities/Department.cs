namespace SISOL.Domain.Entities;

public class Department : BaseEntity
{
    private readonly List<Department> _subDepartments = [];

    public Guid? ParentId { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyCollection<Department> SubDepartments => _subDepartments.AsReadOnly();

    private Department(string name, Guid? parentId)
    {
        Name = name;
        ParentId = parentId;
    }
}
