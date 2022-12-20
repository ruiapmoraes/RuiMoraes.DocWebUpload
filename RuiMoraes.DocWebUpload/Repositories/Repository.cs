using RuiMoraes.DocWebUpload.Models;
using RuiMoraes.DocWebUpload.Repositories.Interfaces;

namespace RuiMoraes.DocWebUpload.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntidade
    {
        protected readonly DocWebUploadDbContext _context;

        public Repository(DocWebUploadDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> BuscaTodos()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity BuscaPorId(int? id)
        {
            var result = _context.Set<TEntity>().Find(id);
            return result;
        }
        public TEntity Adicionar(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }


        public TEntity Editar(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Remover(int? id)
        {
            var entity = BuscaPorId(id);
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
