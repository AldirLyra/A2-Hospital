namespace A2_Hospital.dtos.estatisticas
{
    public class AtendimentosEstatisticasDto
    {
        public int TotalAtendimentos { get; set; }
        public Dictionary<string, int> PorStatus { get; set; }
        public Dictionary<string, int> PorTipo { get; set; }
        public Dictionary<string, int> PorPeriodo { get; set; }
    }
}
