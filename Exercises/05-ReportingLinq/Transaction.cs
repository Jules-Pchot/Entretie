namespace ReportingLinqExercice;

public class Transaction
{
    public string CompteId { get; }
    public string Categorie { get; }
    public decimal Montant { get; }
    public DateTime Date { get; }

    public Transaction(string compteId, string categorie, decimal montant, DateTime date)
    {
        CompteId = compteId;
        Categorie = categorie;
        Montant = montant;
        Date = date;
    }

    public override string ToString()
        => $"{Date:yyyy-MM-dd} | {CompteId} | {Categorie,-12} | {Montant,10:0.00} EUR";
}
