namespace Aseguradora.Aplicacion;

public class Poliza
{
    public int ID { get; set; } = -1;
    public int VehiculoAsegurado { get; set; }
    public double ValorAsegurado { get; set; }
    public string Franquicia { get; set; }
    public string Cobertura { get; set; }
    public DateTime VigenteDesde { get; set; }
    public DateTime VigenteHasta { get; set; }
    public Poliza(int vehiculoAsegurado, double valorAsegurado, string franquicia, string cobertura, DateTime vigenteDesde, DateTime vigenteHasta)
    {
        VehiculoAsegurado = vehiculoAsegurado;
        ValorAsegurado = valorAsegurado;
        Franquicia = franquicia;
        Cobertura = cobertura;
        VigenteDesde = vigenteDesde;
        VigenteHasta = vigenteHasta;
    }

    public override string ToString()
    {
        return  $"{ID}: VehiculoAsegurado:{VehiculoAsegurado} ValorAsegurado:{ValorAsegurado}"+
                $" Franquicia:{Franquicia} Cobertura:{Cobertura} VigenteDesde:{VigenteDesde:d}"+
                $" VigenteHasta:{VigenteHasta:d}";
    }
}