using ReportingLinqExercice;

Console.WriteLine("=== Exercice 5 : Reporting bancaire avec LINQ (SOLUTION) ===\n");

var transactions = new List<Transaction>
{
    new("CPT-A", "Alimentation", 45.20m, new DateTime(2026, 8, 1)),
    new("CPT-A", "Loyer",        850.00m, new DateTime(2026, 8, 2)),
    new("CPT-B", "Alimentation", 62.10m, new DateTime(2026, 8, 3)),
    new("CPT-A", "Loisirs",      120.00m, new DateTime(2026, 8, 4)),
    new("CPT-B", "Loyer",        700.00m, new DateTime(2026, 8, 5)),
    new("CPT-C", "Alimentation", 30.00m, new DateTime(2026, 8, 6)),
    new("CPT-B", "Loisirs",      15.90m, new DateTime(2026, 8, 7)),
};

TestRunner.Test("TotalParCompte calcule le bon total pour chaque compte", () =>
{
    var totaux = RapportBancaire.TotalParCompte(transactions);
    return totaux.Count == 3
        && totaux["CPT-A"] == 1015.20m
        && totaux["CPT-B"] == 778.00m
        && totaux["CPT-C"] == 30.00m;
});

TestRunner.Test("TopTransactions retourne les 3 plus gros montants tries par ordre decroissant", () =>
{
    var top3 = RapportBancaire.TopTransactions(transactions, 3);
    return top3.Count == 3
        && top3[0].Montant == 850.00m
        && top3[1].Montant == 700.00m
        && top3[2].Montant == 120.00m;
});

TestRunner.Test("TopTransactions gere n plus grand que la taille de la liste", () =>
{
    var top = RapportBancaire.TopTransactions(transactions, 100);
    return top.Count == transactions.Count;
});

TestRunner.Test("MoyenneParCategorie calcule la bonne moyenne pour 'Alimentation'", () =>
{
    var moyennes = RapportBancaire.MoyenneParCategorie(transactions);
    return moyennes.ContainsKey("Alimentation") && moyennes["Alimentation"] == 45.77m;
});

TestRunner.Test("MoyenneParCategorie couvre toutes les categories", () =>
{
    var moyennes = RapportBancaire.MoyenneParCategorie(transactions);
    return moyennes.Count == 3
        && moyennes.ContainsKey("Loyer")
        && moyennes.ContainsKey("Loisirs");
});

TestRunner.AfficherResume();

static class TestRunner
{
    private static int _reussis = 0;
    private static int _total = 0;

    public static void Test(string nom, Func<bool> test)
    {
        _total++;
        try
        {
            bool ok = test();
            if (ok) _reussis++;
            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"[{(ok ? "OK   " : "ECHEC")}] {nom}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERREUR] {nom} -> {ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    public static void AfficherResume()
    {
        Console.WriteLine();
        Console.ForegroundColor = _reussis == _total ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"Resultat : {_reussis}/{_total} tests reussis");
        Console.ResetColor();
    }
}
