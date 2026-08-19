namespace PileAnnulationExercice;

public enum TypeOperation
{
    Depot,
    Retrait
}

public class Operation
{
    public TypeOperation Type { get; }
    public decimal Montant { get; }

    public Operation(TypeOperation type, decimal montant)
    {
        Type = type;
        Montant = montant;
    }

    public override string ToString() => $"{Type} de {Montant:0.00} EUR";
}
