using CompteBancaireExercice;

Console.WriteLine("=== Exercice 1 : Compte Bancaire ===\n");

var compte = new CompteBancaire("Alice Dupont", 100m);

TestRunner.Test("Depot valide augmente le solde", () =>
{
    compte.Deposer(50m);
    return compte.Solde == 150m;
});

TestRunner.Test("Retrait valide diminue le solde", () =>
{
    compte.Retirer(30m);
    return compte.Solde == 120m;
});

TestRunner.Test("Depot refuse si montant <= 0", () =>
{
    try
    {
        compte.Deposer(-10m);
        return false;
    }
    catch (ArgumentException)
    {
        return true;
    }
});

TestRunner.Test("Retrait refuse si solde insuffisant (solde inchange)", () =>
{
    decimal soldeAvant = compte.Solde;
    try
    {
        compte.Retirer(10_000m);
        return false;
    }
    catch (SoldeInsuffisantException)
    {
        return compte.Solde == soldeAvant;
    }
});

TestRunner.Test("Historique contient uniquement les operations reussies, triees du plus recent au plus ancien", () =>
{
    var historique = compte.ObtenirHistorique(DateTime.Now.AddMinutes(-5));
    if (historique.Count != 2) return false; // Depot 50 + Retrait 30 (la tentative refusee ne compte pas)
    return historique[0].Type == TypeTransaction.Retrait && historique[1].Type == TypeTransaction.Depot;
});

TestRunner.Test("Historique filtre correctement par date (rien avant hier)", () =>
{
    var historique = compte.ObtenirHistorique(DateTime.Now.AddDays(1)); // demain -> aucune transaction
    return historique.Count == 0;
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
