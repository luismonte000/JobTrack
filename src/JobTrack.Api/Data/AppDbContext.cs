using JobTrack.Api.Entities;
using JobTrack.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobTrack.Api.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserSkill> UserSkills => Set<UserSkill>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    public DbSet<ApplicationStatusHistory> ApplicationStatusHistories =>
        Set<ApplicationStatusHistory>();

    public DbSet<JobAnalysis> JobAnalyses => Set<JobAnalysis>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .Property(user => user.FullName)
            .HasMaxLength(150)
            .IsRequired();

        // UserSkill -> ApplicationUser (1:N)
        builder.Entity<UserSkill>()
            .HasOne(skill => skill.User)
            .WithMany()
            .HasForeignKey(skill => skill.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Company -> ApplicationUser (1:N)
        builder.Entity<Company>()
            .HasOne(company => company.User)
            .WithMany()
            .HasForeignKey(company => company.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Chave alternativa para permitir a relação composta com JobApplication
        builder.Entity<Company>()
            .HasAlternateKey(company => new { company.Id, company.UserId });

        // JobApplication -> ApplicationUser (1:N)
        builder.Entity<JobApplication>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(application => application.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobApplication -> Company (N:1, opcional)
        // Garante que a empresa pertence ao mesmo utilizador da candidatura.
        builder.Entity<JobApplication>()
            .HasOne<Company>()
            .WithMany()
            .HasForeignKey(application => new
    {
        application.CompanyId,
        application.UserId
    })
    .HasPrincipalKey(company => new
    {
        company.Id,
        company.UserId
    })
    .OnDelete(DeleteBehavior.Restrict);

        // ApplicationStatusHistory -> JobApplication (1:N)
        builder.Entity<ApplicationStatusHistory>()
            .HasOne<JobApplication>()
            .WithMany()
            .HasForeignKey(history => history.JobApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobAnalysis -> JobApplication (1:N)
        builder.Entity<JobAnalysis>()
            .HasOne<JobApplication>()
            .WithMany()
            .HasForeignKey(analysis => analysis.JobApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        // UserSkill
        builder.Entity<UserSkill>()
            .Property(skill => skill.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Entity<UserSkill>()
            .Property(skill => skill.Level)
            .HasMaxLength(30)
            .IsRequired();

        builder.Entity<UserSkill>()
            .Property(skill => skill.YearsOfExperience)
            .HasPrecision(4, 1);

        // Company
        builder.Entity<Company>()
            .Property(company => company.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Entity<Company>()
            .Property(company => company.Website)
            .HasMaxLength(500);

        builder.Entity<Company>()
            .Property(company => company.Location)
            .HasMaxLength(200);

        // JobApplication
        builder.Entity<JobApplication>()
            .Property(application => application.JobTitle)
            .HasMaxLength(200)
            .IsRequired();

        builder.Entity<JobApplication>()
            .Property(application => application.Location)
            .HasMaxLength(200);

        builder.Entity<JobApplication>()
            .Property(application => application.WorkMode)
            .HasMaxLength(20);

        builder.Entity<JobApplication>()
            .Property(application => application.Salary)
            .HasPrecision(12, 2);

        builder.Entity<JobApplication>()
            .Property(application => application.SalaryCurrency)
            .HasMaxLength(3);

        builder.Entity<JobApplication>()
            .Property(application => application.JobUrl)
            .HasMaxLength(2048);

        builder.Entity<JobApplication>()
            .Property(application => application.Status)
            .HasMaxLength(30)
            .IsRequired();

        // ApplicationStatusHistory
        builder.Entity<ApplicationStatusHistory>()
            .Property(history => history.PreviousStatus)
            .HasMaxLength(30);

        builder.Entity<ApplicationStatusHistory>()
            .Property(history => history.NewStatus)
            .HasMaxLength(30)
            .IsRequired();

        // JobAnalysis
        builder.Entity<JobAnalysis>()
            .Property(analysis => analysis.MatchPercentage)
            .HasPrecision(5, 2);

        builder.Entity<JobAnalysis>()
            .ToTable(table => table.HasCheckConstraint(
                "CK_JobAnalyses_MatchPercentage",
                "\"MatchPercentage\" >= 0 AND \"MatchPercentage\" <= 100"));    

        builder.Entity<JobAnalysis>()
            .Property(analysis => analysis.MatchedSkills)
            .HasColumnType("jsonb");

        builder.Entity<JobAnalysis>()
            .Property(analysis => analysis.MissingSkills)
            .HasColumnType("jsonb");
    }
}