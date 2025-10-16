using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class master_slot
{
    public int id { get; set; }

    public string slot_time { get; set; } = null!;

    public bool? is_active { get; set; }

    public virtual ICollection<service_booking> service_bookings { get; set; } = new List<service_booking>();
}
