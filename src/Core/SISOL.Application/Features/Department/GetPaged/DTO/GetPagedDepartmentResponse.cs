namespace SISOL.Application.Features.Department.GetPaged.DTO;

internal class GetPagedDepartmentResponse
{
    public Guid Id { get; set; }
    public Guid? ParentDepartmentId { get; set; }
    public required string Name { get; set; }
}
