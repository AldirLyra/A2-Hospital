using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2_Hospital.Models
{
    public class Especialidade
    {
        public Guid Id { get; set; }
        [Required] public string Nome { get; set; }

        [JsonIgnore]
        public List<ProfissionalSaude> Profissionais { get; set; } = new List<ProfissionalSaude>();
    }
}
