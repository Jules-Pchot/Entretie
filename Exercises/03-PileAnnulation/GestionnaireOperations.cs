namespace PileAnnulationExercice;

/// Simule un distributeur qui doit pouvoir annuler la derniere operation
/// effectuee (fonctionnalite "Undo"), a la maniere d'une pile d'annulation
/// dans un editeur de texte.
public class GestionnaireOperations
{
    public decimal Solde { get; private set; }

    private readonly Stack<Operation> _historique = new();

    public GestionnaireOperations(decimal soldeInitial)
    {
        Solde = soldeInitial;
    }

    /// TODO 1 :
    /// Appliquer l'operation au solde (Depot => +Montant, Retrait => -Montant)
    /// puis l'empiler dans l'historique pour permettre une annulation future.
    public void Executer(Operation operation)
    {
        throw new NotImplementedException();
    }

    /// TODO 2 :
    /// Annuler la derniere operation executee : la retirer du sommet de la pile
    /// et appliquer l'effet inverse sur le solde (annuler un Depot => -Montant,
    /// annuler un Retrait => +Montant).
    /// Retourne l'operation annulee, ou null si aucune operation n'est annulable.
    public Operation? Annuler()
    {
        throw new NotImplementedException();
    }

    /// TODO 3 :
    /// Retourner le nombre d'operations encore presentes dans l'historique
    /// (donc annulables), sans le modifier.
    public int NombreOperationsAnnulables()
    {
        throw new NotImplementedException();
    }
}
