namespace ReportingLinqExercice;

public static class RapportBancaire
{
    public static Dictionary<string, decimal> TotalParCompte(List<Transaction> transactions)
    {
        return transactions
            .GroupBy(t => t.CompteId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Montant));
    }

    public static List<Transaction> TopTransactions(List<Transaction> transactions, int n)
    {
        return transactions
            .OrderByDescending(t => t.Montant)
            .Take(n)
            .ToList();
    }

    public static Dictionary<string, decimal> MoyenneParCategorie(List<Transaction> transactions)
    {
        return transactions
            .GroupBy(t => t.Categorie)
            .ToDictionary(g => g.Key, g => Math.Round(g.Average(t => t.Montant), 2));
    }
}
