using PracticaDB2TCB.Service;

namespace PracticaDB2TCB;
internal class Program
{
    public static void Main(string[] args)
    {
        CoreService service = new();
        service.Run();
    }
}
