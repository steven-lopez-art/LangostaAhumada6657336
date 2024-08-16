namespace LangostaAhumada6657336
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            _dbService = dbService;
            InitializeComponent();
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResutado());
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var r = (Pedido)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = r.Id;
                    nombreCliente.Text = r.NombreCliente;
                    numeroPlatos.Text = r.NumeroPlatos;
                    resultado.Text = r.Pago;
                    break;

                case "Delete":
                    await _dbService.Delete(r);
                    listview.ItemsSource = await _dbService.GetResutado();
                    break;
            }
        }
    

        private async void calcularPago_Clicked(object sender, EventArgs e)
        {
            int tot;

            if (int.TryParse(numeroPlatos.Text, out int numero))
            {
                if (numero > 300)
                {
                    tot = numero * 75;
                }
                else if (numero > 200)
                {
                    tot = numero * 85;
                }
                else
                {
                    tot = numero * 95;
                }
                resultado.Text = ("Total a pagar : $" + tot);


                if (_editResultadoId == 0)
                {
                    await _dbService.Create(new Pedido
                    {
                        NombreCliente = nombreCliente.Text,
                        NumeroPlatos = numeroPlatos.Text,
                        Pago = resultado.Text
                    });
                }
                else
                {
                    await _dbService.Update(new Pedido
                    {
                        Id = _editResultadoId,
                        NombreCliente = nombreCliente.Text,
                        NumeroPlatos = numeroPlatos.Text,
                        Pago = resultado.Text
                    });
                    _editResultadoId = 0;
                }
                nombreCliente.Text = string.Empty;
                numeroPlatos.Text = string.Empty;
                resultado.Text = string.Empty;

                listview.ItemsSource = await _dbService.GetResutado();
            }

            else
            {
                resultado.Text = "Ingrese los numeros correctamente";

            }
        }
    }
}
