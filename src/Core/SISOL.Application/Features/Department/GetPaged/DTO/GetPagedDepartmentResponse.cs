namespace SISOL.Application.Features.Department.GetPaged.DTO;

public class GetPagedDepartmentResponse
{
    public Guid Id { get; set; }
    public Guid? ParentDepartmentId { get; set; }
    public required string Name { get; set; }
}
