using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Domain.Common;

namespace SISOL.Application.Features.Departments.Create.Command;

public class CreateDepartmentCommand : ICommand<Result<Guid>>
{
    public required string Name { get; set; }
    public Guid? ParentId { get; set; }
}
