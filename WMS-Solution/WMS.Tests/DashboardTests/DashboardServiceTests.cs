using Microsoft.EntityFrameworkCore;
using Xunit;
using WMS.Application.Services;
using WMS.Infrastructure.Data;
using WMS.Domain.Entities;

namespace WMS.Tests.DashboardTests;

public class DashboardServiceTests
{
    private WmsDbContext GetDbContext()
    {
        var options =
            new DbContextOptionsBuilder<WmsDbContext>()
            .UseInMemoryDatabase(
                Guid.NewGuid().ToString())
            .Options;

        return new WmsDbContext(options);
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnEmployeeCount()
    {
        var context = GetDbContext();

        context.Employees.Add(
            new Employee
            {
                EmployeeId = 1,
                FirstName = "John"
            });

        await context.SaveChangesAsync();

        var service =
            new DashboardService(context);

        var result =
            await service.GetSummaryAsync();

        Assert.Equal(1,
            result.TotalEmployees);
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnDepartmentCount()
    {
        var context = GetDbContext();

        context.Departments.Add(
            new Department
            {
                DepartmentId = 1,
                DepartmentName = "IT"
            });

        await context.SaveChangesAsync();

        var service =
            new DashboardService(context);

        var result =
            await service.GetSummaryAsync();

        Assert.Equal(1,
            result.TotalDepartments);
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnProjectCount()
    {
        var context = GetDbContext();

        context.Projects.Add(
            new Project
            {
                ProjectId = 1,
                ProjectName = "WMS",
                Status = "Active"
            });

        await context.SaveChangesAsync();

        var service =
            new DashboardService(context);

        var result =
            await service.GetSummaryAsync();

        Assert.Equal(1,
            result.TotalProjects);
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnClientCount()
    {
        var context = GetDbContext();

        context.Clients.Add(
            new Client
            {
                ClientId = 1,
                ClientName = "Microsoft"
            });

        await context.SaveChangesAsync();

        var service =
            new DashboardService(context);

        var result =
            await service.GetSummaryAsync();

        Assert.Equal(1,
            result.TotalClients);
    }

    [Fact]
    public async Task GetEmployeeDashboardAsync_InvalidEmployee_ShouldThrowException()
    {
        var context = GetDbContext();

        var service =
            new DashboardService(context);

        await Assert.ThrowsAsync<Exception>(
            () =>
            service.GetEmployeeDashboardAsync(999));
    }
}