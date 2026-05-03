using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Application.Common.DTOs;
using SISOL.Application.Features.Departments.GetPaged.DTO;
using SISOL.Domain.Common;

namespace SISOL.Application.Features.Departments.GetPaged.Query;

public class GetPagedDepartmentQuery : PagedRequest, IQuery<Result<PagedResponse<GetPagedDepartmentResponse>>>
{
}
