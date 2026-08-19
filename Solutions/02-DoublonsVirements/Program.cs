using DoublonsVirementsExercice;

Console.WriteLine("=== Exercice 2 : Detection de doublons de virements (SOLUTION) ===\n");

var virementsAvecDoublons = new List<Virement>
{
    new("VIR-1001", 250.00m, "Jean Martin"),
    new("VIR-1002", 75.50m, "Sophie Bernard"),
    new("VIR-1003", 1200.00m, "Entreprise ABC"),
    new("VIR-1001", 250.00m, "Jean Martin"),
    new("VIR-1004", 40.00m, "Paul Petit"),
    new("VIR-1003", 1200.00m, "Entreprise ABC"),
};

var virementsSansDoublons = new List<Virement>
{
    new("VIR-2001", 100.00m, "Marie Leroy"),
    new("VIR-2002", 300.00m, "Luc Moreau"),
    new("VIR-2003", 50.00m, "Julie Simon"),
};

TestRunner.Test("Detecte les 2 references en doublon", () =>
{
    var doublons = DetecteurDoublons.TrouverReferencesEnDoublon(virementsAvecDoublons);
    var attendu = new HashSet<string> { "VIR-1001", "VIR-1003" };
    return doublons.Count == 2 && doublons.All(attendu.Contains);
});

TestRunner.Test("Aucun doublon detecte quand la liste est propre", () =>
{
    var doublons = DetecteurDoublons.TrouverReferencesEnDoublon(virementsSansDoublons);
    return doublons.Count == 0;
});

TestRunner.Test("Liste vide -> aucun doublon", () =>
{
    var doublons = DetecteurDoublons.TrouverReferencesEnDoublon(new List<Virement>());
    return doublons.Count == 0;
});

TestRunner.Test("ToutesLesReferencesSontUniques retourne false s'il y a des doublons", () =>
{
    return DetecteurDoublons.ToutesLesReferencesSontUniques(virementsAvecDoublons) == false;
});

TestRunner.Test("ToutesLesReferencesSontUniques retourne true si tout est unique", () =>
{
    return DetecteurDoublons.ToutesLesReferencesSontUniques(virementsSansDoublons) == true;
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
