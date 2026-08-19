namespace FileVirementsExercice;

/// Simule le traitement par lots (batch) des virements en attente, dans l'ordre
/// FIFO (First In, First Out) : le premier virement soumis est le premier traite.
public class FileAttenteVirements
{
    private readonly Queue<Virement> _enAttente = new();

    public decimal SoldeDisponible { get; private set; }

    public int NombreEnAttente => _enAttente.Count;

    public FileAttenteVirements(decimal soldeInitial)
    {
        SoldeDisponible = soldeInitial;
    }

    /// TODO 1 :
    /// Ajouter le virement a la fin de la file d'attente.
    public void AjouterVirement(Virement virement)
    {
        throw new NotImplementedException();
    }

    /// TODO 2 :
    /// Traiter le prochain virement de la file (le plus ancien en premier - FIFO) :
    /// - Si le solde disponible est suffisant, debiter le solde et retourner un
    ///   ResultatTraitement avec Succes = true.
    /// - Sinon, retourner un ResultatTraitement avec Succes = false, SANS modifier
    ///   le solde, et sans remettre le virement dans la file.
    /// - Retourne null si la file est vide (aucun virement a traiter).
    public ResultatTraitement? TraiterProchain()
    {
        throw new NotImplementedException();
    }

    /// TODO 3 :
    /// Traiter tous les virements actuellement en attente, dans l'ordre FIFO,
    /// et retourner la liste de tous les ResultatTraitement (succes et echecs).
    /// Reutiliser TraiterProchain().
    public List<ResultatTraitement> TraiterTous()
    {
        throw new NotImplementedException();
    }
}
