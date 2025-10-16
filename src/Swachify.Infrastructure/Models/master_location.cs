using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class master_location
{
    public long id { get; set; }

    public string country_name { get; set; } = null!;

    public string state_name { get; set; } = null!;

    public bool? is_active { get; set; }

    public virtual ICollection<customer_complaint> customer_complaints { get; set; } = new List<customer_complaint>();

    public virtual ICollection<user_registration> user_registrations { get; set; } = new List<user_registration>();
}
