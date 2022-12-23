using System.Device.Gpio;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{apiKey}", (string apiKey) =>
{
    if (apiKey != app.Configuration["ApiKey"])
    {
        return "Dje ces kraaalju";
    }
    
    int pin = 18;
    using var controller = new GpioController();
    controller.OpenPin(pin, PinMode.Output);
    controller.Write(pin, PinValue.High );
    Thread.Sleep(3000);
    controller.Write(pin, PinValue.Low);

    return "Otvoreno kraaalju!!!!!!";
});

app.Run();
