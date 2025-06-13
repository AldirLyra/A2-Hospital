namespace A2_Hospital.dtos
{
    public class AtendimentoDto
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string Local { get; set; }
        public string ProfissionalNome { get; set; }
    }
}
