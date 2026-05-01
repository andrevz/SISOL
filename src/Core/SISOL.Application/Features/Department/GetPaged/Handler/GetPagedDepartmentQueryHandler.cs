using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Application.Common.Contracts.Repositories;
using SISOL.Application.Common.DTOs;
using SISOL.Application.Common.Utils;
using SISOL.Application.Features.Department.GetPaged.DTO;
using SISOL.Application.Features.Department.GetPaged.Query;
using SISOL.Domain.Common;

namespace SISOL.Application.Features.Department.GetPaged.Handler
{
    internal class GetPagedDepartmentQueryHandler : IQueryHandler<GetPagedDepartmentQuery, Result<PagedResponse<GetPagedDepartmentResponse>>>
    {
        private readonly IDepartmentRepository _repository;

        public GetPagedDepartmentQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<GetPagedDepartmentResponse>>> HandleAsync(GetPagedDepartmentQuery query, CancellationToken cancellationToken = default)
        {
            var response = new PagedResponse<GetPagedDepartmentResponse>();

            var (Collection, TotalCount) = await _repository.ListAsync<GetPagedDepartmentResponse, Guid>(
                selector: s => new GetPagedDepartmentResponse
                {
                    Id = s.Id,
                    ParentDepartmentId = s.ParentDepartmentId,
                    Name = s.Name
                },
                predicate: null,
                orderBy: null,
                page: query.PageNumber,
                pageSize: query.PageSize);

            response.Result = Collection;
            response.TotalRows = TotalCount;
            response.TotalRowsPerPage = Collection.Count;
            response.TotalPages = PagingHelpers.CalculateTotalPages(TotalCount, query.PageSize);

            return Result.Success(response);
        }
    }
}
