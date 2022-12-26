using System.Device.Gpio;
// ReSharper disable AccessToDisposedClosure

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

using var controller = new GpioController();
int pin = 18;
controller.OpenPin(pin, PinMode.Output);
controller.Write(pin, PinValue.High );

app.MapGet("/{apiKey}", (string apiKey) =>
{
    if (apiKey != app.Configuration["ApiKey"])
    {
        return "Dje ces kraaalju";
    }

    controller.Write(pin, PinValue.Low);
    Console.WriteLine($"{DateTime.Now} [ON] Relay");
    try
    {
        Thread.Sleep(TimeSpan.FromSeconds(app.Configuration.GetValue<int>("TriggerIntervalInSec")));
    }
    finally
    {
        controller.Write(pin, PinValue.High);
        Console.WriteLine($"{DateTime.Now} [OFF] Relay");
    }

    return "Otvoreno kraaalju!!!!!!";
});

app.Run();
