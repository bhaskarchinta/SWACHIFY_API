using System;
using System.Collections.Generic;

namespace Swachify.Infrastructure.Models;

public class user_registration
{
    public long id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public int? role_id { get; set; }

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public long? dept_id { get; set; }

    public string mobile { get; set; } = null!;

    public int? age { get; set; }

    public int? gender_id { get; set; }

    public long? created_by { get; set; }

    public DateTime? created_date { get; set; }

    public long? modified_by { get; set; }

    public DateTime? modified_date { get; set; }

    public bool? is_active { get; set; }

    public long? location_id { get; set; }

    public virtual ICollection<customer_complaint> customer_complaintcreated_byNavigations { get; set; } = new List<customer_complaint>();

    public virtual ICollection<customer_complaint> customer_complaintmodified_byNavigations { get; set; } = new List<customer_complaint>();

    public virtual ICollection<customer_complaint> customer_complaintusers { get; set; } = new List<customer_complaint>();

    public virtual master_department? dept { get; set; }

    public virtual master_gender? gender { get; set; }

    public virtual master_location? location { get; set; }

    public virtual master_role? role { get; set; }

    public virtual ICollection<service_booking> service_bookingcreated_byNavigations { get; set; } = new List<service_booking>();

    public virtual ICollection<service_booking> service_bookingmodified_byNavigations { get; set; } = new List<service_booking>();

    public virtual ICollection<user_auth> user_authcreated_byNavigations { get; set; } = new List<user_auth>();

    public virtual ICollection<user_auth> user_authmodified_byNavigations { get; set; } = new List<user_auth>();

    public virtual ICollection<user_auth> user_authusers { get; set; } = new List<user_auth>();
}
