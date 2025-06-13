namespace A2_Hospital.dtos.estatisticas
{
    public class PacientesEstatisticasDto
    {
        public int TotalPacientes { get; set; }
        public Dictionary<string, int> PorSexo { get; set; }
        public Dictionary<string, int> PorEstadoCivil { get; set; }
        public Dictionary<string, int> PorFaixaEtaria { get; set; }
    }
}
