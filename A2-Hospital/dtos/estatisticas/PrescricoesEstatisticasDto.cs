namespace A2_Hospital.dtos.estatisticas
{
    public class PrescricoesEstatisticasDto
    {
        public int TotalPrescricoes { get; set; }
        public Dictionary<string, int> PorMedicamento { get; set; }
        public Dictionary<string, int> PorStatus { get; set; }
        public Dictionary<string, int> PorPeriodo { get; set; }
    }
}
