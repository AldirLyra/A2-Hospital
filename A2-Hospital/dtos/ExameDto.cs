namespace A2_Hospital.dtos
{
    public class ExameDto
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public string Resultado { get; set; }
    }
}
