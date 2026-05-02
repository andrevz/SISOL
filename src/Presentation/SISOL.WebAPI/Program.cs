using Microsoft.AspNetCore.Mvc;
using SISOL.Application;
using SISOL.Application.Common.Contracts.Services.CQRS;
using SISOL.Application.Common.DTOs;
using SISOL.Application.Features.Department.GetPaged.DTO;
using SISOL.Application.Features.Department.GetPaged.Query;
using SISOL.Domain.Common;
using SISOL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("api/departments", async Task<IResult> ([FromQuery] int pageNumber, [FromQuery] int pageSize, IDispatcher dispatcher) =>
{
    var query = new GetPagedDepartmentQuery { PageNumber = pageNumber, PageSize = pageSize };
    var result = await dispatcher.QueryAsync<GetPagedDepartmentQuery, Result<PagedResponse<GetPagedDepartmentResponse>>>(query);

    if (result.IsSuccess && result.Value == null)
    {
        return TypedResults.NotFound();
    }

    if (result.IsSuccess)
    {
        return TypedResults.Ok(result.Value);
    }

    if (result.Errors.Count != 0)
    {
        return TypedResults.BadRequest(new
        {
            isSuccess = false,
            error = result.Errors,
            timespan = DateTime.UtcNow
        });
    }

    return TypedResults.BadRequest();
})
   .WithName("GetPagedDepartments");

app.Run();
