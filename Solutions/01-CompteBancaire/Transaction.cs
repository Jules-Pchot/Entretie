namespace CompteBancaireExercice;

public enum TypeTransaction
{
    Depot,
    Retrait
}

public class Transaction
{
    public TypeTransaction Type { get; }
    public decimal Montant { get; }
    public DateTime Date { get; }
    public string Description { get; }

    public Transaction(TypeTransaction type, decimal montant, DateTime date, string description)
    {
        Type = type;
        Montant = montant;
        Date = date;
        Description = description;
    }

    public override string ToString()
        => $"{Date:yyyy-MM-dd HH:mm} | {Type,-7} | {Montant,10:0.00} EUR | {Description}";
}
