# Indices progressifs (à donner par palier)

Ce document est réservé à l'interviewer. L'objectif de chaque exercice est
d'observer le **raisonnement** du candidat (comment il modélise le problème,
quelle structure il choisit et pourquoi) plutôt que sa capacité à se
souvenir de la syntaxe exacte.

## Comment utiliser ce document

Pour chaque `TODO`, 3 niveaux d'indices sont fournis :

- **Niveau 1 — Question de relance** : une question ouverte, à poser si le
  candidat semble bloqué ou part dans une mauvaise direction. Ne donne
  aucune réponse, oriente juste la réflexion.
- **Niveau 2 — Indice conceptuel** : pointe vers le bon concept ou la bonne
  structure de données, sans donner l'implémentation.
- **Niveau 3 — Indice concret** : décrit l'algorithme en langage naturel,
  sans code. À utiliser seulement si le candidat reste bloqué après le
  niveau 2 — c'est le dernier filet avant de simplement montrer la solution.

**Ne donnez jamais un niveau avant d'avoir laissé le candidat chercher.** Un
candidat qui trouve seul avec le niveau 1 démontre bien plus qu'un candidat
qui a besoin du niveau 3 dès le départ — notez le niveau d'indice utilisé
pour chaque TODO, c'est en soi une donnée d'évaluation utile.

---

## Exercice 1 — Compte Bancaire

### TODO 1 : `Deposer`

1. Qu'est-ce qui doit se passer si quelqu'un essaie de déposer -50 € ou 0 € ?
   Est-ce un cas normal à gérer silencieusement, ou une erreur à signaler ?
2. Un montant invalide n'est pas un échec métier comme "solde insuffisant",
   c'est une mauvaise utilisation de la méthode par l'appelant. Quel
   mécanisme du langage sert justement à signaler ça, plutôt qu'un simple
   `return false` ?
3. Levez une exception standard adaptée à un argument invalide *avant* de
   toucher au solde. Une fois le montant validé, ajoutez-le au solde puis
   enregistrez la transaction dans l'historique.

### TODO 2 : `Retirer`

1. De combien de façons différentes un retrait peut-il échouer ? Sont-elles
   de la même nature d'erreur ?
2. Vous avez déjà une exception générique pour un montant invalide. Pour
   "il n'y a pas assez d'argent sur le compte", est-ce la même erreur, ou
   quelque chose de plus spécifique au métier bancaire mériterait sa propre
   exception ?
3. Vérifiez d'abord que le montant est valide, puis que le solde est
   suffisant, *avant* de modifier quoi que ce soit — sinon vous risquez de
   devoir annuler une modification déjà faite si une vérification échoue
   après coup.

### TODO 3 : `ObtenirHistorique`

1. Si on demande l'historique "depuis hier", veut-on voir les transactions
   les plus anciennes ou les plus récentes en premier dans le résultat ?
2. Il y a deux opérations à faire sur la collection : garder seulement ce
   qui correspond à la date, et présenter le résultat dans un certain
   ordre. Le langage a-t-il un outil pensé pour enchaîner ce genre
   d'opérations sur une collection ?
3. Filtrez les transactions dont la date est postérieure ou égale à la date
   donnée, triez le résultat par date décroissante, puis matérialisez-le en
   liste.

---

## Exercice 2 — Détection de doublons de virements

### TODO 1 : `TrouverReferencesEnDoublon`

1. Si vous deviez repérer les doublons à la main, sur une longue liste
   papier, quelle information devriez-vous noter au fur et à mesure de
   votre lecture ?
2. Vous avez besoin de savoir, pour chaque référence, *combien de fois*
   elle apparaît. Quelle structure associe naturellement une clé (la
   référence) à une valeur (un compteur) ?
3. Parcourez la liste une seule fois en incrémentant un compteur par
   référence dans un dictionnaire, puis ne gardez que les clés dont le
   compteur dépasse 1.

### TODO 2 : `ToutesLesReferencesSontUniques`

1. Ici, la question posée est différente de la précédente : "y a-t-il *au
   moins* un doublon ?" plutôt que "quels sont *tous* les doublons ?".
   Avez-vous vraiment besoin de compter toutes les occurrences pour
   répondre à ça ?
2. Pourriez-vous vous arrêter dès la première fois que vous retombez sur
   une référence déjà rencontrée ? Quelle structure permet de tester
   rapidement "ai-je déjà vu cet élément ?"
3. Parcourez la liste en ajoutant chaque référence à un ensemble : si
   l'ajout échoue parce que l'élément y est déjà, c'est qu'il y a un
   doublon — vous pouvez retourner `false` immédiatement sans lire le
   reste de la liste.

---

## Exercice 3 — Pile d'annulation d'opérations

### TODO 1 : `Executer`

1. Pour pouvoir annuler une opération plus tard, qu'est-ce qu'il faut avoir
   gardé en mémoire au moment où elle est exécutée ?
2. Deux choses doivent se produire : le solde change, et l'opération est
   mémorisée quelque part pour une éventuelle annulation. Faut-il stocker
   juste le montant, ou l'opération complète (avec son type) ? Pourquoi ?
3. Appliquez l'effet de l'opération sur le solde selon son type, puis
   empilez l'opération elle-même (pas seulement son montant) au sommet de
   la structure d'historique.

### TODO 2 : `Annuler`

1. Si la dernière opération était un dépôt de 50 €, que doit concrètement
   faire "l'annuler" sur le solde ?
2. Quelle opération sur une pile retire l'élément du sommet et vous le
   redonne ? Est-ce suffisant de simplement le "regarder" (`Peek`), ou
   faut-il aussi le retirer pour être sûr de ne pas l'annuler deux fois ?
3. Retirez l'opération du sommet de la pile, puis appliquez sur le solde
   l'effet strictement inverse de celui appliqué lors de son exécution
   (un dépôt annulé se comporte comme un retrait, et inversement).

### TODO 3 : `NombreOperationsAnnulables`

1. Cette information n'existe-t-elle pas déjà, sous une forme ou une
   autre, dans la structure que vous utilisez pour stocker l'historique ?
2. Pas besoin d'ajouter un compteur séparé à maintenir à la main — une
   propriété déjà existante de votre pile répond directement à la
   question.

---

## Exercice 4 — File d'attente de virements

### TODO 1 : `AjouterVirement`

1. Un nouveau virement doit-il être traité avant ou après ceux déjà en
   attente ?
2. Placez-le à l'endroit de la structure qui correspond à "vient d'arriver,
   sera traité en dernier parmi ceux présents".

### TODO 2 : `TraiterProchain`

1. Parmi les virements en attente, lequel doit être traité en premier : le
   plus ancien ou le plus récent ? Qu'est-ce que ça implique sur l'ordre de
   lecture de la structure ?
2. Avant de retirer un virement de la file, avez-vous déjà vérifié s'il
   peut réellement être payé ? Et si le paiement échoue *après* l'avoir
   retiré, doit-il repartir dans la file, ou est-ce définitivement terminé
   pour lui ?
3. Retirez le prochain virement de la file, puis comparez son montant au
   solde disponible : s'il est suffisant, débitez le solde et renvoyez un
   résultat de succès ; sinon, renvoyez un résultat d'échec sans modifier
   le solde et sans remettre le virement en attente.

### TODO 3 : `TraiterTous`

1. Vous avez déjà une méthode qui sait traiter "le prochain" virement.
   Comment vous en servir pour traiter "tous" les virements sans réécrire
   la même logique ?
2. Quelle est la condition naturelle pour arrêter de boucler ? Que retourne
   votre méthode existante quand il n'y a plus rien à traiter ?
3. Appelez la méthode de traitement unitaire en boucle, en accumulant
   chaque résultat dans une liste, jusqu'à ce qu'elle retourne l'absence de
   résultat (file vide).

---

## Exercice 5 — Reporting bancaire avec LINQ

### TODO 1 : `TotalParCompte`

1. Si vous deviez faire ça à la main sur une feuille de calcul, quelle
   serait votre première opération : trier les lignes, les filtrer, ou les
   rassembler par paquets ?
2. Vous voulez un résultat "par compte" — quelle opération sur une
   collection permet de rassembler des éléments selon une clé commune
   avant de les agréger ?
3. Regroupez les transactions par identifiant de compte, sommez le montant
   à l'intérieur de chaque groupe, puis transformez le résultat en
   dictionnaire clé/valeur.

### TODO 2 : `TopTransactions`

1. Pour trouver les transactions les plus importantes, faut-il d'abord
   trier la liste ou d'abord la réduire ?
2. Comment trier une collection par montant du plus grand au plus petit, et
   comment ne garder que les `n` premiers résultats une fois triés ?
3. Triez par montant décroissant, puis ne conservez que les `n` premiers
   éléments du résultat trié.

### TODO 3 : `MoyenneParCategorie`

1. Ce TODO ressemble beaucoup au premier — qu'est-ce qui change entre "un
   total par compte" et "une moyenne par catégorie" ?
2. Après avoir regroupé par catégorie, quelle opération d'agrégation donne
   une moyenne plutôt qu'une somme ? Et le résultat d'une division a-t-il
   besoin d'être nettoyé avant d'être présenté ?
3. Regroupez par catégorie, calculez la moyenne des montants dans chaque
   groupe, puis arrondissez le résultat à 2 décimales avant de le mettre
   dans le dictionnaire final.

---

## Grille d'observation transversale

Au-delà de la solution trouvée, ces questions permettent de juger la
qualité du raisonnement, indépendamment du niveau d'indice utilisé :

- **Le candidat nomme-t-il la structure de données avant de coder ?**
  ("Je vais utiliser une pile parce que..." vs. coder au hasard jusqu'à ce
  que ça marche).
- **Anticipe-t-il les cas limites de lui-même** (liste vide, solde exact,
  montant nul) ou seulement après un test en échec ?
- **Sait-il justifier le choix d'une structure plutôt qu'une autre**
  (ex: pourquoi un `HashSet` plutôt qu'un `List.Contains` ici) ?
- **Repère-t-il une occasion de réutiliser du code déjà écrit** (comme
  `TraiterTous` qui devrait s'appuyer sur `TraiterProchain`) sans qu'on ait
  à le lui souffler ?
