using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace A2_Hospital.Models
{
    public class Atendimento
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        [Required] public string Tipo { get; set; }
        [Required] public string Status { get; set; }
        public string Local { get; set; }
        public Guid PacienteId { get; set; }
        [JsonIgnore]
        public Paciente Paciente { get; set; }
        public Guid ProfissionalId { get; set; }
        [JsonIgnore]
        public ProfissionalSaude Profissional { get; set; }
        public Guid ProntuarioId { get; set; }
        [JsonIgnore]
        public Prontuario Prontuario { get; set; }
        public List<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
        public List<Exame> Exames { get; set; } = new List<Exame>();
        [JsonIgnore]
        public Internacao Internacao { get; set; }
    }
}
