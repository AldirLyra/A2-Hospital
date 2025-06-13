using A2_Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace A2_Hospital.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<ProfissionalSaude> ProfissionaisSaude { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Prontuarios)
                .WithOne(pr => pr.Paciente)
                .HasForeignKey(pr => pr.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prontuario>()
                .HasMany(pr => pr.Atendimentos)
                .WithOne(a => a.Prontuario)
                .HasForeignKey(a => a.ProntuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProfissionalSaude>()
                .HasMany(p => p.Atendimentos)
                .WithOne(a => a.Profissional)
                .HasForeignKey(a => a.ProfissionalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Atendimentos)
                .WithOne(a => a.Paciente)
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Internacoes)
                .WithOne(i => i.Paciente)
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Internacao)
                .WithOne(i => i.Atendimento)
                .HasForeignKey<Internacao>(i => i.AtendimentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Atendimento>()
                .HasMany(a => a.Prescricoes)
                .WithOne(p => p.Atendimento)
                .HasForeignKey(p => p.AtendimentoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfissionalSaude>()
                .HasMany(p => p.Prescricoes)
                .WithOne(pr => pr.Profissional)
                .HasForeignKey(pr => pr.ProfissionalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Atendimento>()
                .HasMany(a => a.Exames)
                .WithOne(e => e.Atendimento)
                .HasForeignKey(e => e.AtendimentoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.AltaHospitalar)
                .WithOne(ah => ah.Internacao)
                .HasForeignKey<AltaHospitalar>(ah => ah.InternacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfissionalSaude>()
                .HasOne(p => p.Especialidade)
                .WithMany(e => e.Profissionais)
                .HasForeignKey(p => p.EspecialidadeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
