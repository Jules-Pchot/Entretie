# Entretie — Exercices de live coding C# / .NET (contexte bancaire)

Ce dépôt contient 5 exercices rapides de live coding en C# / .NET destinés à
évaluer les compétences d'un candidat sur le langage et le maniement des
structures de données du BCL (`List`, `Dictionary`, `HashSet`, `Stack`,
`Queue`, LINQ), dans un contexte métier bancaire.

## Structure du dépôt

```
Entretie/
├── Exercices.sln                  # Solution VS regroupant les 5 exercices (usage interne / relecture)
├── CheatSheet_CSharp.md           # Fiche de révision sur les structures de données C#
├── Exercises/                     # <-- Ce que le candidat doit recevoir/cloner
│   ├── 01-CompteBancaire/         # List<T>, OOP, exceptions
│   ├── 02-DoublonsVirements/      # HashSet<T>, Dictionary<K,V>
│   ├── 03-PileAnnulation/         # Stack<T> (LIFO)
│   ├── 04-FileVirements/          # Queue<T> (FIFO)
│   └── 05-ReportingLinq/          # LINQ, Dictionary<K,V>
└── Solutions/                     # <-- Reserve a l'interviewer, NE PAS partager avec le candidat
    ├── Solutions.sln
    ├── EXPLICATIONS.md            # Explication detaillee de chaque solution
    └── 01-.../ 02-.../ ...        # Implementations completes, memes tests que Exercises/
```

Chaque exercice est un **projet console .NET 8 autonome** (son propre
`.csproj`), sans dépendance externe (pas de package NuGet à restaurer). Le
candidat n'a qu'à ouvrir le `.csproj` dans Visual Studio, compléter le code,
et lancer `F5` : un petit harnais de tests intégré dans `Program.cs` affiche
`OK` / `ECHEC` pour chaque cas testé, sans besoin d'installer de framework de
test.

## Comment faire passer l'entretien

### 1. Préparer l'accès du candidat

Le dossier `Exercises/` est ce que le candidat doit récupérer — **pas**
`Solutions/`. Deux options :

- **Option simple** : donner l'accès en lecture au dépôt complet si vous
  êtes présent pendant tout l'entretien (vous surveillez qu'il ne consulte
  pas `Solutions/`).
- **Option propre** : créer un dépôt séparé (ou une branche allégée) ne
  contenant que le dossier `Exercises/`, à partager avec le candidat. Par
  exemple :
  ```bash
  git clone --no-checkout https://github.com/Jules-Pchot/Entretie.git entretie-candidat
  cd entretie-candidat
  git sparse-checkout init --cone
  git sparse-checkout set Exercises
  git checkout claude/csharp-banking-interview-exercises-70ekzc
  ```
  Le candidat se retrouve avec uniquement le dossier `Exercises/` sur sa machine.

### 2. Déroulé suggéré (~60-70 min)

1. Présentez le contexte : "Vous travaillez sur des outils internes pour
   une banque, voici 5 petits exercices indépendants."
2. Pour chaque exercice (dans l'ordre ou selon le temps disponible) :
   - Le candidat ouvre le `.csproj` correspondant dans Visual Studio.
   - Il lit le `README.md` du dossier (contexte + consignes).
   - Il complète les `TODO` dans le fichier de logique métier.
   - Il lance `F5` pour vérifier ses résultats via les tests automatiques.
   - Discussion : demandez-lui d'expliquer ses choix (complexité,
     structures de données utilisées, pourquoi telle exception, etc.)
3. Comptez 8-12 minutes par exercice. S'il bloque longtemps sur un exercice,
   passez au suivant — l'objectif est de couvrir plusieurs structures de
   données, pas de terminer les 5.

### 3. Après l'entretien

Consultez `Solutions/EXPLICATIONS.md` pour :
- Comparer l'approche du candidat à la solution de référence.
- Identifier les pièges qu'il a évités (ou non).
- Utiliser la grille d'évaluation rapide en fin de document.

Le fichier `CheatSheet_CSharp.md` peut être transmis **après** l'entretien
au candidat (recruté ou non) comme support de révision sur les structures de
données C#.

## Résumé des 5 exercices

| # | Nom                       | Structure(s) clé(s)          | Contexte métier |
|---|----------------------------|-------------------------------|------------------|
| 1 | Compte Bancaire            | `List<T>`, exceptions          | Dépôt/retrait avec historique de transactions |
| 2 | Doublons de virements      | `HashSet<T>`, `Dictionary<K,V>`| Détecter des virements soumis deux fois |
| 3 | Pile d'annulation          | `Stack<T>` (LIFO)              | Fonction "Undo" sur les opérations d'un distributeur |
| 4 | File de virements          | `Queue<T>` (FIFO)              | Traitement par lots des virements en attente |
| 5 | Reporting avec LINQ        | LINQ, `Dictionary<K,V>`        | Agrégations et statistiques multi-comptes |

## Prérequis techniques

- Visual Studio 2022 (ou VS Code + extension C#) avec le SDK **.NET 8**.
- Aucun package NuGet supplémentaire n'est nécessaire.
