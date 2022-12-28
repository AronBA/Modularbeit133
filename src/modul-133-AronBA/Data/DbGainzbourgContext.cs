using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using modul_133_AronBA.Models;

namespace modul_133_AronBA.Data;

public partial class DbGainzbourgContext : DbContext
{
    public DbGainzbourgContext()
    {
    }

    public DbGainzbourgContext(DbContextOptions<DbGainzbourgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAusruestung> TblAusruestungs { get; set; }

    public virtual DbSet<TblGruppenkur> TblGruppenkurs { get; set; }

    public virtual DbSet<TblMitglied> TblMitglieds { get; set; }

    public virtual DbSet<TblTrainer> TblTrainers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTrainer>()
            .ToTable(tb => tb.HasTrigger("trg_OnDeleteTrainer"));

        modelBuilder.Entity<TblAusruestung>(entity =>
        {
            entity.HasKey(e => e.ArtNr).HasName("pk_Ausruestung");

            entity.Property(e => e.ArtNr).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblGruppenkur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Gruppenkurs");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TblGruppenkurs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Gruppenkurs_Trainer");
        });

        modelBuilder.Entity<TblMitglied>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Mitglied");

            entity.Property(e => e.Ahv).IsFixedLength();

            entity.HasOne(d => d.Trainer).WithMany(p => p.TblMitglieds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Trainer");
        });

        modelBuilder.Entity<TblTrainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Trainer");
            entity.Property(e => e.Passwort).IsFixedLength();
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


   
}
