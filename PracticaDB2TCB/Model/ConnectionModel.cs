using PracticaDB2TCB.Utils;

namespace PracticaDB2TCB.Model;

public class ConnectionModel
{
    private string _password;
    public string Host { get; set; }
    public string Database { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get => AesUtils.DescifrarTexto(Convert.FromBase64String(_password)); set => _password = value; }
    public string ConnectionString => $"Server={Host};Port={Port};Database={Database};Uid={User};Pwd={Password};";
}