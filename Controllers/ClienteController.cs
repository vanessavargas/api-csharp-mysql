using Microsoft.AspNetCore.Mvc;
using projetoCsharp.Model;
using projetoCsharp.Repository;

namespace projetoCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        //injetar dependencia do repositorio
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _repository.GetClientes();
            return clientes.Any() ? Ok(clientes) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _repository.GetClienteById(id);
            return cliente != null
            ? Ok(cliente) : NotFound("Cliente não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            _repository.AddCliente(cliente);
            return await _repository.SaveChangesAsync()
            ? Ok("Cliente adicionado") : BadRequest("Algo deu errado.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Cliente cliente)
        {
            var clienteExiste = await _repository.GetClienteById(id);
            if (clienteExiste == null) return NotFound("Cliente não encontrado");

            clienteExiste.Nome = cliente.Nome ?? clienteExiste.Nome;
            clienteExiste.DataNascimento = cliente.DataNascimento != new DateTime()
            ? cliente.DataNascimento : clienteExiste.DataNascimento;
            clienteExiste.Endereco = cliente.Endereco ?? clienteExiste.Endereco;
            clienteExiste.Email = cliente.Email ?? clienteExiste.Email;

            _repository.AtualizarCliente(clienteExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Cliente atualizado.") : BadRequest("Algo deu errado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteExiste = await _repository.GetClienteById(id);
            if (clienteExiste == null)
                return NotFound("Cliente não encontrado");

            _repository.DeletarCliente(clienteExiste);

            return await _repository.SaveChangesAsync()
            ? Ok("Cliente deletado.") : BadRequest("Algo deu errado.");
        }

    }
}