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
    Thread.Sleep(TimeSpan.FromSeconds(app.Configuration.GetValue<int>("TriggerIntervalInSec")));
    controller.Write(pin, PinValue.High);

    return "Otvoreno kraaalju!!!!!!";
});

app.Run();
