using comerce.aplication.contract;
using comerce.data.Contract;
using comerce.domain.model;
using comerce.domain.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comerce.aplication.services
{
    public class ProdutoService(IProdutoRepository _produtoRepository) : IProdutoService
    {
        public async Task<ActionResult> Get(int id)
        {
            var entity = _produtoRepository.Get(id);
            if (entity == null) return new NotFoundResult();

            return new OkObjectResult(entity);
        }

        public async Task<ActionResult> GetByName(string name)
        {
            var entity = _produtoRepository.GetByName(name);
            if (entity == null) return new NotFoundResult();

            return new OkObjectResult(entity);
        }

        public async Task<ActionResult> GetBySort(List<EstockModelView> estocks)
        {
            var entity = _produtoRepository.GetBySort(estocks);
            if (entity == null) return new NotFoundResult();

            return new OkObjectResult(entity);
        }

        public async Task<ActionResult> Add(ProdutoModelView produto)
        {
            if (produto.Valor < 0 || produto.Estoque < 0 || produto.Nome.Length < 3) return new BadRequestResult();

            var entity = new Produto(produto.Nome, produto.Valor, produto.Estoque);

            _produtoRepository.Add(entity);
            _produtoRepository.Savechanges();

            return new NoContentResult();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = _produtoRepository.Get(id);
            if (entity == null) return new NotFoundResult();

            _produtoRepository.Delete(entity);
            _produtoRepository.Savechanges();

            return new NoContentResult();
        }

        public async Task<ActionResult> Update(int id, ProdutoModelView produto)
        {
            if (produto.Valor < 0 || produto.Estoque < 0 || produto.Nome.Length < 3) return new BadRequestResult();

            var entity = _produtoRepository.Get(id);
            if (entity == null) return new NotFoundResult();

            entity.SetNome(produto.Nome);
            entity.SetValor(produto.Valor);
            entity.SetEstoque(produto.Estoque);

            _produtoRepository.Update(entity);
            _produtoRepository.Savechanges();

            return new OkResult();
        }
    }
}

