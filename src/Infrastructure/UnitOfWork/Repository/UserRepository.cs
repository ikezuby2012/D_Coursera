using Application.Abstractions.Interface;
using Domain.Users;
using Infrastructure.Database;


namespace Infrastructure.UnitOfWork.Repository;
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(User user)
    {
        _db.Users.Update(user);
    }
}
