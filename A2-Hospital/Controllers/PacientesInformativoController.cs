using A2_Hospital.Data;
using A2_Hospital.dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A2_Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesInformativoController : ControllerBase
    {
        private readonly HospitalContext _context;

        public PacientesInformativoController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/PacientesInformativo/{id}/Detalhes
        [HttpGet("{id}/Detalhes")]
        public async Task<ActionResult<PacienteDetalhesDto>> GetPacienteDetalhes(Guid id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Prontuarios)
                .Include(p => p.Atendimentos).ThenInclude(a => a.Profissional)
                .Include(p => p.Atendimentos).ThenInclude(a => a.Prescricoes)
                .Include(p => p.Atendimentos).ThenInclude(a => a.Exames)
                .Include(p => p.Internacoes).ThenInclude(i => i.AltaHospitalar)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
                return NotFound();

            var dto = new PacienteDetalhesDto
            {
                Id = paciente.Id,
                NomeCompleto = paciente.NomeCompleto,
                CPF = paciente.CPF,
                DataNascimento = paciente.DataNascimento,
                Sexo = paciente.Sexo,
                TipoSanguineo = paciente.TipoSanguineo,
                Telefone = paciente.Telefone,
                Email = paciente.Email,
                EnderecoCompleto = paciente.EnderecoCompleto,
                NumeroCartaoSUS = paciente.NumeroCartaoSUS,
                EstadoCivil = paciente.EstadoCivil,
                PossuiPlanoSaude = paciente.PossuiPlanoSaude,
                Prontuario = paciente.Prontuarios.FirstOrDefault() != null ? new ProntuarioDto
                {
                    Id = paciente.Prontuarios.First().Id,
                    NumeroProntuario = paciente.Prontuarios.First().NumeroProntuario,
                    DataAbertura = paciente.Prontuarios.First().DataAbertura,
                    ObservacoesGerais = paciente.Prontuarios.First().ObservacoesGerais
                } : null,
                Atendimentos = paciente.Atendimentos.Select(a => new AtendimentoDto
                {
                    Id = a.Id,
                    DataHora = a.DataHora,
                    Tipo = a.Tipo,
                    Status = a.Status,
                    Local = a.Local,
                    ProfissionalNome = a.Profissional.NomeCompleto
                }).ToList(),
                Prescricoes = paciente.Atendimentos.SelectMany(a => a.Prescricoes).Select(p => new PrescricaoDto
                {
                    Id = p.Id,
                    Medicamento = p.Medicamento,
                    Dosagem = p.Dosagem,
                    Frequencia = p.Frequencia,
                    ViaAdministracao = p.ViaAdministracao,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim,
                    StatusPrescricao = p.StatusPrescricao
                }).ToList(),
                Exames = paciente.Atendimentos.SelectMany(a => a.Exames).Select(e => new ExameDto
                {
                    Id = e.Id,
                    Tipo = e.Tipo,
                    DataSolicitacao = e.DataSolicitacao,
                    DataRealizacao = e.DataRealizacao,
                    Resultado = e.Resultado
                }).ToList(),
                Internacoes = paciente.Internacoes.Select(i => new InternacaoDto
                {
                    Id = i.Id,
                    DataEntrada = i.DataEntrada,
                    PrevisaoAlta = i.PrevisaoAlta,
                    MotivoInternacao = i.MotivoInternacao,
                    Leito = i.Leito,
                    Quarto = i.Quarto,
                    Setor = i.Setor,
                    StatusInternacao = i.StatusInternacao
                }).ToList(),
                Altas = paciente.Internacoes.Where(i => i.AltaHospitalar != null).Select(i => new AltaHospitalarDto
                {
                    Id = i.AltaHospitalar.Id,
                    DataAlta = i.AltaHospitalar.DataAlta,
                    CondicaoPaciente = i.AltaHospitalar.CondicaoPaciente,
                    InstrucoesPosAlta = i.AltaHospitalar.InstrucoesPosAlta
                }).ToList()
            };

            return Ok(dto);
        }

        // GET: api/PacientesInformativo/{id}/Atendimentos
        [HttpGet("{id}/Atendimentos")]
        public async Task<ActionResult<IEnumerable<AtendimentoDto>>> GetAtendimentos(Guid id, [FromQuery] string? status, [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            var query = _context.Atendimentos
                .Where(a => a.PacienteId == id)
                .Include(a => a.Profissional)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(a => a.Status == status);

            if (dataInicio.HasValue)
                query = query.Where(a => a.DataHora >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(a => a.DataHora <= dataFim.Value);

            var atendimentos = await query
                .Select(a => new AtendimentoDto
                {
                    Id = a.Id,
                    DataHora = a.DataHora,
                    Tipo = a.Tipo,
                    Status = a.Status,
                    Local = a.Local,
                    ProfissionalNome = a.Profissional.NomeCompleto
                })
                .ToListAsync();

            return Ok(atendimentos);
        }

        // GET: api/PacientesInformativo/{id}/Prescricoes
        [HttpGet("{id}/Prescricoes")]
        public async Task<ActionResult<IEnumerable<PrescricaoDto>>> GetPrescricoes(Guid id, [FromQuery] string? status)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            var prescricoes = await _context.Prescricoes
                .Where(p => p.Atendimento.PacienteId == id)
                .Where(p => string.IsNullOrEmpty(status) || p.StatusPrescricao == status)
                .Select(p => new PrescricaoDto
                {
                    Id = p.Id,
                    Medicamento = p.Medicamento,
                    Dosagem = p.Dosagem,
                    Frequencia = p.Frequencia,
                    ViaAdministracao = p.ViaAdministracao,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim,
                    StatusPrescricao = p.StatusPrescricao
                })
                .ToListAsync();

            return Ok(prescricoes);
        }

        // GET: api/PacientesInformativo/{id}/Exames
        [HttpGet("{id}/Exames")]
        public async Task<ActionResult<IEnumerable<ExameDto>>> GetExames(Guid id, [FromQuery] string? tipo, [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            var query = _context.Exames
                .Where(e => e.Atendimento.PacienteId == id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(e => e.Tipo == tipo);

            if (dataInicio.HasValue)
                query = query.Where(e => e.DataSolicitacao >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(e => e.DataSolicitacao <= dataFim.Value);

            var exames = await query
                .Select(e => new ExameDto
                {
                    Id = e.Id,
                    Tipo = e.Tipo,
                    DataSolicitacao = e.DataSolicitacao,
                    DataRealizacao = e.DataRealizacao,
                    Resultado = e.Resultado
                })
                .ToListAsync();

            return Ok(exames);
        }

        // GET: api/PacientesInformativo/{id}/Internacoes
        [HttpGet("{id}/Internacoes")]
        public async Task<ActionResult<IEnumerable<InternacaoDto>>> GetInternacoes(Guid id, [FromQuery] string? status)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            var internacoes = await _context.Internacoes
                .Where(i => i.PacienteId == id)
                .Where(i => string.IsNullOrEmpty(status) || i.StatusInternacao == status)
                .Select(i => new InternacaoDto
                {
                    Id = i.Id,
                    DataEntrada = i.DataEntrada,
                    PrevisaoAlta = i.PrevisaoAlta,
                    MotivoInternacao = i.MotivoInternacao,
                    Leito = i.Leito,
                    Quarto = i.Quarto,
                    Setor = i.Setor,
                    StatusInternacao = i.StatusInternacao
                })
                .ToListAsync();

            return Ok(internacoes);
        }

        // GET: api/PacientesInformativo/Buscar
        [HttpGet("Buscar")]
        public async Task<ActionResult<IEnumerable<PacienteResumoDto>>> BuscarPacientes(
            [FromQuery] string? nome,
            [FromQuery] string? cpf,
            [FromQuery] string? cartaoSus,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10)
        {
            var query = _context.Pacientes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(p => p.NomeCompleto.Contains(nome));

            if (!string.IsNullOrEmpty(cpf))
                query = query.Where(p => p.CPF == cpf);

            if (!string.IsNullOrEmpty(cartaoSus))
                query = query.Where(p => p.NumeroCartaoSUS == cartaoSus);

            var pacientes = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .Select(p => new PacienteResumoDto
                {
                    Id = p.Id,
                    NomeCompleto = p.NomeCompleto,
                    CPF = p.CPF,
                    NumeroCartaoSUS = p.NumeroCartaoSUS
                })
                .ToListAsync();

            return Ok(pacientes);
        }
    }
}
