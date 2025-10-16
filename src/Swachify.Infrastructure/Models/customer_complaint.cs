using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class customer_complaint
{
    public long id { get; set; }

    public long user_id { get; set; }

    public long? location_id { get; set; }

    public string? address { get; set; }

    public string description { get; set; } = null!;

    public long? created_by { get; set; }

    public DateTime? created_date { get; set; }

    public long? modified_by { get; set; }

    public DateTime? modified_date { get; set; }

    public bool? is_active { get; set; }

    public virtual user_registration? created_byNavigation { get; set; }

    public virtual master_location? location { get; set; }

    public virtual user_registration? modified_byNavigation { get; set; }

    public virtual user_registration user { get; set; } = null!;
}
