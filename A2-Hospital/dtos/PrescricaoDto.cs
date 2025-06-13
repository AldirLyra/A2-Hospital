namespace A2_Hospital.dtos
{
    public class PrescricaoDto
    {
        public Guid Id { get; set; }
        public string Medicamento { get; set; }
        public string Dosagem { get; set; }
        public string Frequencia { get; set; }
        public string ViaAdministracao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string StatusPrescricao { get; set; }
    }
}
