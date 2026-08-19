# Exercice 1 — Compte Bancaire

**Structures/notions visées :** classes, encapsulation, exceptions, `List<T>`
**Durée indicative :** 10-12 min

## Contexte

Vous devez compléter l'implémentation d'un `CompteBancaire` simple : dépôt,
retrait, et consultation de l'historique des transactions.

## Consignes

1. Ouvrez `01-CompteBancaire.csproj` dans Visual Studio (Fichier → Ouvrir → Projet/Solution).
2. Complétez les 3 `TODO` dans `CompteBancaire.cs` :
   - `Deposer` : ajoute le montant au solde et enregistre une `Transaction`.
   - `Retirer` : retire le montant du solde (avec gestion du solde insuffisant).
   - `ObtenirHistorique` : filtre et trie l'historique par date.
3. Lancez le projet (`F5` ou `Ctrl+F5`). `Program.cs` exécute une série de
   tests automatiques et affiche `OK` / `ECHEC` pour chacun.
4. L'objectif est d'obtenir `6/6 tests réussis`.

## Ce qui est évalué

- Validation des entrées (`ArgumentException`)
- Exception personnalisée (`SoldeInsuffisantException`)
- Manipulation d'une `List<T>` (ajout, filtrage, tri)
- Lisibilité et structure du code
