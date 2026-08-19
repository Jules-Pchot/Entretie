namespace DoublonsVirementsExercice;

public static class DetecteurDoublons
{
    /// TODO 1 :
    /// Le service de virements a parfois un bug qui soumet deux fois le meme virement
    /// (meme ReferenceId). Retourner la liste des ReferenceId qui apparaissent
    /// plusieurs fois dans "virements" (chaque reference en doublon ne doit apparaitre
    /// qu'une seule fois dans le resultat).
    /// Indice : Dictionary&lt;string,int&gt; pour compter les occurrences, ou HashSet&lt;string&gt;
    /// pour detecter les elements deja vus.
    public static List<string> TrouverReferencesEnDoublon(List<Virement> virements)
    {
        throw new NotImplementedException();
    }

    /// TODO 2 :
    /// Retourner true si toutes les references sont uniques, false des qu'un doublon
    /// est trouve. Cette methode doit pouvoir s'arreter au premier doublon rencontre
    /// (pas besoin de scanner toute la liste dans ce cas) : utiliser un HashSet&lt;string&gt;.
    public static bool ToutesLesReferencesSontUniques(List<Virement> virements)
    {
        throw new NotImplementedException();
    }
}
