using A2_Hospital.Data;
using A2_Hospital.dtos.formularios;
using A2_Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A2_Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly HospitalContext _context;

        public PacientesController(HospitalContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[SwaggerOperation(Summary = "Obtém a lista de pacientes", Description = "Retorna todos os pacientes cadastrados no sistema.")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            try
            {
                var pacientes = await _context.Pacientes
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Prescricoes)
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Exames)
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Internacao)
                                .ThenInclude(i => i.AltaHospitalar)
                    .Include(p => p.Atendimentos)
                    .Include(p => p.Internacoes)
                        .ThenInclude(i => i.AltaHospitalar)
                    .ToListAsync();
                Console.WriteLine($"Pacientes: {pacientes.Count}, Internações: {pacientes.SelectMany(p => p.Internacoes).Count()}, Prescrições: {pacientes.SelectMany(p => p.Prontuarios).SelectMany(pr => pr.Atendimentos).SelectMany(a => a.Prescricoes).Count()}, Exames: {pacientes.SelectMany(p => p.Prontuarios).SelectMany(pr => pr.Atendimentos).SelectMany(a => a.Exames).Count()}");
                return pacientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar pacientes: {ex.Message}");
                return StatusCode(500, "Erro interno ao buscar pacientes.");
            }
        }

        [HttpGet("{id}")]
        //[SwaggerOperation(Summary = "Obtém um paciente por ID", Description = "Retorna os detalhes de um paciente específico.")]
        public async Task<ActionResult<Paciente>> GetPaciente(Guid id)
        {
            try
            {
                var paciente = await _context.Pacientes
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Prescricoes)
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Exames)
                    .Include(p => p.Prontuarios)
                        .ThenInclude(pr => pr.Atendimentos)
                            .ThenInclude(a => a.Internacao)
                                .ThenInclude(i => i.AltaHospitalar)
                    .Include(p => p.Atendimentos)
                    .Include(p => p.Internacoes)
                        .ThenInclude(i => i.AltaHospitalar)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (paciente == null) return NotFound();

                return paciente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar paciente: {ex.Message}");
                return StatusCode(500, "Erro interno ao buscar paciente.");
            }
        }

        //[HttpPost]
        ////[SwaggerOperation(Summary = "Cria um novo paciente", Description = "Adiciona um novo paciente ao sistema.")]
        //public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        //{
        //    _context.Pacientes.Add(paciente);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Paciente>> PostPaciente(PacienteFormularioDto dto)
        {
            var paciente = new Paciente
            {
                Id = Guid.NewGuid(),
                NomeCompleto = dto.NomeCompleto,
                CPF = dto.Cpf,
                DataNascimento = dto.DataNascimento,
                Sexo = dto.Sexo,
                Telefone = dto.Telefone,
                EnderecoCompleto = dto.EnderecoCompleto,
                NumeroCartaoSUS = dto.NumeroCartaoSUS,
                PossuiPlanoSaude = dto.PossuiPlanoSaude,
                Prontuarios = new List<Prontuario>(),
                Atendimentos = new List<Atendimento>(),
                Internacoes = new List<Internacao>()
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        //[SwaggerOperation(Summary = "Atualiza um paciente", Description = "Atualiza os dados de um paciente existente.")]
        public async Task<IActionResult> PutPaciente(Guid id, Paciente paciente)
        {
            if (id != paciente.Id) return BadRequest();
            _context.Entry(paciente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pacientes.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Summary = "Remove um paciente", Description = "Remove um paciente do sistema.")]
        public async Task<IActionResult> DeletePaciente(Guid id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
