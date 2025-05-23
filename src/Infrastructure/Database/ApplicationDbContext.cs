﻿using System.Data;
using System.Linq.Expressions;
using System.Threading;
using Application.Abstractions.Data;
using Domain.Assignments;
using Domain.AssignmentSubmission;
using Domain.Common;
using Domain.Course;
using Domain.Enrollment;
using Domain.Exams;
using Domain.Media;
using Domain.Todos;
using Domain.Users;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentSubmissions> AssignmentSubmissions { get; set; }
    public DbSet<Exams> Examination { get; set; }
    public DbSet<ExamQuestions> ExamQuestions { get; set; }
    public DbSet<ExamQuestionOption> ExamQuestionOptions { get; set; }
    public DbSet<ExamsSubmission> ExamsSubmissions { get; set; }
    public DbSet<ExamAnswer> ExamAnswers { get; set; }
    public DbSet<Enrollment> Enrollment { get; set; }
    public DatabaseFacade DatabaseFacade => base.Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);

        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");
                LambdaExpression filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(IAuditableEntity.IsSoftDeleted)),
                        Expression.Constant(false)
                    ),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

    public void GetAppLock(string appLockId)
    {
        string sql = "EXEC sp_getapplock @Resource, 'Exclusive';";
        var parameter = new SqlParameter("@Resource", SqlDbType.NVarChar, 255)
        {
            Value = appLockId
        };

        Database.ExecuteSqlRaw(sql, parameter);
    }

    public async Task GetAppLockAsync(string appLockId, int timeOut, CancellationToken cancellationToken)
    {
        const string sql = @"
        DECLARE @result INT;
        EXEC @result = sp_getapplock 
            @Resource = @Resource,
            @LockMode = 'Exclusive',
            @LockOwner = 'Transaction',
            @LockTimeout = @Timeout;
        
        IF @result < 0
            THROW 50000, 'Could not acquire application lock', 1;";

        SqlParameter[] parameters = new[]
        {
            new SqlParameter("@Resource", SqlDbType.NVarChar) { Value = appLockId },
            new SqlParameter("@Timeout", SqlDbType.Int) { Value = timeOut }
        };

        await Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }
}
