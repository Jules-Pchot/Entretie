namespace DoublonsVirementsExercice;

public static class DetecteurDoublons
{
    public static List<string> TrouverReferencesEnDoublon(List<Virement> virements)
    {
        var occurrences = new Dictionary<string, int>();

        foreach (var virement in virements)
        {
            occurrences.TryGetValue(virement.ReferenceId, out int count);
            occurrences[virement.ReferenceId] = count + 1;
        }

        return occurrences
            .Where(kvp => kvp.Value > 1)
            .Select(kvp => kvp.Key)
            .ToList();
    }

    public static bool ToutesLesReferencesSontUniques(List<Virement> virements)
    {
        var referencesVues = new HashSet<string>();

        foreach (var virement in virements)
        {
            if (!referencesVues.Add(virement.ReferenceId))
            {
                // Add() retourne false si l'element etait deja present -> doublon trouve
                return false;
            }
        }

        return true;
    }
}
