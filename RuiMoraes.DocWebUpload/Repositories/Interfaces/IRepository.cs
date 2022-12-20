using RuiMoraes.DocWebUpload.Models;

namespace RuiMoraes.DocWebUpload.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntidade
    {
        IEnumerable<TEntity> BuscaTodos();
        TEntity BuscaPorId(int? id);
        TEntity Adicionar(TEntity entity);
        TEntity Editar(TEntity entity);
        bool Remover(int? id);
    }
}
