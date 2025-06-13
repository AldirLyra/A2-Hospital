namespace A2_Hospital.dtos
{
    public class AltaHospitalarDto
    {
        public Guid Id { get; set; }
        public DateTime DataAlta { get; set; }
        public string CondicaoPaciente { get; set; }
        public string InstrucoesPosAlta { get; set; }
    }
}
