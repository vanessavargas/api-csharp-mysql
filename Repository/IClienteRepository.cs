using myProject.Model;

namespace myProject.Repository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int id);
        void AddCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void DeletarCliente(Cliente cliente);
        Task<bool> SaveChangesAsync();
        
    }
}