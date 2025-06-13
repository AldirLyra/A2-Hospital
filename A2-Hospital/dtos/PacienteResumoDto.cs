namespace A2_Hospital.dtos
{
    public class PacienteResumoDto
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string NumeroCartaoSUS { get; set; }
    }
}
