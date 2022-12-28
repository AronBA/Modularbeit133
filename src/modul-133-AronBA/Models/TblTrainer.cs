using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace modul_133_AronBA.Models;

[Table("tbl_Trainer")]
public partial class TblTrainer
{
    [Key]
    [Column("id")]
    public int Id { get; set; } 

    [Column("benutzername")]
    [StringLength(10, ErrorMessage = "Der Benutzername ist zu lang")]
    [Required(ErrorMessage = "Bitte gib einen Benutzernamen an")]
    public string Benutzername { get; set; } = null!;

    [Column("vorname")]
    [StringLength(32 ,ErrorMessage = "Der Nachname ist zu lang")]
    [Required(ErrorMessage = "Bitte gib einen Namen an")]
    public string? Vorname { get; set; } 

    [Column("nachname")]
    [StringLength(32, ErrorMessage = "Der Nachname ist zu lang")]
    [Required(ErrorMessage = "Bitte gib einen Namen an")]
    public string? Nachname { get; set; } 

    [Column("passwort")]
    [Required(ErrorMessage = "Bitte gib ein Passwort an")]
    public string Passwort { get; set; } = null!;

    [Column("headcoach")]
    public bool Headcoach { get; set; }
    [Column("deleted")]
    public bool Deleted { get; set; }

    [InverseProperty("Trainer")]
    public virtual ICollection<TblGruppenkur> TblGruppenkurs { get; } = new List<TblGruppenkur>();

    [InverseProperty("Trainer")]
    public virtual ICollection<TblMitglied> TblMitglieds { get; } = new List<TblMitglied>();

  
}
