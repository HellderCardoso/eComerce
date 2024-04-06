using comerce.domain.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace comerce.aplication.contract
{
    public interface IProdutoService
    {
        Task<ActionResult> Get(int id);
        Task<ActionResult> GetByName(string name);
        Task<ActionResult> GetBySort(List<EstockModelView> estock);
        Task<ActionResult> Add(ProdutoModelView produto);
        Task<ActionResult> Delete(int id);
        Task<ActionResult> Update(int id, ProdutoModelView produto);
    }
}
