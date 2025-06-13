using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2_Hospital.Models
{
    public class Exame
    {
        public Guid Id { get; set; }
        [Required] public string Tipo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public string Resultado { get; set; }
        public Guid AtendimentoId { get; set; }
        [JsonIgnore]
        public Atendimento Atendimento { get; set; }
    }
}
