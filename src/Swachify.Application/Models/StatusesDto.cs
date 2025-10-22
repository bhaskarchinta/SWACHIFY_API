namespace Swachify.Application.DTOs;
public class StatusesDtos
{
    public long id { get; set; }

    public string status { get; set; } = null!;

    public bool? is_active { get; set; }
}