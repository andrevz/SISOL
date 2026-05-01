using SISOL.Application.Common.Contracts.Repositories;
using SISOL.Domain.Entities;
using SISOL.Infrastructure.Configurations.Persistence.Context;

namespace SISOL.Infrastructure.Adapters.Repositories;

internal class DepartmentRepository(AppDbContext context) : BaseRepository<Department>(context), IDepartmentRepository
{
}
