using SQLite;

namespace LangostaAhumada6657336
{
    [Table("pedido")]
    public class Pedido
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombrecliente")]
        public string? NombreCliente { get; set; }

        [Column("numeroplatos")]
        public string? NumeroPlatos { get; set; }

        [Column("pago")]
        public string? Pago { get; set; }
    }
}
