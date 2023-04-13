using PracticaDB2TCB.DB;
using PracticaDB2TCB.Utils;

namespace PracticaDB2TCB.Service;

public class CoreService
{
    private readonly MovimientoDBContext dbContext;
    private readonly ConfigUtils configUtils;
    private readonly InitialValuesService initialService;
    private readonly ProcessService processService;
    
    public CoreService()
    {
        configUtils = new();
        dbContext = new(configUtils.ConnectionModel);
        initialService = new(dbContext, configUtils.MovimientoList);
        processService = new(configUtils.ConnectionModel, configUtils.ProcessList);
    }
    public void Run()
    {
        initialService.Run();
        processService.Run();
    }
}