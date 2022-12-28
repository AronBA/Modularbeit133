using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace modul_133_AronBA.Models;

[Table("tbl_Ausruestung")]
public partial class TblAusruestung
{
    [Key]
    [Column("artNr")]
    [Required(ErrorMessage = "Bitte geben Sie eine Artikelnummber an")]
    [Range(0,9999999999999, ErrorMessage = "Die Artikel nummer muss zwischen 0 und 9999999999999 liegen")]
    public int ArtNr { get; set; }

    [Column("bezeichnung")]
    [StringLength(32,ErrorMessage = "Die Bezeichnung darf nur maximal 32 Zeichen haben")]
    [Required(ErrorMessage = "Bitte geben Sie eine Bezeichnung an")]
    public string? Bezeichnung { get; set; }

    [Column("gewichtKg")]
    [Required(ErrorMessage = "Bitte geben Sie das Gewicht an")]
    public short? GewichtKg { get; set; }

    [Column("anzahl")]
    [Required(ErrorMessage = "Bitte geben Sie die Anzahl an")]
    public byte? Anzahl { get; set; }
}
