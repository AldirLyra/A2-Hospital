using A2_Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace A2_Hospital.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new HospitalContext(
                serviceProvider.GetRequiredService<DbContextOptions<HospitalContext>>());

            Console.WriteLine("Iniciando SeedData.Initialize...");

            // Verificar conexão com o banco
            try
            {
                context.Database.OpenConnection();
                Console.WriteLine("Conexão com o banco estabelecida com sucesso.");
                context.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco: {ex.Message}");
                throw;
            }

            // Verificar e aplicar migrações
            var pendingMigrations = context.Database.GetPendingMigrations().Any();
            if (pendingMigrations)
            {
                Console.WriteLine("Aplicando migrações pendentes...");
                context.Database.Migrate();
                Console.WriteLine("Migrações aplicadas com sucesso.");
            }
            else
            {
                Console.WriteLine("Nenhuma migração pendente. Banco já está atualizado.");
            }

            // Limpar tabelas para garantir estado inicial
            Console.WriteLine("Limpando tabelas existentes...");
            context.AltasHospitalares.RemoveRange(context.AltasHospitalares);
            context.Internacoes.RemoveRange(context.Internacoes);
            context.Exames.RemoveRange(context.Exames);
            context.Prescricoes.RemoveRange(context.Prescricoes);
            context.Atendimentos.RemoveRange(context.Atendimentos);
            context.Prontuarios.RemoveRange(context.Prontuarios);
            context.Pacientes.RemoveRange(context.Pacientes);
            context.ProfissionaisSaude.RemoveRange(context.ProfissionaisSaude);
            context.Especialidades.RemoveRange(context.Especialidades);
            context.SaveChanges();
            Console.WriteLine("Tabelas limpas com sucesso.");

            try
            {
                // Inserir Especialidades
                Console.WriteLine("Inserindo Especialidades...");
                var especialidades = new Especialidade[]
                {
                new() { Id = Guid.NewGuid(), Nome = "Cardiologia" },
                new() { Id = Guid.NewGuid(), Nome = "Pediatria" },
                new() { Id = Guid.NewGuid(), Nome = "Ortopedia" },
                new() { Id = Guid.NewGuid(), Nome = "Neurologia" },
                new() { Id = Guid.NewGuid(), Nome = "Clínica Geral" },
                new() { Id = Guid.NewGuid(), Nome = "Ginecologia" },
                new() { Id = Guid.NewGuid(), Nome = "Dermatologia" },
                new() { Id = Guid.NewGuid(), Nome = "Oftalmologia" },
                new() { Id = Guid.NewGuid(), Nome = "Psiquiatria" },
                new() { Id = Guid.NewGuid(), Nome = "Endocrinologia" }
                };
                context.Especialidades.AddRange(especialidades);
                context.SaveChanges();
                Console.WriteLine($"Inseridas {especialidades.Length} especialidades.");

                // Capturar IDs gerados
                for (int i = 0; i < especialidades.Length; i++)
                {
                    Console.WriteLine($"Especialidade {especialidades[i].Nome} - Id: {especialidades[i].Id}");
                }

                // Inserir ProfissionaisSaude
                Console.WriteLine("Inserindo ProfissionaisSaude...");
                var profissionais = new ProfissionalSaude[]
                {
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. João Silva", CPF = "12345678901", Email = "joao.silva@hospital.com", Telefone = "11987654321", RegistroConselho = "CRM12345", TipoRegistro = "CRM", EspecialidadeId = especialidades[0].Id, DataAdmissao = DateTime.Now.AddYears(-5), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Maria Oliveira", CPF = "98765432109", Email = "maria.oliveira@hospital.com", Telefone = "11912345678", RegistroConselho = "CRM54321", TipoRegistro = "CRM", EspecialidadeId = especialidades[1].Id, DataAdmissao = DateTime.Now.AddYears(-3), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Pedro Santos", CPF = "45678912345", Email = "pedro.santos@hospital.com", Telefone = "11923456789", RegistroConselho = "CRM67890", TipoRegistro = "CRM", EspecialidadeId = especialidades[2].Id, DataAdmissao = DateTime.Now.AddYears(-4), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Ana Costa", CPF = "78912345678", Email = "ana.costa@hospital.com", Telefone = "11934567890", RegistroConselho = "CRM09876", TipoRegistro = "CRM", EspecialidadeId = especialidades[3].Id, DataAdmissao = DateTime.Now.AddYears(-2), CargaHorariaSemanal = 32, Turno = "Manhã", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Enf. Lucas Almeida", CPF = "32165498701", Email = "lucas.almeida@hospital.com", Telefone = "11945678901", RegistroConselho = "COREN12345", TipoRegistro = "COREN", EspecialidadeId = especialidades[4].Id, DataAdmissao = DateTime.Now.AddYears(-1), CargaHorariaSemanal = 36, Turno = "Tarde", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Fernanda Lima", CPF = "65498732109", Email = "fernanda.lima@hospital.com", Telefone = "11956789012", RegistroConselho = "CRM23456", TipoRegistro = "CRM", EspecialidadeId = especialidades[5].Id, DataAdmissao = DateTime.Now.AddYears(-6), CargaHorariaSemanal = 40, Turno = "Noite", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Rafael Souza", CPF = "14725836901", Email = "rafael.souza@hospital.com", Telefone = "11967890123", RegistroConselho = "CRM34567", TipoRegistro = "CRM", EspecialidadeId = especialidades[6].Id, DataAdmissao = DateTime.Now.AddYears(-3), CargaHorariaSemanal = 36, Turno = "Manhã", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dra. Camila Ribeiro", CPF = "25836914701", Email = "camila.ribeiro@hospital.com", Telefone = "11978901234", RegistroConselho = "CRM45678", TipoRegistro = "CRM", EspecialidadeId = especialidades[7].Id, DataAdmissao = DateTime.Now.AddYears(-2), CargaHorariaSemanal = 32, Turno = "Tarde", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Enf. Mariana Dias", CPF = "36914725801", Email = "mariana.dias@hospital.com", Telefone = "11989012345", RegistroConselho = "COREN23456", TipoRegistro = "COREN", EspecialidadeId = especialidades[4].Id, DataAdmissao = DateTime.Now.AddYears(-1), CargaHorariaSemanal = 36, Turno = "Noite", Ativo = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Dr. Gabriel Mendes", CPF = "74185296301", Email = "gabriel.mendes@hospital.com", Telefone = "11990123456", RegistroConselho = "CRM56789", TipoRegistro = "CRM", EspecialidadeId = especialidades[8].Id, DataAdmissao = DateTime.Now.AddYears(-4), CargaHorariaSemanal = 40, Turno = "Manhã", Ativo = true }
                };
                context.ProfissionaisSaude.AddRange(profissionais);
                context.SaveChanges();
                Console.WriteLine($"Inseridos {profissionais.Length} profissionais.");

                // Inserir Pacientes
                Console.WriteLine("Inserindo Pacientes...");
                var pacientes = new Paciente[]
                {
                new() { Id = Guid.NewGuid(), NomeCompleto = "Ana Souza", CPF = "11122233344", DataNascimento = new DateTime(1980, 5, 15), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "11987654321", Email = "ana.souza@email.com", EnderecoCompleto = "Rua A, 123", NumeroCartaoSUS = "123456789012345", EstadoCivil = "Casada", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Carlos Mendes", CPF = "22233344455", DataNascimento = new DateTime(1990, 8, 22), Sexo = "Masculino", TipoSanguineo = "O+", Telefone = "11912345678", Email = "carlos.mendes@email.com", EnderecoCompleto = "Rua B, 456", NumeroCartaoSUS = "987654321098765", EstadoCivil = "Solteiro", PossuiPlanoSaude = false },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Beatriz Lima", CPF = "33344455566", DataNascimento = new DateTime(1975, 3, 10), Sexo = "Feminino", TipoSanguineo = "B+", Telefone = "11923456789", Email = "beatriz.lima@email.com", EnderecoCompleto = "Rua C, 789", NumeroCartaoSUS = "456789123456789", EstadoCivil = "Divorciada", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Diego Ferreira", CPF = "44455566677", DataNascimento = new DateTime(1985, 11, 25), Sexo = "Masculino", TipoSanguineo = "AB-", Telefone = "11934567890", Email = "diego.ferreira@email.com", EnderecoCompleto = "Rua D, 101", NumeroCartaoSUS = "321654987654321", EstadoCivil = "Casado", PossuiPlanoSaude = false },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Elisa Costa", CPF = "55566677788", DataNascimento = new DateTime(1995, 7, 30), Sexo = "Feminino", TipoSanguineo = "O-", Telefone = "11945678901", Email = "elisa.costa@email.com", EnderecoCompleto = "Rua E, 202", NumeroCartaoSUS = "654987321456789", EstadoCivil = "Solteira", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Felipe Almeida", CPF = "66677788899", DataNascimento = new DateTime(1982, 1, 12), Sexo = "Masculino", TipoSanguineo = "A-", Telefone = "11956789012", Email = "felipe.almeida@email.com", EnderecoCompleto = "Rua F, 303", NumeroCartaoSUS = "789123456789123", EstadoCivil = "Casado", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Gabriela Santos", CPF = "77788899900", DataNascimento = new DateTime(1998, 9, 5), Sexo = "Feminino", TipoSanguineo = "B-", Telefone = "11967890123", Email = "gabriela.santos@email.com", EnderecoCompleto = "Rua G, 404", NumeroCartaoSUS = "147258369147258", EstadoCivil = "Solteira", PossuiPlanoSaude = false },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Henrique Oliveira", CPF = "88899900011", DataNascimento = new DateTime(1970, 4, 20), Sexo = "Masculino", TipoSanguineo = "O+", Telefone = "11978901234", Email = "henrique.oliveira@email.com", EnderecoCompleto = "Rua H, 505", NumeroCartaoSUS = "258369147258369", EstadoCivil = "Viúvo", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "Isabela Ribeiro", CPF = "99900011122", DataNascimento = new DateTime(1988, 6, 15), Sexo = "Feminino", TipoSanguineo = "A+", Telefone = "11989012345", Email = "isabela.ribeiro@email.com", EnderecoCompleto = "Rua I, 606", NumeroCartaoSUS = "369147258369147", EstadoCivil = "Casada", PossuiPlanoSaude = true },
                new() { Id = Guid.NewGuid(), NomeCompleto = "João Pereira", CPF = "00011122233", DataNascimento = new DateTime(1992, 12, 1), Sexo = "Masculino", TipoSanguineo = "AB+", Telefone = "11990123456", Email = "joao.pereira@email.com", EnderecoCompleto = "Rua J, 707", NumeroCartaoSUS = "741852963741852", EstadoCivil = "Solteiro", PossuiPlanoSaude = false }
                };
                context.Pacientes.AddRange(pacientes);
                context.SaveChanges();
                Console.WriteLine($"Inseridos {pacientes.Length} pacientes.");

                // Inserir Prontuarios
                Console.WriteLine("Inserindo Prontuarios...");
                var prontuarios = new Prontuario[]
                {
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR001", DataAbertura = DateTime.Now.AddMonths(-6), ObservacoesGerais = "Paciente com histórico de hipertensão", PacienteId = pacientes[0].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR002", DataAbertura = DateTime.Now.AddMonths(-3), ObservacoesGerais = "Paciente com alergia a penicilina", PacienteId = pacientes[1].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR003", DataAbertura = DateTime.Now.AddMonths(-5), ObservacoesGerais = "Paciente com diabetes tipo 2", PacienteId = pacientes[2].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR004", DataAbertura = DateTime.Now.AddMonths(-4), ObservacoesGerais = "Paciente com histórico de fraturas", PacienteId = pacientes[3].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR005", DataAbertura = DateTime.Now.AddMonths(-2), ObservacoesGerais = "Paciente com asma", PacienteId = pacientes[4].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR006", DataAbertura = DateTime.Now.AddMonths(-1), ObservacoesGerais = "Paciente com ansiedade", PacienteId = pacientes[5].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR007", DataAbertura = DateTime.Now.AddMonths(-7), ObservacoesGerais = "Paciente com dermatite", PacienteId = pacientes[6].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR008", DataAbertura = DateTime.Now.AddMonths(-8), ObservacoesGerais = "Paciente com glaucoma", PacienteId = pacientes[7].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR009", DataAbertura = DateTime.Now.AddMonths(-3), ObservacoesGerais = "Paciente com glaucoma", PacienteId = pacientes[8].Id },
                new() { Id = Guid.NewGuid(), NumeroProntuario = "PR010", DataAbertura = DateTime.Now.AddMonths(-2), ObservacoesGerais = "Paciente com histórico de enxaqueca", PacienteId = pacientes[9].Id }
                };
                context.Prontuarios.AddRange(prontuarios);
                context.SaveChanges();
                Console.WriteLine($"Inseridos {prontuarios.Length} prontuários.");

                // Inserir Atendimentos
                Console.WriteLine("Inserindo Atendimentos...");
                var atendimentos = new Atendimento[]
                {
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-10), Tipo = "Consulta", Status = "Realizado", Local = "Sala 01", PacienteId = pacientes[0].Id, ProfissionalId = profissionais[0].Id, ProntuarioId = prontuarios[0].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-5), Tipo = "Emergência", Status = "Realizado", Local = "Pronto Socorro", PacienteId = pacientes[1].Id, ProfissionalId = profissionais[1].Id, ProntuarioId = prontuarios[1].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-8), Tipo = "Consulta", Status = "Realizado", Local = "Sala 02", PacienteId = pacientes[2].Id, ProfissionalId = profissionais[2].Id, ProntuarioId = prontuarios[2].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-7), Tipo = "Internação", Status = "Em andamento", Local = "UTI 01", PacienteId = pacientes[3].Id, ProfissionalId = profissionais[3].Id, ProntuarioId = prontuarios[3].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-6), Tipo = "Consulta", Status = "Realizado", Local = "Sala 03", PacienteId = pacientes[4].Id, ProfissionalId = profissionais[4].Id, ProntuarioId = prontuarios[4].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-4), Tipo = "Consulta", Status = "Cancelado", Local = "Sala 04", PacienteId = pacientes[5].Id, ProfissionalId = profissionais[5].Id, ProntuarioId = prontuarios[5].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-3), Tipo = "Emergência", Status = "Realizado", Local = "Pronto Socorro", PacienteId = pacientes[6].Id, ProfissionalId = profissionais[6].Id, ProntuarioId = prontuarios[6].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-2), Tipo = "Consulta", Status = "Realizado", Local = "Sala 05", PacienteId = pacientes[7].Id, ProfissionalId = profissionais[7].Id, ProntuarioId = prontuarios[7].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now.AddDays(-1), Tipo = "Consulta", Status = "Em andamento", Local = "Sala 06", PacienteId = pacientes[8].Id, ProfissionalId = profissionais[8].Id, ProntuarioId = prontuarios[8].Id },
                new() { Id = Guid.NewGuid(), DataHora = DateTime.Now, Tipo = "Consulta", Status = "Realizado", Local = "Sala 07", PacienteId = pacientes[9].Id, ProfissionalId = profissionais[9].Id, ProntuarioId = prontuarios[9].Id }
                };
                context.Atendimentos.AddRange(atendimentos);
                context.SaveChanges();
                Console.WriteLine($"Inseridos {atendimentos.Length} atendimentos.");

                // Inserir Prescricoes
                Console.WriteLine("Inserindo Prescricoes...");
                var prescricoes = new Prescricao[]
                {
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[0].Id, ProfissionalId = profissionais[0].Id, Medicamento = "Losartana", Dosagem = "50mg", Frequencia = "1x ao dia", ViaAdministracao = "Oral", DataInicio = DateTime.Now.AddDays(-10), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[1].Id, ProfissionalId = profissionais[1].Id, Medicamento = "Paracetamol", Dosagem = "500mg", Frequencia = "8 em 8h", ViaAdministracao = "Oral", DataInicio = DateTime.Now.AddDays(-5), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[2].Id, ProfissionalId = profissionais[2].Id, Medicamento = "Insulina", Dosagem = "10UI", Frequencia = "2x ao dia", ViaAdministracao = "Subcutânea", DataInicio = DateTime.Now.AddDays(-8), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[3].Id, ProfissionalId = profissionais[3].Id, Medicamento = "Morfina", Dosagem = "5mg", Frequencia = "6 em 6h", ViaAdministracao = "Intravenosa", DataInicio = DateTime.Now.AddDays(-7), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[4].Id, ProfissionalId = profissionais[4].Id, Medicamento = "Salbutamol", Dosagem = "100mcg", Frequencia = "4 em 4h", ViaAdministracao = "Inalatória", DataInicio = DateTime.Now.AddDays(-6), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[5].Id, ProfissionalId = profissionais[5].Id, Medicamento = "Sertralina", Dosagem = "50mg", Frequencia = "1x ao dia", ViaAdministracao = "Oral", DataInicio = DateTime.Now.AddDays(-4), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[6].Id, ProfissionalId = profissionais[6].Id, Medicamento = "Hidrocortisona", Dosagem = "10mg", Frequencia = "2x ao dia", ViaAdministracao = "Tópica", DataInicio = DateTime.Now.AddDays(-3), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[7].Id, ProfissionalId = profissionais[7].Id, Medicamento = "Latanoprosta", Dosagem = "0.005%", Frequencia = "1x ao dia", ViaAdministracao = "Oftálmica", DataInicio = DateTime.Now.AddDays(-2), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[8].Id, ProfissionalId = profissionais[8].Id, Medicamento = "Risperidona", Dosagem = "2mg", Frequencia = "1x ao dia", ViaAdministracao = "Oral", DataInicio = DateTime.Now.AddDays(-1), StatusPrescricao = "Ativa" },
                new() { Id = Guid.NewGuid(), AtendimentoId = atendimentos[9].Id, ProfissionalId = profissionais[9].Id, Medicamento = "Levotiroxina", Dosagem = "100mcg", Frequencia = "1x ao dia", ViaAdministracao = "Oral", DataInicio = DateTime.Now, StatusPrescricao = "Ativa" }
                };
                context.Prescricoes.AddRange(prescricoes);
                context.SaveChanges();
                Console.WriteLine($"Inseridas {prescricoes.Length} prescrições.");

                // Inserir Exames
                Console.WriteLine("Inserindo Exames...");
                var exames = new Exame[]
                {
                new() { Id = Guid.NewGuid(), Tipo = "Hemograma", DataSolicitacao = DateTime.Now.AddDays(-10), DataRealizacao = DateTime.Now.AddDays(-9), Resultado = "Normal", AtendimentoId = atendimentos[0].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Raio-X", DataSolicitacao = DateTime.Now.AddDays(-5), DataRealizacao = DateTime.Now.AddDays(-4), Resultado = "Sem alterações", AtendimentoId = atendimentos[1].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Glicemia", DataSolicitacao = DateTime.Now.AddDays(-8), DataRealizacao = DateTime.Now.AddDays(-7), Resultado = "Elevada", AtendimentoId = atendimentos[2].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Tomografia", DataSolicitacao = DateTime.Now.AddDays(-7), DataRealizacao = DateTime.Now.AddDays(-6), Resultado = "Fratura confirmada", AtendimentoId = atendimentos[3].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Espirometria", DataSolicitacao = DateTime.Now.AddDays(-6), DataRealizacao = DateTime.Now.AddDays(-5), Resultado = "Capacidade reduzida", AtendimentoId = atendimentos[4].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Eletroencefalograma", DataSolicitacao = DateTime.Now.AddDays(-4), DataRealizacao = DateTime.Now.AddDays(-3), Resultado = "Normal", AtendimentoId = atendimentos[5].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Exame de pele", DataSolicitacao = DateTime.Now.AddDays(-3), DataRealizacao = DateTime.Now.AddDays(-2), Resultado = "Dermatite atópica", AtendimentoId = atendimentos[6].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Exame oftalmológico", DataSolicitacao = DateTime.Now.AddDays(-2), DataRealizacao = DateTime.Now.AddDays(-1), Resultado = "Glaucoma detectado", AtendimentoId = atendimentos[7].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Ressonância magnética", DataSolicitacao = DateTime.Now.AddDays(-1), DataRealizacao = DateTime.Now, Resultado = "Normal", AtendimentoId = atendimentos[8].Id },
                new() { Id = Guid.NewGuid(), Tipo = "Teste de tireoide", DataSolicitacao = DateTime.Now, DataRealizacao = DateTime.Now, Resultado = "Hipotireoidismo", AtendimentoId = atendimentos[9].Id }
                };
                context.Exames.AddRange(exames);
                context.SaveChanges();
                Console.WriteLine($"Inseridos {exames.Length} exames.");

                // Inserir Internacoes
                Console.WriteLine("Inserindo Internacoes...");
                var internacoes = new Internacao[]
                {
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[0].Id, AtendimentoId = atendimentos[0].Id, DataEntrada = DateTime.Now.AddDays(-7), PrevisaoAlta = DateTime.Now.AddDays(3), MotivoInternacao = "Crise hipertensiva", Leito = "L01", Quarto = "Q101", Setor = "Clínica Geral", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[1].Id, AtendimentoId = atendimentos[1].Id, DataEntrada = DateTime.Now.AddDays(-3), PrevisaoAlta = DateTime.Now.AddDays(2), MotivoInternacao = "Fratura", Leito = "L02", Quarto = "Q102", Setor = "Ortopedia", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[2].Id, AtendimentoId = atendimentos[2].Id, DataEntrada = DateTime.Now.AddDays(-5), PrevisaoAlta = DateTime.Now.AddDays(1), MotivoInternacao = "Descompensação diabética", Leito = "L03", Quarto = "Q103", Setor = "Clínica Geral", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[3].Id, AtendimentoId = atendimentos[3].Id, DataEntrada = DateTime.Now.AddDays(-6), PrevisaoAlta = DateTime.Now.AddDays(4), MotivoInternacao = "Trauma ósseo", Leito = "L04", Quarto = "Q104", Setor = "Ortopedia", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[4].Id, AtendimentoId = atendimentos[4].Id, DataEntrada = DateTime.Now.AddDays(-4), PrevisaoAlta = DateTime.Now.AddDays(2), MotivoInternacao = "Crise asmática", Leito = "L05", Quarto = "Q105", Setor = "Clínica Geral", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[5].Id, AtendimentoId = atendimentos[5].Id, DataEntrada = DateTime.Now.AddDays(-3), PrevisaoAlta = DateTime.Now.AddDays(1), MotivoInternacao = "Crise de ansiedade", Leito = "L06", Quarto = "Q106", Setor = "Psiquiatria", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[6].Id, AtendimentoId = atendimentos[6].Id, DataEntrada = DateTime.Now.AddDays(-2), PrevisaoAlta = DateTime.Now.AddDays(1), MotivoInternacao = "Infecção cutânea", Leito = "L07", Quarto = "Q107", Setor = "Dermatologia", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[7].Id, AtendimentoId = atendimentos[7].Id, DataEntrada = DateTime.Now.AddDays(-1), PrevisaoAlta = DateTime.Now.AddDays(2), MotivoInternacao = "Crise ocular", Leito = "L08", Quarto = "Q108", Setor = "Oftalmologia", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[8].Id, AtendimentoId = atendimentos[8].Id, DataEntrada = DateTime.Now.AddDays(-2), PrevisaoAlta = DateTime.Now.AddDays(3), MotivoInternacao = "Surto psicótico", Leito = "L09", Quarto = "Q109", Setor = "Psiquiatria", StatusInternacao = "Ativa" },
                new() { Id = Guid.NewGuid(), PacienteId = pacientes[9].Id, AtendimentoId = atendimentos[9].Id, DataEntrada = DateTime.Now.AddDays(-1), PrevisaoAlta = DateTime.Now.AddDays(2), MotivoInternacao = "Descompensação tireoidiana", Leito = "L10", Quarto = "Q110", Setor = "Endocrinologia", StatusInternacao = "Ativa" }
                };
                context.Internacoes.AddRange(internacoes);
                context.SaveChanges();
                Console.WriteLine($"Inseridas {internacoes.Length} internações.");

                // Inserir AltasHospitalares
                Console.WriteLine("Inserindo AltasHospitalares...");
                var altas = new AltaHospitalar[]
                {
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[0].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Repouso e medicação conforme prescrição" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[1].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Fisioterapia e acompanhamento ortopédico" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[2].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Controle glicêmico rigoroso" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[3].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Imobilização e consultas ortopédicas" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[4].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Uso de inaladores e acompanhamento pulmonar" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[5].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Acompanhamento psiquiátrico" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[6].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Uso de cremes e acompanhamento dermatológico" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[7].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Uso de colírios e consultas oftalmológicas" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[8].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Medicação e acompanhamento psiquiátrico" },
                new() { Id = Guid.NewGuid(), InternacaoId = internacoes[9].Id, DataAlta = DateTime.Now, CondicaoPaciente = "Estável", InstrucoesPosAlta = "Medicação tireoidiana e exames regulares" }
                };
                context.AltasHospitalares.AddRange(altas);
                context.SaveChanges();
                Console.WriteLine($"Inseridas {altas.Length} altas hospitalares.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante o seed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalhes: {ex.InnerException.Message}");
                }
                throw;
            }

            Console.WriteLine("SeedData concluído com sucesso!");
        }
    }
}
