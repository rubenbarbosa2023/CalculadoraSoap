# Calculadora SOAP (.NET Core + SoapCore) — README

> Exemplo simples de **serviço SOAP** escrito em **.NET Core** usando a biblioteca **SoapCore**, com instruções para **macOS** e prints (placeholders) explicativos do fluxo.

---

# Sobre

Este repositório demonstra um **serviço SOAP** de uma calculadora com operações básicas (Add, Subtract, Multiply, Divide). O serviço é implementado em .NET Core e exposto via **SoapCore** para que clientes SOAP (SoapUI, Postman, aplicações .NET, etc.) possam consumir.

---

# Funcionalidades

- Endpoints SOAP com WSDL exposto.  
- Operações:
  - `Add(int a, int b)` → soma  
  - `Subtract(int a, int b)` → subtração  
  - `Multiply(int a, int b)` → multiplicação  
  - `Divide(int a, int b)` → divisão

---

# Pré-requisitos

- macOS (qualquer versão moderna)  
- [.NET SDK (8.0+ recomendado)](https://dotnet.microsoft.com/download) instalado e no `PATH`. Verificar com:  
  ```bash
  dotnet --version
  ```
- Git 
- Ferramenta de teste SOAP (opcional): SoapUI, Postman, curl

---

# Instalação rápida (macOS)

1. Clonar o repositório (ou criar pasta do projeto):
   ```bash
   git clone https://github.com/seu-usuario/calculadora-soap.git
   cd calculadora-soap
   ```

2. Restaurar pacotes:
   ```bash
   dotnet restore
   ```

3. Executar a aplicação:
   ```bash
   dotnet run
   ```

4. Por padrão a app fica disponível em `http://localhost:5000` (ou porta indicada no output). O endpoint SOAP será este: `http://localhost:[tua_porta]/Calculadora.asmx?wsdl`

---

# Estrutura do projeto

```
CalculadoraSOAP/
├─ Services/
│  ├─ ICalculadoraService.cs - Interface
│  └─ CalculadoraService.cs - Lógica do serviço
├─ Properties/
├─ Program.cs - Routing e enconding
```

---

# Código principal

## ICalculadoraService.cs

```csharp
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
```

## CalculadoraService.cs

```csharp
public class CalculadoraService : ICalculadoraService
{
    public int Somar(int a, int b) => a + b;
    public int Subtrair(int a, int b) => a - b;
    public int Dividir(int a, int b) => a / b;
    public int Multiplicar(int a, int b) => a * b;
}
```

## Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddSingleton<ICalculadoraService, CalculadoraService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseSoapEndpoint<ICalculadoraService>("/Calculadora.asmx", new SoapEncoderOptions());

app.Run();
```

---

# Testando

### WSDL / Endpoint

Após executar o projeto, acesse no teu browser:

```
http://localhost:[tua_porta]/Calculadora.asmx?wsdl
```

### Exemplo de requisição SOAP

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Dividir>
         <tem:a>5</tem:a>
         <tem:b>1</tem:b>
      </tem:Dividir>
   </soapenv:Body>
</soapenv:Envelope>
```

**Resposta esperada:**

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
   <s:Body>
      <DividirResponse xmlns="http://tempuri.org/">
         <DividirResult>5</DividirResult>
      </DividirResponse>
   </s:Body>
</s:Envelope>
```

---

# Fluxo e explicação passo-a-passo

1. O **cliente SOAP** envia uma requisição (ex: `Add(3,5)`) para o endpoint.  
2. O **SoapCore** traduz o XML recebido para uma chamada C# (invocando o método correto).  
3. O método é executado no `CalculadoraService`.  
4. O retorno é convertido novamente em XML (SOAP) e enviado de volta ao cliente.

### Fluxo resumido

```
Cliente SOAP → SoapCore Middleware → CalculadoraService → Resposta XML (SOAP)
```

---

# Prints

| Etapa | Descrição | Caminho sugerido |
|-------|------------|------------------|
| 1 | Estrutura do projeto | `./docs/print1_estrutura.png` |
| 2 | Execução no terminal | `./docs/print2_execucao.png` |
| 3 | WSDL no navegador | `./docs/print3_wsdl.png` |
| 4 | Teste no SoapUI/Postman | `./docs/print4_soapui.png` |

---

# Licença & Autor

**Autor:** Rúben Barbosa

Licença: [MIT License](LICENSE)
