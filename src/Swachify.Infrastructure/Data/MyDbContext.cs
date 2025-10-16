using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Swachify.Infrastructure.Models;

namespace Swachify.Infrastructure.Data;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<customer_complaint> customer_complaints { get; set; }

    public DbSet<master_department> master_departments { get; set; }

    public DbSet<master_gender> master_genders { get; set; }

    public DbSet<master_location> master_locations { get; set; }

    public DbSet<master_role> master_roles { get; set; }

    public DbSet<master_service> master_services { get; set; }

    public DbSet<master_service_mapping> master_service_mappings { get; set; }

    public DbSet<master_slot> master_slots { get; set; }

    public DbSet<service_booking> service_bookings { get; set; }

    public DbSet<user_auth> user_auths { get; set; }

    public DbSet<user_registration> user_registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<customer_complaint>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_customer_complaints_id");

            entity.Property(e => e.address).HasColumnType("character varying");
            entity.Property(e => e.created_date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.modified_date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.customer_complaintcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("fk_customer_complaints_created_by");

            entity.HasOne(d => d.location).WithMany(p => p.customer_complaints)
                .HasForeignKey(d => d.location_id)
                .HasConstraintName("fk_customer_complaints_location_id");

            entity.HasOne(d => d.modified_byNavigation).WithMany(p => p.customer_complaintmodified_byNavigations)
                .HasForeignKey(d => d.modified_by)
                .HasConstraintName("fk_customer_complaints_modified_by");

            entity.HasOne(d => d.user).WithMany(p => p.customer_complaintusers)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_complaints_user_id");
        });

        modelBuilder.Entity<master_department>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_department_id");

            entity.ToTable("master_department");

            entity.HasIndex(e => e.department_name, "uk_master_department_department_name").IsUnique();

            entity.Property(e => e.department_name).HasMaxLength(255);
            entity.Property(e => e.is_active).HasDefaultValue(true);
        });

        modelBuilder.Entity<master_gender>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_gender_id");

            entity.ToTable("master_gender");

            entity.HasIndex(e => e.gender_name, "uk_master_gender_gender_name").IsUnique();

            entity.Property(e => e.gender_name).HasMaxLength(255);
            entity.Property(e => e.is_active).HasDefaultValue(true);
        });

        modelBuilder.Entity<master_location>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_location_id");

            entity.ToTable("master_location");

            entity.HasIndex(e => e.state_name, "uk_master_location_state_name").IsUnique();

            entity.Property(e => e.country_name).HasMaxLength(255);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.state_name).HasMaxLength(255);
        });

        modelBuilder.Entity<master_role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_role_id");

            entity.ToTable("master_role");

            entity.HasIndex(e => e.role_name, "uk_master_role_role_name").IsUnique();

            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.role_name).HasMaxLength(255);
        });

        modelBuilder.Entity<master_service>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_service_id");

            entity.ToTable("master_service");

            entity.HasIndex(e => e.service_name, "uk_master_service_service_name").IsUnique();

            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.service_name).HasMaxLength(255);
        });

        modelBuilder.Entity<master_service_mapping>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_service_mapping_id");

            entity.ToTable("master_service_mapping");

            entity.Property(e => e.is_active).HasDefaultValue(true);

            entity.HasOne(d => d.dept).WithMany(p => p.master_service_mappings)
                .HasForeignKey(d => d.dept_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_master_service_mapping_dept_id");

            entity.HasOne(d => d.service).WithMany(p => p.master_service_mappings)
                .HasForeignKey(d => d.service_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_master_service_mapping_service_id");
        });

        modelBuilder.Entity<master_slot>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_master_slots_id");

            entity.HasIndex(e => e.slot_time, "uk_master_slots_slot_time").IsUnique();

            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.slot_time).HasMaxLength(255);
        });

        modelBuilder.Entity<service_booking>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_service_booking_id");

            entity.ToTable("service_booking");

            entity.Property(e => e.booking_id).HasMaxLength(100);
            entity.Property(e => e.created_date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.modified_date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.service_bookingcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("fk_customer_complaints_created_by");

            entity.HasOne(d => d.dept).WithMany(p => p.service_bookings)
                .HasForeignKey(d => d.dept_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service_booking_dept_id");

            entity.HasOne(d => d.modified_byNavigation).WithMany(p => p.service_bookingmodified_byNavigations)
                .HasForeignKey(d => d.modified_by)
                .HasConstraintName("fk_customer_complaints_modified_by");

            entity.HasOne(d => d.service).WithMany(p => p.service_bookings)
                .HasForeignKey(d => d.service_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service_booking_service_id");

            entity.HasOne(d => d.slot).WithMany(p => p.service_bookings)
                .HasForeignKey(d => d.slot_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service_booking_slot_id");
        });

        modelBuilder.Entity<user_auth>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_user_auth_id");

            entity.ToTable("user_auth");

            entity.Property(e => e.created_date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.login_name).HasMaxLength(100);
            entity.Property(e => e.modified_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.password).HasMaxLength(500);

            entity.HasOne(d => d.created_byNavigation).WithMany(p => p.user_authcreated_byNavigations)
                .HasForeignKey(d => d.created_by)
                .HasConstraintName("fk_user_auth_created_by");

            entity.HasOne(d => d.modified_byNavigation).WithMany(p => p.user_authmodified_byNavigations)
                .HasForeignKey(d => d.modified_by)
                .HasConstraintName("fk_user_auth_modified_by");

            entity.HasOne(d => d.user).WithMany(p => p.user_authusers)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_auth_user_id");
        });

        modelBuilder.Entity<user_registration>(entity =>
        {
            entity.HasKey(e => e.id).HasName("pk_user_registration_id");

            entity.ToTable("user_registration");

            entity.HasIndex(e => e.email, "uk_user_registration_email").IsUnique();

            entity.Property(e => e.created_date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.first_name).HasMaxLength(255);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.last_name).HasMaxLength(255);
            entity.Property(e => e.mobile).HasMaxLength(15);
            entity.Property(e => e.modified_date).HasColumnType("timestamp without time zone");
            entity.Property(e => e.password).HasMaxLength(500);

            entity.HasOne(d => d.dept).WithMany(p => p.user_registrations)
                .HasForeignKey(d => d.dept_id)
                .HasConstraintName("fk_user_registration_dept_id");

            entity.HasOne(d => d.gender).WithMany(p => p.user_registrations)
                .HasForeignKey(d => d.gender_id)
                .HasConstraintName("fk_user_registration_gender_id");

            entity.HasOne(d => d.location).WithMany(p => p.user_registrations)
                .HasForeignKey(d => d.location_id)
                .HasConstraintName("fk_user_registration_location_id");

            entity.HasOne(d => d.role).WithMany(p => p.user_registrations)
                .HasForeignKey(d => d.role_id)
                .HasConstraintName("fk_user_registration_role_id");
        });
        base.OnModelCreating(modelBuilder);
    }
}
