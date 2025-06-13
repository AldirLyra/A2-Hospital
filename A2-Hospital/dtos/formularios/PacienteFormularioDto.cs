namespace A2_Hospital.dtos.formularios
{
    public class PacienteFormularioDto
    {
        public string NomeCompleto { get; set; } = null!; 
        public string Cpf { get; set; } = null!; 
        public DateTime DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? Telefone { get; set; }
        public string? EnderecoCompleto { get; set; }
        public string? NumeroCartaoSUS { get; set; }
        public bool PossuiPlanoSaude { get; set; }
    }
}
