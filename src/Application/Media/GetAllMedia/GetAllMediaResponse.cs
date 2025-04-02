using Domain.DTO.Media;

namespace Application.Media.GetAllMedia;
public class GetAllMediaResponse
{
    public IEnumerable<PagedMediaResponseDto> data { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
