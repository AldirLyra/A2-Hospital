namespace A2_Hospital.dtos
{
    public class PacienteDetalhesDto
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string TipoSanguineo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string EnderecoCompleto { get; set; }
        public string NumeroCartaoSUS { get; set; }
        public string EstadoCivil { get; set; }
        public bool PossuiPlanoSaude { get; set; }
        public ProntuarioDto? Prontuario { get; set; }
        public List<AtendimentoDto> Atendimentos { get; set; } = new();
        public List<PrescricaoDto> Prescricoes { get; set; } = new();
        public List<ExameDto> Exames { get; set; } = new();
        public List<InternacaoDto> Internacoes { get; set; } = new();
        public List<AltaHospitalarDto> Altas { get; set; } = new();
    }
}
