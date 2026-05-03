using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Application.Common.Contracts.Repositories;
using SISOL.Application.Common.Contracts.Services.Persistence;
using SISOL.Application.Features.Departments.Create.Command;
using SISOL.Domain.Common;

namespace SISOL.Application.Features.Departments.Create.Handler;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, Result<Guid>>
{
    private readonly IDepartmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentCommandHandler(IDepartmentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> HandleAsync(CreateDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var departmentResult = Domain.Entities.Department.Create(command.Name, command.ParentId);

        if (departmentResult.IsFailure)
            return Result.Failure<Guid>(departmentResult.Error ?? "Create department command error");

        if (departmentResult.Value == null)
            return Result.Failure<Guid>("Error creating a Department");

        if (command.ParentId != null && command.ParentId != Guid.Empty && _repository.GetByIdAsync(command.ParentId.Value) == null)
            return Result.Failure<Guid>("Department for the provided parent ID does not exist");

        await _repository.AddAsync(departmentResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(departmentResult.Value.Id);
    }
}
