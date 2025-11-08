using System.ServiceModel;

namespace SoapService.Services
{
    [ServiceContract]
    public interface ICalculadoraService
    {
        [OperationContract]
        int Somar(int a, int b);

        [OperationContract]
        int Subtrair(int a, int b);

        [OperationContract]
        int Dividir(int a, int b);

        [OperationContract]
        int Multiplicar(int a, int b);
    }
}
