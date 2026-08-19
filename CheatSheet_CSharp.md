# Cheat Sheet C# / .NET — Structures de données & LINQ

Aide-mémoire pour réviser les structures de données et notions couvertes par
les 5 exercices. Contexte bancaire utilisé pour les exemples.

---

## 1. `List<T>` — liste dynamique ordonnée

Usage : historiques, collections modifiables, accès par index.

```csharp
var historique = new List<Transaction>();

historique.Add(transaction);              // ajoute a la fin
historique.Remove(transaction);           // supprime la 1ere occurrence
historique.RemoveAt(0);                   // supprime par index
historique[0];                            // acces par index
historique.Count;                         // nombre d'elements
historique.Contains(transaction);         // O(n)
historique.Sort((a, b) => a.Date.CompareTo(b.Date)); // tri en place
```

**Complexité** : accès par index O(1), insertion en fin O(1) amorti,
insertion en milieu / suppression O(n), recherche `Contains` O(n).

**Piège classique** : `List<T>.Contains` est O(n) — si on cherche
fréquemment une appartenance, préférer `HashSet<T>`.

---

## 2. `Dictionary<TKey, TValue>` — table de hachage clé/valeur

Usage : recherche rapide par identifiant (ex: `CompteId` → solde),
comptage, regroupement.

```csharp
var soldeParCompte = new Dictionary<string, decimal>();

soldeParCompte["CPT-A"] = 1000m;                          // ajoute ou remplace
soldeParCompte.Add("CPT-B", 500m);                        // leve une exception si la cle existe deja
soldeParCompte.TryGetValue("CPT-A", out decimal solde);   // acces securise (pas d'exception)
soldeParCompte.ContainsKey("CPT-A");                      // O(1)
soldeParCompte.GetValueOrDefault("CPT-Z", 0m);             // valeur par defaut si absent
foreach (var kvp in soldeParCompte) { /* kvp.Key, kvp.Value */ }
```

**Complexité** : accès, ajout, suppression en moyenne O(1).

**Piège classique** : `dict[cle]` leve une `KeyNotFoundException` si la clé
n'existe pas — toujours préférer `TryGetValue` ou `GetValueOrDefault` en
lecture, sauf si on est certain de l'existence de la clé.

**Pattern de comptage** :
```csharp
compteurs.TryGetValue(cle, out int n);
compteurs[cle] = n + 1;
// ou, plus concis :
compteurs[cle] = compteurs.GetValueOrDefault(cle) + 1;
```

---

## 3. `HashSet<T>` — ensemble sans doublons

Usage : détection de doublons, tests d'appartenance rapides,
opérations ensemblistes (union, intersection).

```csharp
var referencesVues = new HashSet<string>();

bool ajoute = referencesVues.Add("VIR-1001"); // false si deja present -> detection de doublon en 1 ligne
referencesVues.Contains("VIR-1001");           // O(1)
referencesVues.Remove("VIR-1001");             // O(1)

// Operations ensemblistes
setA.UnionWith(setB);
setA.IntersectWith(setB);
setA.ExceptWith(setB);
```

**Complexité** : ajout, suppression, recherche en moyenne O(1).

**Astuce interview** : `HashSet<T>.Add()` retourne `bool` — très pratique
pour détecter un doublon dès qu'il apparaît, sans structure supplémentaire.

---

## 4. `Stack<T>` — pile LIFO (Last In, First Out)

Usage : annulation d'actions (undo), parcours en profondeur,
évaluation d'expressions, pile d'appels.

```csharp
var pile = new Stack<Operation>();

pile.Push(operation);   // empile (ajoute au sommet)
var derniere = pile.Pop();    // depile et retourne le sommet (leve si vide)
var sommet = pile.Peek();     // lit le sommet sans le retirer (leve si vide)
pile.TryPop(out var op);      // version securisee (pas d'exception si vide)
pile.Count;                    // nombre d'elements
```

**Analogie mémo** : une pile d'assiettes — on ne peut retirer que celle du
dessus. **Dernier entré, premier sorti.**

**Piège classique** : appeler `Pop()`/`Peek()` sur une pile vide lève une
`InvalidOperationException` — toujours vérifier `Count > 0` ou utiliser
`TryPop`/`TryPeek`.

---

## 5. `Queue<T>` — file FIFO (First In, First Out)

Usage : traitement par lots dans l'ordre de réception, files d'attente de
tâches, traitement de messages.

```csharp
var file = new Queue<Virement>();

file.Enqueue(virement);        // ajoute a la fin
var prochain = file.Dequeue(); // retire et retourne le premier (leve si vide)
var premier = file.Peek();     // lit le premier sans le retirer
file.TryDequeue(out var v);    // version securisee
file.Count;
```

**Analogie mémo** : une file d'attente au guichet — le premier arrivé est
le premier servi. **Premier entré, premier sorti.**

**Stack vs Queue — comment ne pas les confondre en entretien** :
- `Stack<T>` = pile d'assiettes → LIFO → `Push`/`Pop`
- `Queue<T>` = file d'attente → FIFO → `Enqueue`/`Dequeue`

---

## 6. LINQ — requêtes sur collections

Usage : filtrage, transformation, agrégation, regroupement, tri.

```csharp
using System.Linq;

// Filtrer
transactions.Where(t => t.Montant > 100);

// Transformer (projeter)
transactions.Select(t => t.Montant);

// Trier
transactions.OrderBy(t => t.Date);
transactions.OrderByDescending(t => t.Montant);

// Limiter
transactions.Take(5);
transactions.Skip(5).Take(5);          // pagination

// Agreger
transactions.Sum(t => t.Montant);
transactions.Average(t => t.Montant);
transactions.Max(t => t.Montant);
transactions.Min(t => t.Montant);
transactions.Count(t => t.Montant > 100);

// Tester
transactions.Any(t => t.Montant > 1000);   // au moins un
transactions.All(t => t.Montant > 0);      // tous

// Regrouper
transactions
    .GroupBy(t => t.CompteId)
    .ToDictionary(g => g.Key, g => g.Sum(t => t.Montant));

// Premier element (ou exception / valeur par defaut)
transactions.First(t => t.Montant > 500);       // leve si aucun match
transactions.FirstOrDefault(t => t.Montant > 500); // null si aucun match

// Convertir en collection concrete
transactions.ToList();
transactions.ToDictionary(t => t.Id);
transactions.ToHashSet();
```

**Piège classique** : LINQ est **lazy** (évaluation différée) sauf pour les
méthodes terminales (`ToList`, `ToDictionary`, `Count`, `Sum`, etc.). Une
requête `Where(...)` non matérialisée se réexécute à chaque énumération.

**Piège classique #2** : `.First()` lève une exception si rien ne correspond
— préférer `.FirstOrDefault()` quand l'absence de résultat est un cas normal.

---

## 7. `decimal` vs `double`/`float` — argent

**Toujours utiliser `decimal` pour manipuler des montants d'argent.**

```csharp
decimal montant = 19.99m;   // suffixe 'm' obligatoire pour un litteral decimal
```

`double`/`float` utilisent une représentation binaire à virgule flottante
qui introduit des erreurs d'arrondi (`0.1 + 0.2 != 0.3` en `double`).
`decimal` est une représentation en base 10, exacte pour les calculs
financiers (au prix d'une performance légèrement inférieure — négligeable
dans la grande majorité des cas).

```csharp
Math.Round(montant, 2);                              // arrondi "banker's rounding" par defaut
Math.Round(montant, 2, MidpointRounding.AwayFromZero); // arrondi classique
```

---

## 8. Exceptions — bonnes pratiques

```csharp
public class SoldeInsuffisantException : Exception
{
    public SoldeInsuffisantException(string message) : base(message) { }
}

// Lever
if (montant <= 0)
    throw new ArgumentException("Le montant doit etre positif.", nameof(montant));

// Capturer
try
{
    compte.Retirer(montant);
}
catch (SoldeInsuffisantException ex)
{
    // traitement specifique
}
catch (Exception ex)
{
    // filet de securite generique (a utiliser avec parcimonie)
}
finally
{
    // toujours execute
}
```

**Règles d'or** :
- Créer une exception dédiée pour une erreur métier récurrente et
  identifiable (`SoldeInsuffisantException` plutôt qu'une `Exception` générique).
- Ne jamais utiliser les exceptions pour un flux de contrôle normal
  (préférer un retour `bool`/`Try...` pattern quand l'échec est *attendu*
  et fréquent, comme `TryGetValue`).
- Toujours valider les entrées **avant** de modifier l'état interne.

---

## 9. Types nullables (`?`) — C# moderne

```csharp
public Operation? Annuler()   // peut retourner null
{
    if (_historique.Count == 0)
        return null;
    return _historique.Pop();
}

// Utilisation cote appelant
var annulee = gestionnaire.Annuler();
if (annulee is not null) { /* ... */ }
Console.WriteLine(annulee?.Montant);          // null-conditional
decimal montant = annulee?.Montant ?? 0m;     // null-coalescing avec valeur par defaut
```

Avec `<Nullable>enable</Nullable>` dans le `.csproj`, le compilateur avertit
si un type référence non-nullable pourrait recevoir `null` — bonne pratique
pour détecter des bugs de `NullReferenceException` à la compilation.

---

## 10. Récapitulatif — quelle structure choisir ?

| Besoin                                          | Structure          |
|--------------------------------------------------|---------------------|
| Liste ordonnée, accès par index, doublons OK      | `List<T>`            |
| Recherche rapide par clé unique                   | `Dictionary<K,V>`    |
| Vérifier une appartenance / éliminer les doublons | `HashSet<T>`         |
| Annuler la dernière action (undo)                 | `Stack<T>` (LIFO)    |
| Traiter dans l'ordre d'arrivée (file d'attente)   | `Queue<T>` (FIFO)    |
| Filtrer / transformer / agréger une collection    | LINQ                 |
| Montants financiers                               | `decimal`             |
