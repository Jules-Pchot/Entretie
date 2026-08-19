# Exercice 4 — File d'attente de virements (FIFO)

**Structures/notions visées :** `Queue<T>`, FIFO, types nullables (`?`)
**Durée indicative :** 10-12 min

## Contexte

Les virements soumis par les clients doivent être traités par lots, dans
l'ordre où ils ont été reçus (le premier soumis est le premier traité).

## Consignes

1. Ouvrez `04-FileVirements.csproj` dans Visual Studio.
2. Complétez les 3 `TODO` dans `FileAttenteVirements.cs` :
   - `AjouterVirement` : ajoute un virement en fin de file.
   - `TraiterProchain` : traite le virement le plus ancien (débit du solde ou refus).
   - `TraiterTous` : traite tous les virements en attente, dans l'ordre FIFO.
3. Lancez le projet (`F5`). Objectif : `7/7 tests réussis`.

## Ce qui est évalué

- Compréhension du fonctionnement FIFO d'une `Queue<T>` (`Enqueue`/`Dequeue`)
- Gestion des cas d'échec (solde insuffisant) sans corrompre l'état
- Réutilisation de code (`TraiterTous` doit s'appuyer sur `TraiterProchain`)
