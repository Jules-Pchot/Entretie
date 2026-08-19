using FileVirementsExercice;

Console.WriteLine("=== Exercice 4 : File d'attente de virements (FIFO) ===\n");

TestRunner.Test("AjouterVirement incremente NombreEnAttente", () =>
{
    var file = new FileAttenteVirements(1000m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001"));
    file.AjouterVirement(new Virement("V2", 200m, "FR76-0002"));
    return file.NombreEnAttente == 2;
});

TestRunner.Test("TraiterProchain traite le virement le plus ancien en premier (FIFO)", () =>
{
    var file = new FileAttenteVirements(1000m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001"));
    file.AjouterVirement(new Virement("V2", 200m, "FR76-0002"));
    var resultat = file.TraiterProchain();
    return resultat != null && resultat.Virement.Id == "V1" && resultat.Succes;
});

TestRunner.Test("TraiterProchain debite le solde en cas de succes", () =>
{
    var file = new FileAttenteVirements(1000m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001"));
    file.TraiterProchain();
    return file.SoldeDisponible == 900m;
});

TestRunner.Test("TraiterProchain refuse et ne debite pas si solde insuffisant", () =>
{
    var file = new FileAttenteVirements(50m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001"));
    var resultat = file.TraiterProchain();
    return resultat is { Succes: false } && file.SoldeDisponible == 50m;
});

TestRunner.Test("TraiterProchain retourne null si la file est vide", () =>
{
    var file = new FileAttenteVirements(1000m);
    return file.TraiterProchain() == null;
});

TestRunner.Test("TraiterProchain retire le virement de la file (qu'il soit accepte ou refuse)", () =>
{
    var file = new FileAttenteVirements(50m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001")); // sera refuse
    file.TraiterProchain();
    return file.NombreEnAttente == 0;
});

TestRunner.Test("TraiterTous traite tous les virements dans l'ordre FIFO", () =>
{
    var file = new FileAttenteVirements(250m);
    file.AjouterVirement(new Virement("V1", 100m, "FR76-0001")); // OK, solde -> 150
    file.AjouterVirement(new Virement("V2", 200m, "FR76-0002")); // REFUSE (solde 150 < 200)
    file.AjouterVirement(new Virement("V3", 50m, "FR76-0003"));  // OK, solde -> 100
    var resultats = file.TraiterTous();

    return resultats.Count == 3
        && resultats[0].Virement.Id == "V1" && resultats[0].Succes
        && resultats[1].Virement.Id == "V2" && !resultats[1].Succes
        && resultats[2].Virement.Id == "V3" && resultats[2].Succes
        && file.SoldeDisponible == 100m
        && file.NombreEnAttente == 0;
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
