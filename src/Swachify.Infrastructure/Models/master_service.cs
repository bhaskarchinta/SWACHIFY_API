using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class master_service
{
    public long id { get; set; }

    public string service_name { get; set; } = null!;

    public bool? is_active { get; set; }

    public virtual ICollection<master_service_mapping> master_service_mappings { get; set; } = new List<master_service_mapping>();

    public virtual ICollection<service_booking> service_bookings { get; set; } = new List<service_booking>();
}
