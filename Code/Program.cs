using SoapCore;
using SoapService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddSingleton<ICalculadoraService, CalculadoraService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseSoapEndpoint<ICalculadoraService>(
    "/Calculadora.asmx", new SoapEncoderOptions()
    );

app.Run();