using System.Text.Json.Serialization;

namespace A2_Hospital.Models
{
    public class AltaHospitalar
    {
        public Guid Id { get; set; }
        public Guid InternacaoId { get; set; }
        [JsonIgnore]
        public Internacao Internacao { get; set; }
        public DateTime DataAlta { get; set; }
        public string CondicaoPaciente { get; set; }
        public string InstrucoesPosAlta { get; set; }
    }
}
