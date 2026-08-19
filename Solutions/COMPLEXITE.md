# Complexité et logique attendue, par TODO

Document réservé à l'interviewer. Pour chaque `TODO`, deux versions :

- **Version optimisée** : ce qu'un développeur confirmé devrait produire.
- **Version "débutant"** : une solution correcte mais naïve, plausible chez
  un développeur junior — pas une erreur, juste une complexité ou une
  élégance sous-optimale. Utile pour situer le niveau du candidat sans le
  pénaliser s'il arrive à cette version en premier.

La complexité est donnée en fonction de `n` = nombre d'éléments de la
collection traitée (transactions, virements, opérations...).

## Tableau récapitulatif

| Exercice | TODO | Optimisé | Débutant (naïf) |
|---|---|---|---|
| 1 | Deposer | O(1) | O(1) |
| 1 | Retirer | O(1) | O(1) |
| 1 | ObtenirHistorique | O(n log n) | O(n²) si tri manuel maladroit |
| 2 | TrouverReferencesEnDoublon | O(n) | O(n²) |
| 2 | ToutesLesReferencesSontUniques | O(n) avec sortie anticipée | O(n) sans sortie anticipée, ou O(n²) |
| 3 | Executer | O(1) | O(1) |
| 3 | Annuler | O(1) | O(n) si mauvaise structure |
| 3 | NombreOperationsAnnulables | O(1) | O(n) si comptage manuel |
| 4 | AjouterVirement | O(1) | O(1) |
| 4 | TraiterProchain | O(1) | O(n) si mauvaise structure |
| 4 | TraiterTous | O(n) au total | O(n) au total (mais code dupliqué) |
| 5 | TotalParCompte | O(n) | O(n·k) avec k = nb de comptes distincts |
| 5 | TopTransactions | O(n log n) | O(n²) si tri manuel |
| 5 | MoyenneParCategorie | O(n) | O(n·k) avec k = nb de categories distinctes |

---

## Exercice 1 — Compte Bancaire

### TODO 1 : `Deposer`

**Complexité (les deux versions) : O(1)** — pas de marge d'optimisation ici,
c'est surtout un test de rigueur (validation, ordre des opérations).

**Logique** :
1. Si `montant <= 0`, lever une exception → sortir immédiatement.
2. Ajouter `montant` à `Solde`.
3. Créer une `Transaction` et l'ajouter à l'historique (`List<T>.Add` est
   O(1) amorti).

Il n'y a pas vraiment de version "naïve" différente ici — la seule erreur
possible est algorithmique (modifier le solde avant de valider), pas de
complexité.

### TODO 2 : `Retirer`

**Complexité (les deux versions) : O(1)**

**Logique** :
1. Valider `montant > 0`.
2. Valider `montant <= Solde`, sinon lever `SoldeInsuffisantException`.
3. Soustraire `montant` de `Solde`.
4. Enregistrer la transaction.

Même remarque que `Deposer` : l'enjeu est l'ordre des vérifications, pas la
complexité.

### TODO 3 : `ObtenirHistorique`

**Version optimisée — O(n log n)**
- Filtrer avec `Where(t => t.Date >= depuis)` : O(n).
- Trier avec `OrderByDescending(t => t.Date)` : O(n log n), c'est le terme
  dominant.
- `.ToList()` : O(k) où k = nombre de résultats filtrés.

**Version débutant — O(n²) si mal fait**
- Un développeur junior fera souvent : une boucle `foreach` pour filtrer
  dans une nouvelle `List<Transaction>` (O(n), correct), **puis** un tri
  "maison" en O(n²) (tri à bulles, tri par sélection) au lieu d'utiliser
  `List<T>.Sort()` ou LINQ.
- Une version "junior mais correcte" utiliserait `List<T>.Sort(comparer)`
  qui est en O(n log n) — donc pas de pénalité de complexité, juste moins
  idiomatique que LINQ.

---

## Exercice 2 — Détection de doublons de virements

### TODO 1 : `TrouverReferencesEnDoublon`

**Version optimisée — O(n) temps, O(n) espace**
- Une seule passe sur la liste.
- `Dictionary<string,int>` : incrémenter le compteur de chaque
  `ReferenceId` rencontré.
- Une seconde passe (sur le dictionnaire, taille ≤ n) pour ne garder que
  les clés avec un compteur > 1.

**Version débutant — O(n²) temps**
- Double boucle : pour chaque virement, comparer sa référence à celle de
  *tous les autres* virements de la liste.
- Fonctionne, mais devient très lent si la liste de virements est grande
  (des dizaines de milliers de lignes en traitement batch, par exemple).
- Signal à observer : le candidat a-t-il conscience que c'est O(n²), et
  sait-il proposer l'alternative O(n) si vous lui demandez "et si la liste
  fait 1 million de virements ?"

### TODO 2 : `ToutesLesReferencesSontUniques`

**Version optimisée — O(n) dans le pire cas, souvent moins en pratique**
- Un seul `HashSet<string>`.
- Parcourir la liste, tenter d'ajouter chaque référence.
- Dès qu'un ajout échoue (référence déjà présente), retourner `false`
  immédiatement — **sortie anticipée**, sans lire le reste de la liste.

**Version débutant — deux variantes possibles**
- *Variante A (correcte mais pas optimale)* : appeler
  `TrouverReferencesEnDoublon(virements).Count == 0`. Complexité toujours
  O(n), mais **sans sortie anticipée** : même si le premier virement est un
  doublon du deuxième, la méthode traite quand même les n-2 éléments
  restants pour rien.
- *Variante B (naïve)* : double boucle comme au TODO 1, O(n²).

---

## Exercice 3 — Pile d'annulation d'opérations

### TODO 1 : `Executer`

**Complexité (les deux versions) : O(1)**

**Logique** :
1. Appliquer l'effet de l'opération sur `Solde` selon son `Type`.
2. `Stack<Operation>.Push(operation)` : O(1) amorti.

**Piège débutant (pas de complexité, mais fréquent)** : n'empiler que le
`Montant` (un `decimal`) au lieu de l'objet `Operation` complet — impossible
ensuite de savoir, au moment d'annuler, si c'était un dépôt ou un retrait.

### TODO 2 : `Annuler`

**Version optimisée — O(1)**
- `Stack<Operation>.Pop()` : retire et retourne le sommet en O(1).
- Appliquer l'effet inverse sur `Solde`.

**Version débutant — piège O(n)**
- Si le candidat modélise l'historique avec une `List<Operation>` au lieu
  d'une `Stack<Operation>`, et retire l'élément avec `RemoveAt(0)` en
  pensant prendre "le dernier", il se trompe doublement :
  1. **Bug logique** : `RemoveAt(0)` retire le *premier* élément (FIFO), pas
     le dernier (LIFO) — l'annulation ne cible pas la bonne opération.
  2. **Complexité** : `RemoveAt(0)` sur une `List<T>` est O(n) (tous les
     éléments suivants sont décalés), alors que `RemoveAt(Count - 1)`
     (la bonne version avec une `List`) serait O(1).
- Une `List` utilisée correctement comme pile (`Add` en fin +
  `RemoveAt(Count - 1)`) reste O(1) mais est moins lisible que `Stack<T>`
  qui exprime directement l'intention.

### TODO 3 : `NombreOperationsAnnulables`

**Version optimisée — O(1)**
- `_historique.Count` — propriété déjà maintenue par la `Stack<T>`.

**Version débutant — piège O(n)**
- Boucler sur la pile pour compter ses éléments un par un (`foreach` +
  compteur), ou pire, dépiler puis réempiler chaque élément pour les
  compter — fonctionne mais totalement inutile ici.

---

## Exercice 4 — File d'attente de virements

### TODO 1 : `AjouterVirement`

**Complexité (les deux versions) : O(1)**
- `Queue<Virement>.Enqueue(virement)`.

### TODO 2 : `TraiterProchain`

**Version optimisée — O(1)**
- `Queue<Virement>.Dequeue()` : retire et retourne le premier élément
  entré en O(1).
- Comparer le montant au solde disponible, débiter ou refuser.

**Version débutant — piège O(n)**
- Modéliser la file avec une `List<Virement>` et retirer le premier élément
  via `RemoveAt(0)` : ça fonctionne (le comportement FIFO est correct cette
  fois, contrairement à l'exercice 3), mais chaque appel est O(n) au lieu
  de O(1) car tous les éléments restants sont décalés d'un cran.
- Sur un traitement par lots de plusieurs milliers de virements, ça
  transforme un traitement global O(n) en O(n²).

### TODO 3 : `TraiterTous`

**Version optimisée — O(n) au total**
- Boucler en appelant `TraiterProchain()` jusqu'à ce qu'il retourne `null`,
  en accumulant les résultats. Chaque virement est traité une seule fois en
  O(1), donc O(n) pour l'ensemble.

**Version débutant — même complexité, mais code dupliqué**
- Réécrire toute la logique de traitement (vérification du solde, débit,
  création du `ResultatTraitement`) directement dans une boucle
  `while (file non vide)`, sans appeler `TraiterProchain()`.
- La complexité reste O(n), donc ce n'est pas un problème de performance,
  mais c'est une duplication de logique (violation du principe DRY) — bon
  point de discussion sur la qualité du code plutôt que sur l'algorithmique.
- **Bug fréquent à repérer** : boucler avec `foreach (var v in _enAttente)`
  au lieu de `Dequeue()` répété — ça *lit* la file sans la vider, et modifie
  une collection pendant qu'on l'énumère (`InvalidOperationException` si on
  essaie aussi de la modifier dans la boucle).

---

## Exercice 5 — Reporting bancaire avec LINQ

### TODO 1 : `TotalParCompte`

**Version optimisée — O(n)**
- `GroupBy(t => t.CompteId)` : une seule passe, regroupement par table de
  hachage interne à LINQ.
- `.ToDictionary(g => g.Key, g => g.Sum(t => t.Montant))` : pour chaque
  groupe (déjà constitué), sommer ses éléments — au total, chaque
  transaction n'est visitée qu'une fois.

**Version débutant — O(n·k)**, où k = nombre de comptes distincts
- Récupérer la liste des `CompteId` distincts (`Distinct()`), puis pour
  chaque compte, refaire un `Where(t => t.CompteId == compte).Sum(...)`
  sur **toute** la liste de transactions.
- Fonctionne, mais chaque compte déclenche un nouveau parcours complet de
  la liste — coûteux si le nombre de comptes est grand.

### TODO 2 : `TopTransactions`

**Version optimisée (pragmatique) — O(n log n)**
- `OrderByDescending(t => t.Montant).Take(n)`.
- C'est la version attendue en entretien "rapide" — un tri complet suivi
  d'une troncature.

**Version débutant — O(n²)**
- Boucle manuelle : n passes successives cherchant à chaque fois le max
  restant (équivalent d'un tri par sélection), ou un tri à bulles maison.

**Pour aller plus loin (bonus, à mentionner seulement si le candidat est
très à l'aise)** : si `n` est énorme et qu'on ne veut que les `k` plus
grosses transactions avec `k` petit, la vraie solution optimale est
O(n log k) avec un tas (`PriorityQueue<TElement,TPriority>` en .NET) de
taille bornée à `k`, plutôt qu'un tri complet de la collection. Hors de
portée d'un exercice "rapide", mais excellent signal si le candidat le
propose spontanément.

### TODO 3 : `MoyenneParCategorie`

**Version optimisée — O(n)**
- `GroupBy(t => t.Categorie)` puis `Average(t => t.Montant)` par groupe :
  chaque transaction visitée une seule fois au total.
- `Math.Round(..., 2)` sur le résultat.

**Version débutant — O(n·k)**, où k = nombre de catégories distinctes
- Même schéma que `TotalParCompte` : un `Where` + `Average` séparé par
  catégorie, en reparcourant toute la liste à chaque fois.

---

## Comment s'en servir en entretien

- Si le candidat produit directement la version optimisée sans y penser
  consciemment : très bon signal, creusez plutôt sur le *pourquoi* ("vous
  auriez pu faire une double boucle, pourquoi ne pas l'avoir fait ?").
- Si le candidat produit la version "débutant" et qu'elle passe les tests :
  ce n'est **pas un échec**, l'exercice est réussi. Posez la question de la
  complexité après coup ("quelle est la complexité de votre solution ? Y
  a-t-il plus efficace ?") pour voir s'il peut l'analyser et l'améliorer
  sur demande — c'est souvent plus révélateur que d'exiger l'optimal du
  premier coup.
- Les pièges "O(n) qui devient O(n²) à cause d'une mauvaise structure"
  (exercices 3 et 4, `RemoveAt(0)` sur une `List`) sont particulièrement
  bons pour distinguer un candidat qui connaît vraiment le coût des
  opérations sur `List<T>` d'un candidat qui code au hasard jusqu'à ce que
  les tests passent.
