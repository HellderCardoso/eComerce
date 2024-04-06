using comerce.domain.model;
using comerce.domain.ModelView;
using Microsoft.Office.Interop.Excel;

namespace comerce.data.Contract
{
    public interface IProdutoRepository
    {
        Produto Get(int id);
        List<Produto> GetByName(string name);
        List<Produto> GetBySort(List<EstockModelView> estock);
        void Add(Produto entity);
        void Update(Produto entity);
        void Delete(Produto entity);
        void Savechanges();
    }
}
