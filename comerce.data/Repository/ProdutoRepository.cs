using comerce.data.context;
using comerce.data.Contract;
using comerce.data.Utilitarios;
using comerce.domain.model;
using comerce.domain.ModelView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;

namespace comerce.data.Repository
{
    public class ProdutoRepository(ComerceContext _context) : IProdutoRepository
    {
        public Produto Get(int id)
        {
            return _context.Produtos.AsNoTracking().Where(t => t.Id == id).FirstOrDefault();
        }

        public List<Produto> GetByName(string name)
        {
            return _context.Produtos.AsNoTracking().Autocomplete(t => t.Nome.Contains(name), o => o.Nome).Take(100).ToList();
        }

        public List<Produto> GetBySort(List<EstockModelView> sorts)
        {
            return _context.Produtos.AsNoTracking().ToSort(sorts);
        }

        public void Add(Produto entity)
        {
            _context.Produtos.Add(entity);
        }

        public void Update(Produto entity)
        {
            _context.Produtos.Update(entity);
        }

        public void Delete(Produto entity)
        {
            _context.Produtos.Remove(entity);
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }
    }
}
