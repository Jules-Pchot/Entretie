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

    public void Deposer(decimal montant, string description = "Depot")
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit etre strictement positif.", nameof(montant));

        Solde += montant;
        _historique.Add(new Transaction(TypeTransaction.Depot, montant, DateTime.Now, description));
    }

    public void Retirer(decimal montant, string description = "Retrait")
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit etre strictement positif.", nameof(montant));

        if (montant > Solde)
            throw new SoldeInsuffisantException(
                $"Solde insuffisant : solde actuel {Solde:0.00} EUR, retrait demande {montant:0.00} EUR.");

        Solde -= montant;
        _historique.Add(new Transaction(TypeTransaction.Retrait, montant, DateTime.Now, description));
    }

    public List<Transaction> ObtenirHistorique(DateTime depuis)
    {
        return _historique
            .Where(t => t.Date >= depuis)
            .OrderByDescending(t => t.Date)
            .ToList();
    }
}
