namespace PileAnnulationExercice;

public class GestionnaireOperations
{
    public decimal Solde { get; private set; }

    private readonly Stack<Operation> _historique = new();

    public GestionnaireOperations(decimal soldeInitial)
    {
        Solde = soldeInitial;
    }

    public void Executer(Operation operation)
    {
        Solde += operation.Type == TypeOperation.Depot ? operation.Montant : -operation.Montant;
        _historique.Push(operation);
    }

    public Operation? Annuler()
    {
        if (_historique.Count == 0)
            return null;

        var derniereOperation = _historique.Pop();

        // On applique l'effet inverse de celui applique par Executer.
        Solde += derniereOperation.Type == TypeOperation.Depot
            ? -derniereOperation.Montant
            : derniereOperation.Montant;

        return derniereOperation;
    }

    public int NombreOperationsAnnulables() => _historique.Count;
}
