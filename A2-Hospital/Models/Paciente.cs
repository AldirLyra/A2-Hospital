using System.ComponentModel.DataAnnotations;

namespace A2_Hospital.Models
{
    public class Paciente
    {
        public Guid Id { get; set; }
        [Required] public string NomeCompleto { get; set; }
        [Required] public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? EnderecoCompleto { get; set; }
        public string? NumeroCartaoSUS { get; set; }
        public string? EstadoCivil { get; set; }
        public bool PossuiPlanoSaude { get; set; }
        public ICollection<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
        public ICollection<Internacao> Internacoes { get; set; } = new List<Internacao>();
    }
}
