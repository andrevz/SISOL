using SISOL.Domain.Common;

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

    public static Result<Department> Create(string name, Guid? parentDepartmentId)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Failure<Department>("Department name is required");

        if (parentDepartmentId == Guid.Empty)
            return Result.Failure<Department>("Invalid parent department ID");

        return Result.Success(new Department(name, parentDepartmentId));
    }
}
