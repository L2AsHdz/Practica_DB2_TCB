using Newtonsoft.Json;
using PracticaDB2TCB.DB;
using PracticaDB2TCB.Model;

namespace PracticaDB2TCB.Utils;

public class ConfigUtils
{
    private string path = "Config.json";
    private string jsonContent;
    
    public List<Movimiento> MovimientoList { get; set; }
    public List<Process> ProcessList { get; set; }
    
    public ConnectionModel ConnectionModel { get; set; }

    public ConfigUtils()
    {
        jsonContent = File.ReadAllText(path);
        ReadConfig();
    }
    
    public void ReadConfig()
    {
        var dynamicJson = JsonConvert.DeserializeObject<dynamic>(jsonContent);

        MovimientoList = JsonConvert.DeserializeObject<List<Movimiento>>(dynamicJson!.Movements.ToString());
        ProcessList = JsonConvert.DeserializeObject<List<Process>>(dynamicJson!.Processes.ToString());
        ConnectionModel = JsonConvert.DeserializeObject<ConnectionModel>(dynamicJson!.Connection.ToString());
    }
}