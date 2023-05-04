using Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
// PRUEBA AGREGAR Y MODIFICAR TITULAR
/*
RepoTitularTXT repoTitu = new RepoTitularTXT();
AgregarTitularUseCase agregarTitular = new AgregarTitularUseCase(repoTitu);
ModificarTitularUseCase modificarTitular = new ModificarTitularUseCase(repoTitu);
agregarTitular.Ejecutar(new Titular("44130300","Macias","Luciano"));
agregarTitular.Ejecutar(new Titular("44130303","Loyola","Nicole"));
agregarTitular.Ejecutar(new Titular("44130304","Suster","Laura"));

Console.ReadKey();

modificarTitular.Ejecutar(new Titular("44130305","Macias","Laureano"));

Console.ReadKey();

agregarTitular.Ejecutar(new Titular("44130305", "Paez", "Maia"));
agregarTitular.Ejecutar(new Titular("44130306", "Suster", "Laura"));

Console.ReadKey();*/
/* PRUEBA AGREGAR Y MODIFICAR POLIZA
RepoPolizaTXT repoPoli = new RepoPolizaTXT();
AgregarPolizaUseCase agregarPoliza = new AgregarPolizaUseCase(repoPoli);
ModificarPolizaUseCase modificarPoliza = new ModificarPolizaUseCase(repoPoli);
agregarPoliza.Ejecutar(new Poliza(1,20000,"1000","Todo Riesgo",new DateTime(2023,4,1),new DateTime(2024,4,1)));
agregarPoliza.Ejecutar(new Poliza(1, 40000, "40000", "Responsabilidad Civil", new DateTime(2023, 6, 1), new DateTime(2024, 6, 1)));

Console.ReadKey();

modificarPoliza.Ejecutar(new Poliza(1, 40000, "0", "Todo Riesgo", new DateTime(2023, 6, 1), new DateTime(2024, 6, 1)){ID=2});

Console.ReadKey();

agregarPoliza.Ejecutar(new Poliza(1,20000,"0","Todo Riesgo",new DateTime(2023,5,23),new DateTime(2024,5,23)));

Console.ReadKey();
*/
/*
RepoVehiculoTXT repoVehi = new RepoVehiculoTXT();
AgregarVehiculoUseCase agregarVehiculo = new AgregarVehiculoUseCase(repoVehi);
ModificarVehiculoUseCase modificarVehiculo = new ModificarVehiculoUseCase(repoVehi);

agregarVehiculo.Ejecutar(new Vehiculo("Prenda","Ford",2022,1));
agregarVehiculo.Ejecutar(new Vehiculo("Prenda", "Mercedes", 2020, 1));

Console.ReadKey();

modificarVehiculo.Ejecutar(new Vehiculo("Robo","Ford",2022,1){ID=1});

Console.ReadKey();

agregarVehiculo.Ejecutar(new Vehiculo("Embargo","Audi",2019,2));

Console.ReadKey();
*/


//PARTE NICKY   ---->

/*

//creo el repositorio para titular
RepoTitularTXT repoTitular = new RepoTitularTXT();

//instancio el caso de uso de titular con mi repositorio
EliminarTitularUseCase eliminarTitular = new EliminarTitularUseCase(repoTitular);
ListarTitularesUseCase listarTitular = new ListarTitularesUseCase(repoTitular);

//listarTitular.Ejecutar();

Console.WriteLine("Eliminando el Titular con ID {0}",2);
eliminarTitular.Ejecutar(3);


List<Titular> listaTitular = listarTitular.Ejecutar();
foreach(Titular elem in listaTitular){
    Console.WriteLine(elem);
}

//----------------------------------------------------------

RepoPolizaTXT repoPoliza = new RepoPolizaTXT();

EliminarPolizaUseCase eliminarPoliza = new EliminarPolizaUseCase(repoPoliza);
ListarPolizasUseCase listarPoliza = new ListarPolizasUseCase(repoPoliza);

Console.WriteLine("Eliminando la Poliza con ID {0}",1);
eliminarPoliza.Ejecutar(2);

List<Poliza> listaPoliza = listarPoliza.Ejecutar();
foreach(Poliza elem in listaPoliza){
    Console.WriteLine(elem);
}



//------------------------------------------------------------

RepoVehiculoTXT repoVehiculo = new RepoVehiculoTXT();

EliminarVehiculoUseCase eliminarVehiculo = new EliminarVehiculoUseCase(repoVehiculo);
ListarVehiculosUseCase listarVehiculo = new ListarVehiculosUseCase(repoVehiculo);

Console.WriteLine("Eliminando el Vehiculo con ID {0}",2);
eliminarVehiculo.Ejecutar(2);

List<Vehiculo> listaVehiculo = listarVehiculo.Ejecutar();
foreach(Vehiculo elem in listaVehiculo){
    Console.WriteLine(elem);
}

*/

