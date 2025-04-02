using Application.Abstractions.Messaging;
using Domain.DTO.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Media.UpdateMediaById;
public sealed record UpdateMediaByIdCommand(Guid Id, IEnumerable<IFormFile> Files, Guid courseId, string collectionName) : ICommand<UpdatedMediaDto>;
