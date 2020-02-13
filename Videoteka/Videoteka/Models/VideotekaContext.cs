using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Videoteka.Models
{
    public partial class VideotekaContext : DbContext
    {
        public VideotekaContext()
        {
        }

        public VideotekaContext(DbContextOptions<VideotekaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Posudbe> Posudbe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LG5IA29\\SQLEXPRESS;Database=Videoteka;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lozinka)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Posudbe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DatumPosudbe).HasColumnType("date");

                entity.Property(e => e.FilmId).HasColumnName("FilmID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Posudbe)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posudbe_Film");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Posudbe)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posudbe_Korisnik");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
