using SharedKernel;

namespace Domain.Auth;
public sealed record AuthRegisteredDomainEvents(Guid userId) : IDomainEvent;
