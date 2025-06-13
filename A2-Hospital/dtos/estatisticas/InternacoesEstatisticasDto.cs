namespace A2_Hospital.dtos.estatisticas
{
    public class InternacoesEstatisticasDto
    {
        public int TotalInternacoes { get; set; }
        public int InternacoesAtivas { get; set; }
        public int InternacoesConcluidas { get; set; }
        public Dictionary<string, int> PorSetor { get; set; }
        public double MediaDiasInternacao { get; set; }
    }
}
