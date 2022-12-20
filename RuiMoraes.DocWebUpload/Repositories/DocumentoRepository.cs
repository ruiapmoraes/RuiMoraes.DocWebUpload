using RuiMoraes.DocWebUpload.Models;
using RuiMoraes.DocWebUpload.Repositories.Interfaces;
using System.Reflection.Metadata;

namespace RuiMoraes.DocWebUpload.Repositories
{
    public class DocumentoRepository : Repository<Documento>, IDocumentoRepository
    {
        public DocumentoRepository(DocWebUploadDbContext context) : base(context) { }
        
    }
}
