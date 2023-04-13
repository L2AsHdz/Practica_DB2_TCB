using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PracticaDB2TCB.DB;
using PracticaDB2TCB.Model;
using Process = PracticaDB2TCB.Model.Process;

namespace PracticaDB2TCB.Service;

public class ProcessService
{
    private readonly ConnectionModel connectionModel;
    private readonly List<Process> processList;

    public ProcessService(ConnectionModel connectionModel, List<Process> processList)
    {
        this.connectionModel = connectionModel;
        this.processList = processList;
    }

    public void Run()
    {
        List<Task> tasks = new();
        processList.ForEach(p =>
        {
            var task = Task.Run(async () =>
            {
                var dbContext = new MovimientoDBContext(connectionModel);
                
                Stopwatch stopwatch = new();
                var maxTime = TimeSpan.FromMilliseconds(p.ExecutionTime);
                
                stopwatch.Start();

                while (stopwatch.Elapsed <= maxTime)
                {
                    await Task.Delay(p.TimeOut);
                    
                    string query;
                    query = p.Increase
                        ? $"UPDATE Movimiento SET valor = valor + @amount WHERE id = @id"
                        : $"UPDATE Movimiento SET valor = valor - @amount WHERE id = @id";
                    
                    
                    if (p.LockTable) await dbContext.Database.ExecuteSqlRawAsync("LOCK TABLES Movimiento WRITE");
                    
                    await dbContext.Database.ExecuteSqlRawAsync(query, new MySqlParameter("@amount", p.Amount), new MySqlParameter("@id", p.ValueId));
                    var op = p.Increase ? "Incremento" : "Decremento";
                    Console.WriteLine($"{op} de {p.Amount} en el valor con id {p.ValueId} - {stopwatch.ElapsedMilliseconds}ms");
                    
                    if (p.LockTable) await dbContext.Database.ExecuteSqlRawAsync("UNLOCK TABLES");
                }
            });
            tasks.Add(task);
        });
        
        Task.WaitAll(tasks.ToArray());
    }
}