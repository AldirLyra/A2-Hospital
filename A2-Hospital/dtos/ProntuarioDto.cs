namespace A2_Hospital.dtos
{
    public class ProntuarioDto
    {
        public Guid Id { get; set; }
        public string NumeroProntuario { get; set; }
        public DateTime DataAbertura { get; set; }
        public string ObservacoesGerais { get; set; }
    }
}
