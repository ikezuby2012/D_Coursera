﻿using SharedKernel;

namespace Domain.Users;

public sealed record UserRegisteredDomainEvent(User user) : IDomainEvent;
