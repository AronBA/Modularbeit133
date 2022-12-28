using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace modul_133_AronBA.Models;

[Table("tbl_Mitglied")]
public partial class TblMitglied
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("trainerid")]
    public int Trainerid { get; set; }

    [Column("vorname")]
    [StringLength(32,ErrorMessage = "Dieser Name ist zu lang")]
    [Required(ErrorMessage = "Bitte geben Sie einen Vornamen an")]
    public string? Vorname { get; set; }

    [Column("nachname")]
    [StringLength(32, ErrorMessage = "Dieser Name ist zu lang")]
    [Required(ErrorMessage = "Bitte geben Sie einen Nachnamen an")]
    public string? Nachname { get; set; }

    [Column("AHV")]
    [StringLength(13, ErrorMessage = "Die AHV-Nummer ist zu lange")]
    [Required(ErrorMessage = "Bitte geben Sie eine AHV-Nummer an")]
    public string? Ahv { get; set; }

    [Column("mitgliedschaftAnfang", TypeName = "date")]
    [Required(ErrorMessage = "Bitte geben sie ein Anfangsdatum an")]
    public DateTime? MitgliedschaftAnfang { get; set; }

    [Column("mitgliedschaftEnde", TypeName = "date")]
    [Required(ErrorMessage = "Bitte geben Sie ein Ende an")]
    public DateTime? MitgliedschaftEnde { get; set; }

    [Column("gebursdatum", TypeName = "date")]
    [Required(ErrorMessage = "Bitte geben Sie ein Geburtsdatum an")]
    public DateTime? Gebursdatum { get; set; }

    [Column("mail")]
    [StringLength(32, ErrorMessage = "Diese Emailadresse ist zu lang")]
    [Required(ErrorMessage = "Bitte geben Sie eine Email an")]
    public string? Mail { get; set; }

    [ForeignKey("Trainerid")]
    [InverseProperty("TblMitglieds")]
  
    public virtual TblTrainer? Trainer { get; set; }
}
