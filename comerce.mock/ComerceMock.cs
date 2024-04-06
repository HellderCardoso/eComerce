using comerce.domain.model;
using System.Drawing;

namespace commerce.mock
{
    public class ComerceMock
    {
        private readonly ISequencial _sequencial;

        public ComerceMock()
        {
            _sequencial = new Sequencial();
        }

        public Produto NovoProduto()
        {
            var id = _sequencial.Next("Id");

            return new Produto(id, "Nome Produto:", 20.3, 30);
        }

        public Produto NovoProdutoInvalido()
        {
            var id = _sequencial.Next("Id");

            return new Produto(id, "Nome Produto:", -2, -4);
        }
    }
}