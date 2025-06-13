using A2_Hospital.Data;
using A2_Hospital.dtos.estatisticas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace A2_Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatisticasController : ControllerBase
    {
        private readonly HospitalContext _context;

        public EstatisticasController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/Estatisticas/Pacientes
        [HttpGet("Pacientes")]
        public async Task<ActionResult<PacientesEstatisticasDto>> GetEstatisticasPacientes()
        {
            var totalPacientes = await _context.Pacientes.CountAsync();

            var porSexo = await _context.Pacientes
                .GroupBy(p => p.Sexo)
                .Select(g => new { Sexo = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Sexo ?? "Desconhecido", g => g.Count);

            var porEstadoCivil = await _context.Pacientes
                .GroupBy(p => p.EstadoCivil)
                .Select(g => new { EstadoCivil = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.EstadoCivil ?? "Desconhecido", g => g.Count);

            var porFaixaEtaria = await _context.Pacientes
                .Select(p => new
                {
                    Idade = DateTime.Now.Year - p.DataNascimento.Year,
                    Faixa = (DateTime.Now.Year - p.DataNascimento.Year) < 18 ? "0-17" :
                            (DateTime.Now.Year - p.DataNascimento.Year) < 30 ? "18-29" :
                            (DateTime.Now.Year - p.DataNascimento.Year) < 45 ? "30-44" :
                            (DateTime.Now.Year - p.DataNascimento.Year) < 60 ? "45-59" : "60+"
                })
                .GroupBy(p => p.Faixa)
                .Select(g => new { Faixa = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Faixa, g => g.Count);

            var dto = new PacientesEstatisticasDto
            {
                TotalPacientes = totalPacientes,
                PorSexo = porSexo,
                PorEstadoCivil = porEstadoCivil,
                PorFaixaEtaria = porFaixaEtaria
            };

            return Ok(dto);
        }

        // GET: api/Estatisticas/Atendimentos
        [HttpGet("Atendimentos")]
        public async Task<ActionResult<AtendimentosEstatisticasDto>> GetEstatisticasAtendimentos()
        {
            var totalAtendimentos = await _context.Atendimentos.CountAsync();
            if (totalAtendimentos == 0)
                return Ok(new AtendimentosEstatisticasDto
                {
                    TotalAtendimentos = 0,
                    PorStatus = new Dictionary<string, int>(),
                    PorTipo = new Dictionary<string, int>(),
                    PorPeriodo = new Dictionary<string, int>()
                });

            var porStatus = await _context.Atendimentos
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Status ?? "Desconhecido", g => g.Count);

            var porTipo = await _context.Atendimentos
                .GroupBy(a => a.Tipo)
                .Select(g => new { Tipo = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Tipo ?? "Desconhecido", g => g.Count);

            var atendimentos = await _context.Atendimentos
                .Select(a => new { a.DataHora })
                .ToListAsync();

            var porPeriodo = atendimentos
                .GroupBy(a => a.DataHora.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Count());

            var dto = new AtendimentosEstatisticasDto
            {
                TotalAtendimentos = totalAtendimentos,
                PorStatus = porStatus,
                PorTipo = porTipo,
                PorPeriodo = porPeriodo
            };

            return Ok(dto);
        }

        // GET: api/Estatisticas/Internacoes
        [HttpGet("Internacoes")]
        public async Task<ActionResult<InternacoesEstatisticasDto>> GetEstatisticasInternacoes()
        {
            var totalInternacoes = await _context.Internacoes.CountAsync();
            if (totalInternacoes == 0)
                return Ok(new InternacoesEstatisticasDto
                {
                    TotalInternacoes = 0,
                    InternacoesAtivas = 0,
                    InternacoesConcluidas = 0,
                    PorSetor = new Dictionary<string, int>(),
                    MediaDiasInternacao = 0
                });

            var internacoesAtivas = await _context.Internacoes
                .Where(i => i.StatusInternacao == "Ativa")
                .CountAsync();

            var internacoesConcluidas = await _context.Internacoes
                .Where(i => i.StatusInternacao == "Concluída")
                .CountAsync();

            var porSetor = await _context.Internacoes
                .GroupBy(i => i.Setor)
                .Select(g => new { Setor = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Setor ?? "Desconhecido", g => g.Count);

            double mediaDiasInternacao = 0;
            var internacoesComAlta = await _context.Internacoes
                .Include(i => i.AltaHospitalar)
                .Where(i => i.AltaHospitalar != null)
                .ToListAsync();

            if (internacoesComAlta.Any())
            {
                mediaDiasInternacao = internacoesComAlta
                    .Average(i => (i.AltaHospitalar.DataAlta - i.DataEntrada).TotalDays);
            }

            var dto = new InternacoesEstatisticasDto
            {
                TotalInternacoes = totalInternacoes,
                InternacoesAtivas = internacoesAtivas,
                InternacoesConcluidas = internacoesConcluidas,
                PorSetor = porSetor,
                MediaDiasInternacao = Math.Round(mediaDiasInternacao, 2)
            };

            return Ok(dto);
        }

        // GET: api/Estatisticas/Exames
        [HttpGet("Exames")]
        public async Task<ActionResult<ExamesEstatisticasDto>> GetEstatisticasExames()
        {
            var totalExames = await _context.Exames.CountAsync();
            if (totalExames == 0)
                return Ok(new ExamesEstatisticasDto
                {
                    TotalExames = 0,
                    PorTipo = new Dictionary<string, int>(),
                    PorStatus = new Dictionary<string, int>(),
                    PorPeriodo = new Dictionary<string, int>()
                });

            var porTipo = await _context.Exames
                .GroupBy(e => e.Tipo)
                .Select(g => new { Tipo = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Tipo ?? "Desconhecido", g => g.Count);

            var porStatus = await _context.Exames
                .GroupBy(e => e.DataRealizacao.HasValue ? "Realizado" : "Solicitado")
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Status, g => g.Count);

            var exames = await _context.Exames
                .Select(e => new { e.DataSolicitacao })
                .ToListAsync();

            var porPeriodo = exames
                .GroupBy(e => e.DataSolicitacao.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Count());

            var dto = new ExamesEstatisticasDto
            {
                TotalExames = totalExames,
                PorTipo = porTipo,
                PorStatus = porStatus,
                PorPeriodo = porPeriodo
            };

            return Ok(dto);
        }

        /// <summary>
        /// Obter estatísticas de prescrições
        /// </summary>
        /// <returns>Estatísticas sobre as prescrições, incluindo total, medicamento, status e período.</returns>
        [HttpGet("Prescricoes")]
        public async Task<ActionResult<PrescricoesEstatisticasDto>> GetEstatisticasPrescricoes()
        {
            var totalPrescricoes = await _context.Prescricoes.CountAsync();
            if (totalPrescricoes == 0)
                return Ok(new PrescricoesEstatisticasDto
                {
                    TotalPrescricoes = 0,
                    PorMedicamento = new Dictionary<string, int>(),
                    PorStatus = new Dictionary<string, int>(),
                    PorPeriodo = new Dictionary<string, int>()
                });

            var porMedicamento = await _context.Prescricoes
                .GroupBy(p => p.Medicamento)
                .Select(g => new { Medicamento = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Medicamento ?? "Desconhecido", g => g.Count);

            var porStatus = await _context.Prescricoes
                .GroupBy(p => p.StatusPrescricao)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Status ?? "Desconhecido", g => g.Count);

            var prescricoes = await _context.Prescricoes
                .Select(p => new { p.DataInicio })
                .ToListAsync();

            var porPeriodo = prescricoes
                .GroupBy(p => p.DataInicio.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Count());

            var dto = new PrescricoesEstatisticasDto
            {
                TotalPrescricoes = totalPrescricoes,
                PorMedicamento = porMedicamento,
                PorStatus = porStatus,
                PorPeriodo = porPeriodo
            };

            return Ok(dto);
        }
    }
}
