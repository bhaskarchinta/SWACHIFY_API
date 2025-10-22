namespace Swachify.Application.DTOs;
public class DepartmentDtos
{
    public long id { get; set; }

    public string department_name { get; set; } = null!;

    public bool? is_active { get; set; }
}