using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace modul_133_AronBA.Models;

[Table("tbl_Gruppenkurs")]
public partial class TblGruppenkur
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("trainerId")]
    public int TrainerId { get; set; }

    [Column("bezeichnung")]
    [StringLength(32, ErrorMessage = "Die Bezeichnung darf nur maximal 32 Zeichen haben")]
    [Required(ErrorMessage = "Bitte geben Sie eine Bezeichnung an")]
    public string? Bezeichnung { get; set; }

    [Column("beginn", TypeName = "datetime")]
    [Required(ErrorMessage = "Bitte geben Sie ein Startpunkt an")]
    public DateTime? Beginn { get; set; }

    [Column("ende", TypeName = "datetime")]
    [Required(ErrorMessage = "Bitte geben Sie ein Ende an ")]
    public DateTime? Ende { get; set; }

    [Column("beschreibung", TypeName = "text")]
    [Required(ErrorMessage = "Bitte geben Sie eine Beschreibung an")]
    public string? Beschreibung { get; set; }

    [ForeignKey("TrainerId")]
    [InverseProperty("TblGruppenkurs")]
    public virtual TblTrainer? Trainer { get; set; }
}
