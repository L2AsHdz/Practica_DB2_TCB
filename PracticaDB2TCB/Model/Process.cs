namespace PracticaDB2TCB.Model;

public class Process
{
    public int ValueId { get; set; }
    public int Amount { get; set; }
    public int TimeOut { get; set; }
    public int ExecutionTime { get; set; }
    public bool Increase { get; set; }
    public bool LockTable { get; set; }
}