namespace A2_Hospital.dtos
{
    public class InternacaoDto
    {
        public Guid Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? PrevisaoAlta { get; set; }
        public string MotivoInternacao { get; set; }
        public string Leito { get; set; }
        public string Quarto { get; set; }
        public string Setor { get; set; }
        public string StatusInternacao { get; set; }
    }
}
