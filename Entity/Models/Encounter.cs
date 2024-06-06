using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

[Table("Encounter")]
public partial class Encounter
{
    [Key]
    public int EncounterId { get; set; }

    public int RequestId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? MedicalHistory { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Medications { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Allergies { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Temp { get; set; }

    [Column("HR")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Hr { get; set; }

    [Column("RR")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Rr { get; set; }

    [Column("Blood Pressure(S)")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BloodPressureS { get; set; }

    [Column("Blood Pressure(D)")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BloodPressureD { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? O2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Pain { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Heent { get; set; }

    [Column("CV")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Cv { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Chest { get; set; }

    [Column("ABD")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Abd { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Extr { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Skin { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Neuro { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Other { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Diagnosis { get; set; }

    [Column("Treatment Plan")]
    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentPlan { get; set; }

    [Column("Medications Dispensed")]
    [StringLength(200)]
    [Unicode(false)]
    public string? MedicationsDispensed { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Procedures { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Followup { get; set; }

    [Column("location")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Location { get; set; }

    [Column("History Of Present Illness Or Injury")]
    [StringLength(200)]
    [Unicode(false)]
    public string? HistoryOfPresentIllnessOrInjury { get; set; }

    public bool? IsFinalize { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("Encounters")]
    public virtual Request Request { get; set; } = null!;
}
