# Exercice 2 — Détection de doublons de virements

**Structures/notions visées :** `HashSet<T>`, `Dictionary<K,V>`, algorithmique de base
**Durée indicative :** 8-10 min

## Contexte

Un bug dans le service de virements soumet parfois deux fois le même virement
(même `ReferenceId`). Vous devez écrire un détecteur de doublons.

## Consignes

1. Ouvrez `02-DoublonsVirements.csproj` dans Visual Studio.
2. Complétez les 2 `TODO` dans `DetecteurDoublons.cs` :
   - `TrouverReferencesEnDoublon` : renvoie les `ReferenceId` apparaissant plus d'une fois.
   - `ToutesLesReferencesSontUniques` : renvoie `false` dès le premier doublon trouvé.
3. Lancez le projet (`F5`). Objectif : `5/5 tests réussis`.

## Ce qui est évalué

- Choix de la bonne structure de données (`HashSet` vs `Dictionary`)
- Complexité algorithmique (éviter les boucles imbriquées en O(n²))
- Capacité à distinguer deux besoins proches (lister tous les doublons vs.
  détection rapide avec sortie anticipée)
