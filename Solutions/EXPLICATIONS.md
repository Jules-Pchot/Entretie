# Explications des solutions

Ce document détaille, pour chacun des 5 exercices, la logique de la solution,
ce qu'elle est censée démontrer chez le candidat, les pièges classiques et
des variantes de réponses acceptables.

---

## Exercice 1 — Compte Bancaire (`List<T>`, OOP, exceptions)

### Logique de la solution

```csharp
public void Deposer(decimal montant, string description = "Depot")
{
    if (montant <= 0)
        throw new ArgumentException("Le montant doit etre strictement positif.", nameof(montant));

    Solde += montant;
    _historique.Add(new Transaction(TypeTransaction.Depot, montant, DateTime.Now, description));
}
```

`Retirer` suit la même logique mais vérifie en plus `montant > Solde` avant de
lever `SoldeInsuffisantException`. `ObtenirHistorique` utilise LINQ
(`Where` + `OrderByDescending`) pour filtrer et trier en une seule expression.

### Ce que ça démontre

- **Encapsulation** : `Solde` a un setter `private`, donc il ne peut être
  modifié que via `Deposer`/`Retirer` — invariant protégé.
- **Exceptions métier** : créer une exception dédiée (`SoldeInsuffisantException`)
  plutôt que de renvoyer un booléen ou lever une exception générique.
- **Validation défensive** : vérifier les entrées avant de modifier l'état.

### Pièges fréquents à observer chez le candidat

- Modifier `Solde` **avant** de valider (ex: débiter puis vérifier) → incohérence transitoire.
  On veut : valider d'abord, modifier ensuite.
- Utiliser `float`/`double` pour de l'argent plutôt que `decimal` (erreurs d'arrondi).
- Oublier de trier l'historique ou trier dans le mauvais sens.
- Stocker les erreurs dans l'historique (une tentative refusée ne doit **pas**
  créer de `Transaction`).

### Variante acceptable

Utiliser une boucle `foreach` manuelle avec un `List<Transaction>` triée via
`.Sort(...)` plutôt que LINQ est tout aussi valide — à évaluer selon le
niveau du candidat (LINQ montre une meilleure maîtrise du langage).

---

## Exercice 2 — Détection de doublons de virements (`HashSet<T>`, `Dictionary<K,V>`)

### Logique de la solution

```csharp
public static List<string> TrouverReferencesEnDoublon(List<Virement> virements)
{
    var occurrences = new Dictionary<string, int>();
    foreach (var virement in virements)
    {
        occurrences.TryGetValue(virement.ReferenceId, out int count);
        occurrences[virement.ReferenceId] = count + 1;
    }
    return occurrences.Where(kvp => kvp.Value > 1).Select(kvp => kvp.Key).ToList();
}

public static bool ToutesLesReferencesSontUniques(List<Virement> virements)
{
    var referencesVues = new HashSet<string>();
    foreach (var virement in virements)
    {
        if (!referencesVues.Add(virement.ReferenceId))
            return false; // Add() retourne false si l'element existait deja
    }
    return true;
}
```

### Ce que ça démontre

- Connaître la différence entre `HashSet<T>` (appartenance) et
  `Dictionary<K,V>` (comptage/association clé-valeur).
- Connaître l'astuce `HashSet<T>.Add()` qui retourne un `bool` indiquant si
  l'élément a été **réellement** ajouté (utile pour sortir tôt d'une boucle).
- Complexité **O(n)** au lieu d'une double boucle **O(n²)** (comparer chaque
  virement à tous les autres) — un piège classique pour juger la rigueur
  algorithmique du candidat.

### Pièges fréquents à observer chez le candidat

- Utiliser une double boucle `for`/`foreach` imbriquée → fonctionne mais
  n'est pas optimal ; bon point de discussion sur la complexité.
- Oublier `TryGetValue` et écrire `occurrences[id]++` directement (lève une
  `KeyNotFoundException` si la clé n'existe pas encore) — sauf s'ils
  initialisent à 0 avec `GetValueOrDefault`.
- Ne pas dédupliquer le résultat de `TrouverReferencesEnDoublon` (renvoyer la
  référence en double autant de fois qu'elle apparaît).

### Variante acceptable

```csharp
occurrences[virement.ReferenceId] = occurrences.GetValueOrDefault(virement.ReferenceId) + 1;
```
ou une solution 100% LINQ :
```csharp
return virements
    .GroupBy(v => v.ReferenceId)
    .Where(g => g.Count() > 1)
    .Select(g => g.Key)
    .ToList();
```

---

## Exercice 3 — Pile d'annulation d'opérations (`Stack<T>`, LIFO)

### Logique de la solution

```csharp
public void Executer(Operation operation)
{
    Solde += operation.Type == TypeOperation.Depot ? operation.Montant : -operation.Montant;
    _historique.Push(operation);
}

public Operation? Annuler()
{
    if (_historique.Count == 0) return null;
    var derniereOperation = _historique.Pop();
    Solde += derniereOperation.Type == TypeOperation.Depot
        ? -derniereOperation.Montant
        : derniereOperation.Montant;
    return derniereOperation;
}
```

### Ce que ça démontre

- Compréhension du comportement **LIFO** (Last In, First Out) d'une `Stack<T>`.
- Capacité à raisonner sur une **opération inverse** (annuler un dépôt =
  retirer le montant, et vice-versa) — un bon indicateur de rigueur logique.
- Gestion propre d'un cas limite (pile vide → retour `null`, pas d'exception).

### Pièges fréquents à observer chez le candidat

- Utiliser `Peek()` au lieu de `Pop()` dans `Annuler` (l'opération resterait
  dans la pile et pourrait être annulée deux fois).
- Inverser le signe dans le mauvais sens (recréer l'effet au lieu de l'annuler).
- Utiliser une `List<T>` avec `RemoveAt(Count - 1)` pour simuler une pile :
  ça fonctionne, mais c'est l'occasion de demander "connaissez-vous une
  structure plus adaptée ?" pour amener `Stack<T>`.
- Type de retour non-nullable (`Operation` au lieu de `Operation?`) qui
  oblige à lever une exception ou retourner un objet "vide" pour le cas
  pile vide — discutable mais à signaler.

---

## Exercice 4 — File d'attente de virements (`Queue<T>`, FIFO)

### Logique de la solution

```csharp
public ResultatTraitement? TraiterProchain()
{
    if (_enAttente.Count == 0) return null;
    var virement = _enAttente.Dequeue();

    if (virement.Montant > SoldeDisponible)
        return new ResultatTraitement(virement, false, "Solde disponible insuffisant");

    SoldeDisponible -= virement.Montant;
    return new ResultatTraitement(virement, true, "Virement traite avec succes");
}

public List<ResultatTraitement> TraiterTous()
{
    var resultats = new List<ResultatTraitement>();
    ResultatTraitement? resultat;
    while ((resultat = TraiterProchain()) != null)
        resultats.Add(resultat);
    return resultats;
}
```

### Ce que ça démontre

- Compréhension du comportement **FIFO** (First In, First Out) d'une `Queue<T>`.
- Capacité à **réutiliser une méthode** (`TraiterTous` s'appuie sur
  `TraiterProchain` plutôt que de dupliquer la logique) — bon indicateur de
  qualité de code (DRY).
- Gestion d'un échec métier (solde insuffisant) **sans corrompre l'état** :
  le virement refusé quitte quand même la file (il n'est pas remis en
  attente indéfiniment), et le solde n'est pas débité.

### Pièges fréquents à observer chez le candidat

- Utiliser `Peek()` au lieu de `Dequeue()` avant de vérifier le solde, puis
  oublier de retirer le virement de la file en cas de refus (incohérence
  d'état).
- Réintroduire le virement refusé dans la file (`Enqueue` à nouveau) —
  provoquerait une boucle infinie potentielle dans `TraiterTous`.
- Copier-coller la logique de `TraiterProchain` dans `TraiterTous` au lieu
  de la réutiliser.
- Condition de boucle incorrecte dans `TraiterTous` (ex: `while (file non vide)`
  au lieu de tester le retour de `TraiterProchain`) — fonctionne mais est
  redondant avec le check déjà présent dans `TraiterProchain`.

---

## Exercice 5 — Reporting bancaire avec LINQ (`GroupBy`, `Dictionary<K,V>`)

### Logique de la solution

```csharp
public static Dictionary<string, decimal> TotalParCompte(List<Transaction> transactions)
    => transactions.GroupBy(t => t.CompteId).ToDictionary(g => g.Key, g => g.Sum(t => t.Montant));

public static List<Transaction> TopTransactions(List<Transaction> transactions, int n)
    => transactions.OrderByDescending(t => t.Montant).Take(n).ToList();

public static Dictionary<string, decimal> MoyenneParCategorie(List<Transaction> transactions)
    => transactions.GroupBy(t => t.Categorie)
                    .ToDictionary(g => g.Key, g => Math.Round(g.Average(t => t.Montant), 2));
```

### Ce que ça démontre

- Maîtrise de LINQ : `GroupBy`, `Sum`, `Average`, `OrderByDescending`, `Take`.
- Conversion fluide entre `IEnumerable<T>`, `List<T>` et `Dictionary<K,V>`
  (`ToList()`, `ToDictionary()`).
- Attention portée à la précision numérique (`decimal` + `Math.Round`) —
  essentiel dans un contexte bancaire.

### Pièges fréquents à observer chez le candidat

- Utiliser `.First()` au lieu de `.Take(n)` pour `TopTransactions` (ne
  retournerait qu'un seul élément).
- Oublier `Math.Round` dans `MoyenneParCategorie` → le test échoue à cause
  de décimales supplémentaires (`45.766666...` au lieu de `45.77`).
- Écrire une boucle manuelle avec des `Dictionary<string, decimal>` et des
  compteurs à la main pour calculer la moyenne — fonctionne, mais c'est
  l'occasion idéale de demander "connaissez-vous LINQ pour simplifier ça ?".
- Modifier la liste `transactions` d'origine (trier en place avec `.Sort()`)
  au lieu de retourner une nouvelle liste triée.

---

## Grille d'évaluation rapide (suggestion)

| Exercice | Structure clé          | Niveau attendu en junior | Niveau attendu en confirmé |
|----------|-------------------------|---------------------------|------------------------------|
| 1        | `List<T>`, exceptions   | Implémente avec aide       | Implémente seul + explique l'encapsulation |
| 2        | `HashSet`/`Dictionary`  | Trouve une solution O(n²) | Trouve directement la solution O(n) |
| 3        | `Stack<T>`              | Connaît Push/Pop            | Raisonne vite sur l'opération inverse |
| 4        | `Queue<T>`              | Connaît Enqueue/Dequeue      | Réutilise le code, gère les cas d'échec proprement |
| 5        | LINQ                    | Connaît `Where`/`Select`     | Maîtrise `GroupBy`, `Average`, conversions |
