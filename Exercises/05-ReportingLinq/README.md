# Exercice 5 — Reporting bancaire avec LINQ

**Structures/notions visées :** LINQ (`GroupBy`, `OrderBy`, `Sum`, `Average`), `Dictionary<K,V>`
**Durée indicative :** 10-12 min

## Contexte

La banque veut générer un rapport simple à partir d'une liste de transactions
multi-comptes : totaux par compte, plus grosses transactions, moyenne par
catégorie de dépense.

## Consignes

1. Ouvrez `05-ReportingLinq.csproj` dans Visual Studio.
2. Complétez les 3 `TODO` dans `RapportBancaire.cs` :
   - `TotalParCompte` : somme des montants groupée par `CompteId`.
   - `TopTransactions` : les `n` transactions les plus élevées, triées.
   - `MoyenneParCategorie` : montant moyen par `Categorie`, arrondi à 2 décimales.
3. Lancez le projet (`F5`). Objectif : `5/5 tests réussis`.

## Ce qui est évalué

- Maîtrise de LINQ (`GroupBy`, `Select`, `OrderByDescending`, `Take`, `Average`)
- Conversion `IEnumerable` → `Dictionary` / `List`
- Gestion des arrondis avec `decimal` et `Math.Round`
