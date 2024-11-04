using AttenDex.Application.Repository.Interfaces;
using AttenDex.Shared.Contracts;
using AttenDex.Shared.Models;

namespace AttenDex.Api.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeesEndpoints(this WebApplication app)
    {
        app.MapGet("/employees", async (Supabase.Client client) =>
        {
            var response = await client.From<Employee>().Get();
            var employees = response.Models;

            return Results.Ok(employees);
        });

        app.MapPost("/employees", async (CreateEmployeeRequest request, Supabase.Client client) =>
        {
            var newLog = new Employee
            {
                EmployeeId = request.EmployeeId,
                Name = request.Name,
                TimeIn = request.TimeIn
            };

            var response = await client.From<Employee>().Insert(newLog);

            var recordedLog = response.Models.First();

            return Results.Ok(recordedLog.Id);
        });
    }
}

