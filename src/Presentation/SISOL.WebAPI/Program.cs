using Microsoft.AspNetCore.Mvc;
using SISOL.Application;
using SISOL.Application.Common.Contracts.Services.CQRS;
using SISOL.Application.Common.DTOs;
using SISOL.Application.Features.Departments.Create.Command;
using SISOL.Application.Features.Departments.GetPaged.DTO;
using SISOL.Application.Features.Departments.GetPaged.Query;
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

app.MapGet("api/departments", async Task<IResult> ([FromQuery] int? pageNumber, [FromQuery] int? pageSize, IDispatcher dispatcher) =>
{
    var query = new GetPagedDepartmentQuery();

    if (pageNumber > 0 && pageSize > 0)
    {
        query.PageNumber = pageNumber.Value;
        query.PageSize = pageSize.Value;
    }

    var result = await dispatcher.QueryAsync<GetPagedDepartmentQuery, Result<PagedResponse<GetPagedDepartmentResponse>>>(query);

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

app.MapGet("api/departments/{id:guid}", (Guid id, IDispatcher dispatcher) => { })
    .WithName("GetDepartmentById");

app.MapPost("api/departments", async Task<IResult> (CreateDepartmentCommand command, IDispatcher dispatcher) =>
{
    var result = await dispatcher.SendAsync<CreateDepartmentCommand, Result<Guid>>(command);

    if (result.IsSuccess)
        return TypedResults.CreatedAtRoute("GetDepartmentById", new { id = result.Value });

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
    .WithName("CreateDepartment");

app.Run();
