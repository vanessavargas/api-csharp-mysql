using Microsoft.AspNetCore.Mvc;
using myProject.Model;
using myProject.Repository;

namespace myProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        //injetar dependencia do repositorio
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _repository.GetProdutos();
            return produtos.Any() ? Ok(produtos) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _repository.GetProdutoById(id);
            return produto != null
            ? Ok(produto) : NotFound("Produto não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            _repository.AddProduto(produto);
            return await _repository.SaveChangesAsync()
            ? Ok("Produto adicionado") : BadRequest("Algo deu errado.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Produto produto)
        {
            var produtoExiste = await _repository.GetProdutoById(id);
            if (produtoExiste == null) return NotFound("Produto não encontrado");

            produtoExiste.NomeProduto = produto.NomeProduto ?? produtoExiste.NomeProduto;

            _repository.AtualizarProduto(produtoExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Produto atualizado.") : BadRequest("Algo deu errado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produtoExiste = await _repository.GetProdutoById(id);
            if (produtoExiste == null)
                return NotFound("Produto não encontrado");

            _repository.DeletarProduto(produtoExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Produto deletado.") : BadRequest("Algo deu errado.");
        }

    }
}