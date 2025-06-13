using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2_Hospital.Models
{
    public class Prontuario
    {
        public Guid Id { get; set; }
        [Required] public string NumeroProntuario { get; set; }
        public DateTime DataAbertura { get; set; }
        public string ObservacoesGerais { get; set; }
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public List<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    }
}
