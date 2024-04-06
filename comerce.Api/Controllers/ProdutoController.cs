using comerce.aplication.contract;
using comerce.domain.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;

namespace comerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController(IProdutoService _produtoService) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _produtoService.Get(id);
        }

        [HttpPost("GetBySort")]
        public async Task<IActionResult> GetBySort([FromBody] List<EstockModelView> estock)
        {
            return await _produtoService.GetBySort(estock);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return await _produtoService.GetByName(name);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoModelView produto)
        {
            return await _produtoService.Add(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProdutoModelView produto, int id)
        {
            return await _produtoService.Update(id, produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _produtoService.Delete(id);
        }
    }
}