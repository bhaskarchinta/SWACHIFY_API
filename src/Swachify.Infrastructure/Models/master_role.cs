using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class master_role
{
    public int id { get; set; }

    public string role_name { get; set; } = null!;

    public bool? is_active { get; set; }

    public virtual ICollection<user_registration> user_registrations { get; set; } = new List<user_registration>();
}
