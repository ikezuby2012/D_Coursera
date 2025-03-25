using Application.Abstractions.Messaging;
using Domain.DTO.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Media.CreateMedia;

public sealed record CreateMediaCommand(IEnumerable<IFormFile> files, Guid courseId, string collectionName) : ICommand<IEnumerable<CreatedMediaDto>>;

