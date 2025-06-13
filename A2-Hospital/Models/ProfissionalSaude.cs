using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2_Hospital.Models
{
    public class ProfissionalSaude
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string RegistroConselho { get; set; }
        public string TipoRegistro { get; set; }
        public Guid EspecialidadeId { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int CargaHorariaSemanal { get; set; }
        public string Turno { get; set; }
        public bool Ativo { get; set; }
        [JsonIgnore]
        public Especialidade Especialidade { get; set; }
        [JsonIgnore]
        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
        [JsonIgnore]
        public ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
    }
}

