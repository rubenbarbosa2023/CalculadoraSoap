namespace SoapService.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public int Somar(int a, int b) => a + b;
        public int Subtrair(int a, int b) => a - b;
        public int Dividir(int a, int b) => a / b;
        public int Multiplicar(int a, int b) => a * b;
    }
}
