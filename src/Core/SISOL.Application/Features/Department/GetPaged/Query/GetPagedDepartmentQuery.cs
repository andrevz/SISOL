using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Application.Common.DTOs;
using SISOL.Application.Features.Department.GetPaged.DTO;
using SISOL.Domain.Common;

namespace SISOL.Application.Features.Department.GetPaged.Query;

internal class GetPagedDepartmentQuery : PagedRequest, IQuery<Result<PagedResponse<GetPagedDepartmentResponse>>>
{
}
