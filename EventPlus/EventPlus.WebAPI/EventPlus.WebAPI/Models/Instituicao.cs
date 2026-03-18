using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Instituicao")]
public partial class Instituicao
{
    [Key]
    public Guid IdInstituicao { get; set; }

    [StringLength(100)]
    public string Endereco { get; set; } = null!;

    [StringLength(100)]
    public string NomeFantasia { get; set; } = null!;

    [Column("CNPJ")]
    [StringLength(14)]
    public string? Cnpj { get; set; }

    [JsonIgnore]
    [InverseProperty("IdInstuicaoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
