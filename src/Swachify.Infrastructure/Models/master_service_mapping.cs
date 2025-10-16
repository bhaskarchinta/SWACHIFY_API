using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class master_service_mapping
{
    public int id { get; set; }

    public long service_id { get; set; }

    public long dept_id { get; set; }

    public bool? is_active { get; set; }

    public virtual master_department dept { get; set; } = null!;

    public virtual master_service service { get; set; } = null!;
}
