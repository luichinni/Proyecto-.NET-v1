namespace Aseguradora.Aplicacion;

public class Vehiculo
{
    public int ID { get; set; } = -1;
    public string Dominio { get; set; }
    public string Marca { get; set; }
    public int AnoFabricacion { get; set; }
    public int Titular { get; set; }

    public Vehiculo(string dominio, string marca, int anoFabricacion, int titular)
    {
        Dominio = dominio;
        Marca = marca;
        AnoFabricacion = anoFabricacion;
        Titular = titular;
    }

    public override string ToString()
    {
        return  $"{ID}: Dominio:{Dominio} Marca:{Marca} AnoFabricacion:{AnoFabricacion} Titular:{Titular}";
    }
}