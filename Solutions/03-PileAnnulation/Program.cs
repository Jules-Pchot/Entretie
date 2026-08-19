using PileAnnulationExercice;

Console.WriteLine("=== Exercice 3 : Pile d'annulation d'operations (SOLUTION) ===\n");

TestRunner.Test("Executer un depot augmente le solde", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Depot, 50m));
    return gestionnaire.Solde == 150m;
});

TestRunner.Test("Executer un retrait diminue le solde", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Retrait, 30m));
    return gestionnaire.Solde == 70m;
});

TestRunner.Test("Annuler un depot retire le montant du solde", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Depot, 50m));
    var annulee = gestionnaire.Annuler();
    return gestionnaire.Solde == 100m && annulee?.Type == TypeOperation.Depot;
});

TestRunner.Test("Annuler un retrait redonne le montant au solde", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Retrait, 30m));
    var annulee = gestionnaire.Annuler();
    return gestionnaire.Solde == 100m && annulee?.Type == TypeOperation.Retrait;
});

TestRunner.Test("Les annulations respectent l'ordre LIFO (dernier execute = premier annule)", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Depot, 50m));
    gestionnaire.Executer(new Operation(TypeOperation.Retrait, 20m));
    var premiereAnnulation = gestionnaire.Annuler();
    return premiereAnnulation?.Type == TypeOperation.Retrait && gestionnaire.Solde == 150m;
});

TestRunner.Test("Annuler sur historique vide retourne null et ne change pas le solde", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    var annulee = gestionnaire.Annuler();
    return annulee == null && gestionnaire.Solde == 100m;
});

TestRunner.Test("NombreOperationsAnnulables reflete correctement la pile", () =>
{
    var gestionnaire = new GestionnaireOperations(100m);
    gestionnaire.Executer(new Operation(TypeOperation.Depot, 10m));
    gestionnaire.Executer(new Operation(TypeOperation.Depot, 20m));
    gestionnaire.Annuler();
    return gestionnaire.NombreOperationsAnnulables() == 1;
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
