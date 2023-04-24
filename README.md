# kata-LegacyTrain (TrainTrain)
*Le kata TrainTrain est un kata écrit par Thomas PIERRAIN et Bruno BOUCARD suite à une inspiration du kata d’Emily BACHE ([Train reservation](https://github.com/emilybache/KataTrainReservation) )
*(Sauf que dans TrainTrain, on a écrit plus de code, pour rendre plus savoureux le côté Legacy)*

# Contexte

Nous sommes une petite équipe de dev et nous arrivons sur une code-base assez moche -mais qui est en prod- et pour laquelle nous n'avons plus aucun dev pour nous expliquer leurs intentions initiales et justifier de leurs choix. On va devoir se débrouiller seuls pour rajouter une fonctionnalité manquante.

Le système s’appelle TrainTrain, et 

 `Notre API TrainTrain permet de reserver des places dans les trains`

Pour faire son travail, celle-ci attends un identifiant de train (qui correspond dans les faits à un voyage) et un nombre de places souhaitées. Elle retournera un JSON qui contient les noms des sièges réservés ainsi qu’un identifiant de réservation/booking qui est unique.

# Les règles métiers

Notre API est censée implémenter quelques règles métiers :

1. On ne peux réserver que des sièges qui ne sont pas déjà réservés
2. On ne doit pas remplir les trains à plus de 70% de leur capacité totale (on doit en effet laisser les 30% restants aux salariés de HassanCehef, l’opérateur officiel et historique, car eux aussi on leur propre système de réservation de billets de train)
3. On ne doit pas séparer les couples et les familles en les plaçant dans des voitures (coaches) différentes
4. **La règles métier qui manque et qu’il va falloir ajouter est la suivante :** On ne doit pas remplir chaque voiture/coach à plus de 70% de leur capacité (pour permettre aux clients directs de l’opérateur officiel et historique de pouvoir réserver ces 30% restants)

# Les tierces parties utilisées

Pour faire sont travail, notre jeune startup exploite 2 APIs de l'opérateur national et historique HassanCehef.

Ces 2 APIs de la HassanCehef sont :

### L'API TrainDataService

- **retourne le détail et la composition des trains à partir de leur identifiant de train**
- **permet de booker/reserver un ensemble de sièges sur un train** en
précisant l'identifiant du train, les identifiants/noms des sièges
sollicités et un booking reference valide récupéré auprès de l'API **BookingReferenceService**

### L'API BookingReferenceService

- Permet de **récupérer un identifiant unique de réservation juste avant** d'appeler le TrainDataService pour effectuer une reservation (on a
externalisé le BookingReferenceService pour répondre à une contrainte
réglementaire Européene, mais certain·e·s disent que c'est plutôt à
cause de la loi de Conway chez HassanCehef...)

# Objectifs

On nous a demandé de rajouter une nouvelle fonctionnalité (la règle métier #4: “*On ne doit pas remplir chaque voiture/coach à plus de 70% de leur capacité”*) 

mais…

En rajoutant quelques tests d’acceptation sur la route, on s’est rendu-compte que la règle #3: “*On ne doit pas séparer les couples et les familles en les plaçant dans des voitures (coaches) différentes”*

n’était tout simplement pas implémentée… 

C'est là où on en est dans la code base. 

---

On te propose donc nous faire une proposition. Par quoi commencerais-tu ? :

- implémenter la fonctionnalité (#3) qu’on est censé avoir et qui ne fonctionne visiblement pas ? (i.e.: de ne pas séparer les familles)
- implémenter la nouvelle fonctionnalité qu’on nous a demandé de rajouter (#4)
- autre chose ?

Letsgo!
