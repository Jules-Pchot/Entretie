namespace FileVirementsExercice;

public class FileAttenteVirements
{
    private readonly Queue<Virement> _enAttente = new();

    public decimal SoldeDisponible { get; private set; }

    public int NombreEnAttente => _enAttente.Count;

    public FileAttenteVirements(decimal soldeInitial)
    {
        SoldeDisponible = soldeInitial;
    }

    public void AjouterVirement(Virement virement)
    {
        _enAttente.Enqueue(virement);
    }

    public ResultatTraitement? TraiterProchain()
    {
        if (_enAttente.Count == 0)
            return null;

        var virement = _enAttente.Dequeue();

        if (virement.Montant > SoldeDisponible)
        {
            return new ResultatTraitement(virement, false, "Solde disponible insuffisant");
        }

        SoldeDisponible -= virement.Montant;
        return new ResultatTraitement(virement, true, "Virement traite avec succes");
    }

    public List<ResultatTraitement> TraiterTous()
    {
        var resultats = new List<ResultatTraitement>();

        ResultatTraitement? resultat;
        while ((resultat = TraiterProchain()) != null)
        {
            resultats.Add(resultat);
        }

        return resultats;
    }
}
