namespace Checklist.Domain.Entities;

public class Summary
{
    public string[] Maker { get; set; }
    public string Checker { get; set; }
    public string[] Dba { get; set; }
    public DateTime EodDate { get; set; }
    public int TxnCount { get; set; }

    public Summary(string[] maker, string checker, string[] dba, DateTime eodDate, int txnCount)
    {
        Maker = maker;
        Checker = checker;
        Dba = dba;
        EodDate = eodDate;
        TxnCount = txnCount;
    }
}