namespace DoublonsVirementsExercice;

public class Virement
{
    public string ReferenceId { get; }
    public decimal Montant { get; }
    public string Beneficiaire { get; }

    public Virement(string referenceId, decimal montant, string beneficiaire)
    {
        ReferenceId = referenceId;
        Montant = montant;
        Beneficiaire = beneficiaire;
    }

    public override string ToString() => $"{ReferenceId} | {Montant,10:0.00} EUR -> {Beneficiaire}";
}
