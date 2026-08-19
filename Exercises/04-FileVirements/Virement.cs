namespace FileVirementsExercice;

public class Virement
{
    public string Id { get; }
    public decimal Montant { get; }
    public string CompteDestination { get; }

    public Virement(string id, decimal montant, string compteDestination)
    {
        Id = id;
        Montant = montant;
        CompteDestination = compteDestination;
    }

    public override string ToString() => $"{Id} | {Montant,10:0.00} EUR -> {CompteDestination}";
}

public class ResultatTraitement
{
    public Virement Virement { get; }
    public bool Succes { get; }
    public string Message { get; }

    public ResultatTraitement(Virement virement, bool succes, string message)
    {
        Virement = virement;
        Succes = succes;
        Message = message;
    }

    public override string ToString() => $"{(Succes ? "OK" : "REFUSE")} - {Virement} ({Message})";
}
