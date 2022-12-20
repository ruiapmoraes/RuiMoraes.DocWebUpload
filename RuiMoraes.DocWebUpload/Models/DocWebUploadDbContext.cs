using Microsoft.EntityFrameworkCore;

namespace RuiMoraes.DocWebUpload.Models
{
    public class DocWebUploadDbContext : DbContext
    {
        public DocWebUploadDbContext(DbContextOptions<DocWebUploadDbContext> options)
            : base(options) { }
        public DbSet<Documento> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            //builder.ApplyConfigurationsFromAssembly(typeof(DocWebUploadDbContext).Assembly);
        }

    }
}
