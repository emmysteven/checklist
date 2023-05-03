using Checklist.Domain.Common;

namespace Checklist.Domain.Entities;

public class Summary  : AuditableEntity
{
    public string Makers { get; set; }
    public string Checkers { get; set; }
    public string Dbas { get; set; }
    public DateTime EodDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Duration { get; set; }
    public int TxnCount { get; set; }

    public Summary(
        string makers, 
        string checkers, 
        string dbas,
        DateTime eodDate, 
        string startTime,
        string endTime,
        string duration,
        int txnCount
        ) {
        Makers = makers;
        Checkers = checkers;
        Dbas = dbas;
        EodDate = eodDate;
        StartTime = startTime;
        EndTime = endTime;
        Duration = duration;
        TxnCount = txnCount;
    }
}