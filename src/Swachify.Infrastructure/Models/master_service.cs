using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public partial class master_service
{
    public long id { get; set; }

    public string service_name { get; set; } = null!;

    public bool? is_active { get; set; }

    public decimal? regular { get; set; }

    public decimal? premium { get; set; }

    public decimal? ultimate { get; set; }

    public bool? is_regular { get; set; }

    public bool? is_premium { get; set; }

    public bool? is_ultimate { get; set; }

    public virtual ICollection<master_service_mapping> master_service_mappings { get; set; } = new List<master_service_mapping>();

    public virtual ICollection<service_booking> service_bookings { get; set; } = new List<service_booking>();

    public virtual ICollection<user_registration> user_registrations { get; set; } = new List<user_registration>();
}
