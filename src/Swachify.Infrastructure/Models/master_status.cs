using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public partial class master_status
{
    public long id { get; set; }

    public string status { get; set; } = null!;

    public bool? is_active { get; set; }

    public virtual ICollection<service_booking> service_bookings { get; set; } = new List<service_booking>();
}
