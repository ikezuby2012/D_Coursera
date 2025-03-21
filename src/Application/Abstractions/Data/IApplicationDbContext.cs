﻿using Domain.Course;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Course> Courses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
