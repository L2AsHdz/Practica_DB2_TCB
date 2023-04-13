using Microsoft.EntityFrameworkCore;
using PracticaDB2TCB.DB;

namespace PracticaDB2TCB.Service;

public class InitialValuesService
{
    private readonly MovimientoDBContext dbContext;
    private readonly List<Movimiento> movimientoList;

    public InitialValuesService(MovimientoDBContext dbContext, List<Movimiento> movimientoList)
    {
        this.dbContext = dbContext;
        this.movimientoList = movimientoList;
    }

    public void Run()
    {
        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Movimiento");
        
        movimientoList.ForEach(m =>
        {
            dbContext.Movimiento.Add(m);
        });

        dbContext.SaveChanges();
    }
    
}