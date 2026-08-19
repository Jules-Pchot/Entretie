namespace ReportingLinqExercice;

public static class RapportBancaire
{
    /// TODO 1 :
    /// Retourner le total des montants (Sum), groupe par CompteId,
    /// sous forme de Dictionary&lt;string, decimal&gt;.
    /// A faire avec LINQ (GroupBy + ToDictionary).
    public static Dictionary<string, decimal> TotalParCompte(List<Transaction> transactions)
    {
        throw new NotImplementedException();
    }

    /// TODO 2 :
    /// Retourner les "n" transactions ayant les montants les plus eleves,
    /// triees par montant decroissant.
    /// A faire avec LINQ (OrderByDescending + Take).
    public static List<Transaction> TopTransactions(List<Transaction> transactions, int n)
    {
        throw new NotImplementedException();
    }

    /// TODO 3 :
    /// Retourner le montant moyen par categorie, sous forme de
    /// Dictionary&lt;string, decimal&gt;, arrondi a 2 decimales.
    /// A faire avec LINQ (GroupBy + Average).
    public static Dictionary<string, decimal> MoyenneParCategorie(List<Transaction> transactions)
    {
        throw new NotImplementedException();
    }
}
