namespace CompteBancaireExercice;

public class SoldeInsuffisantException : Exception
{
    public SoldeInsuffisantException(string message) : base(message) { }
}

public class CompteBancaire
{
    public string Titulaire { get; }
    public decimal Solde { get; private set; }

    private readonly List<Transaction> _historique = new();

    public CompteBancaire(string titulaire, decimal soldeInitial = 0)
    {
        Titulaire = titulaire;
        Solde = soldeInitial;
    }

    /// TODO 1 :
    /// - Lever une ArgumentException si montant <= 0.
    /// - Ajouter le montant au solde.
    /// - Enregistrer une Transaction (Type = Depot) dans l'historique, avec DateTime.Now.
    public void Deposer(decimal montant, string description = "Depot")
    {
        throw new NotImplementedException();
    }

    /// TODO 2 :
    /// - Lever une ArgumentException si montant <= 0.
    /// - Lever une SoldeInsuffisantException si le solde est insuffisant.
    /// - Retirer le montant du solde.
    /// - Enregistrer une Transaction (Type = Retrait) dans l'historique.
    public void Retirer(decimal montant, string description = "Retrait")
    {
        throw new NotImplementedException();
    }

    /// TODO 3 :
    /// Retourner la liste des transactions dont la Date est >= depuis,
    /// triee de la plus recente a la plus ancienne.
    public List<Transaction> ObtenirHistorique(DateTime depuis)
    {
        throw new NotImplementedException();
    }
}
