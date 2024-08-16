
using SQLite;

namespace LangostaAhumada6657336
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Pedido>();
        }

        public async Task<List<Pedido>> GetResutado()
        {
            return await _connection.Table<Pedido>().ToListAsync();
        }

        public async Task<Pedido> GetById(int id)
        {
            return await _connection.Table<Pedido>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Pedido pedido)
        {
            await _connection.InsertAsync(pedido);
        }
        public async Task Update(Pedido pedido)
        {
            await _connection.UpdateAsync(pedido);
        }
        public async Task Delete(Pedido pedido)
        {
            await _connection.DeleteAsync(pedido);
        }
    }
}
