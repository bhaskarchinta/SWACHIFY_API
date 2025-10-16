using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class service_booking
{
    public long id { get; set; }

    public string? booking_id { get; set; }

    public long dept_id { get; set; }

    public long service_id { get; set; }

    public int slot_id { get; set; }

    public long? created_by { get; set; }

    public DateTime? created_date { get; set; }

    public long? modified_by { get; set; }

    public DateTime? modified_date { get; set; }

    public bool? is_active { get; set; }

    public virtual user_registration? created_byNavigation { get; set; }

    public virtual master_department dept { get; set; } = null!;

    public virtual user_registration? modified_byNavigation { get; set; }

    public virtual master_service service { get; set; } = null!;

    public virtual master_slot slot { get; set; } = null!;
}
