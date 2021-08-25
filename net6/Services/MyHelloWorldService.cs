namespace net6.Services;

public interface IHelloWorldService
{
    string HelloWorld {get;}

    string Hello(string name);
}

public class MyHelloWorldService: IHelloWorldService 
{
    public string HelloWorld => "Hello World! I'm a service!";

    public string Hello(string name) => $"Hello {name}! I'm a service!";
}
