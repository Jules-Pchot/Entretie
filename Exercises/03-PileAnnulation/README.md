# Exercice 3 — Pile d'annulation d'opérations (Undo)

**Structures/notions visées :** `Stack<T>`, LIFO, types nullables (`?`)
**Durée indicative :** 10 min

## Contexte

Un distributeur doit pouvoir annuler la dernière opération effectuée
(fonctionnalité "Undo"), comme dans un éditeur de texte.

## Consignes

1. Ouvrez `03-PileAnnulation.csproj` dans Visual Studio.
2. Complétez les 3 `TODO` dans `GestionnaireOperations.cs` :
   - `Executer` : applique l'opération au solde et l'empile.
   - `Annuler` : dépile la dernière opération et inverse son effet sur le solde.
   - `NombreOperationsAnnulables` : nombre d'opérations encore dans la pile.
3. Lancez le projet (`F5`). Objectif : `7/7 tests réussis`.

## Ce qui est évalué

- Compréhension du fonctionnement LIFO d'une `Stack<T>` (`Push`/`Pop`/`Peek`)
- Gestion du cas limite (pile vide → retour `null`)
- Raisonnement sur l'opération inverse (dépôt ↔ retrait)
