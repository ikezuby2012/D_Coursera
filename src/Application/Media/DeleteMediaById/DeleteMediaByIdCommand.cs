using Application.Abstractions.Messaging;

namespace Application.Media.DeleteMediaById;
public sealed record DeleteMediaByIdCommand(Guid Id) : ICommand;
