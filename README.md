# kata-LegacyTrain (TrainTrain)
Kata on how to refactor a typical legacy code base (made by Thomas PIERRAIN & Bruno BOUCARD, directly inspired by [Emily Bache's kata](https://github.com/emilybache/KataTrainReservation) ).

## Contexte général
SSII a gagné un appel d'offre pour mise en oeuvre rapide d'un logiciel de réservation de sièges dans les trains.
Après avoir développé une première version de l'appli, la SSII a continuée a faire evoluer le système jusqu'à arriver à une situation de blocage: le client demande une modification de l'algoithme de reservation ce qui semble impossible à la SSII (qui plus est, à perdu entre-temps tous ses développeurs partis faire autre chose de plus intéressant). La SSII a depuis jetée l'éponge en produisant un avenant/devis hors de prix pour le client qui nous sollicite pour "reprendre le dossier".

Nous arrivons donc sur une code base assez moche, pour laquelle nous n'avons plus aucun développeur pour nous expliquer leurs intentions initiales et justifier de leurs choix. 

## Description de l'architecture et des APIs exernes impliquées

     Notre API TrainTrain permet de reserver des places dans les trains

Pour y arriver, notre jeune startup exploite 2 APIs de l'opérateur national et historique HassanCehef. 

Ces 2 APIs de la HassanCehef sont :

### L'API TrainDataService 
- retourne le détail et la composition des trains à partir de leur identifiant de train
- permet de booker/reserver un ensemble de sièges sur un train en précisant l'identifiant du train, les identifiants/noms des sièges sollicités et un booking reference valide récupéré auprès de l'API __BookingReferenceService__

### L'API BookingReferenceService
- Permet de récupérer un identifiant unique de réservation juste avant d'appeler le TrainDataService pour effectuer une reservation (on a externalisé le BookingReferenceService pour répondre à une contrainte réglementaire Européene, mais certain·e·s disent que c'est plutôt à cause de la loi de Conway chez HassanCehef...)

---

## Instructions

On vient de rajouter 3 tests d'acceptation sur les règles métiers évoquées.
Should:
   - Reserve_seats_when_available() - __OK__
   - Not_reserve_seats_when_it_exceed_max_capacity_threshold() (on ne peut pas réserver plus de 70% du train) - __OK__
   - Reserve_all_seats_in_the_same_coach() - __KO! Stupeur: la règle du non chevauchement entre voiture pour une même réservation n'est pas implémentée !___

Le client n'en croit pas ses yeux. Et on vient de proposer au client d'implémenter correctement cette vieille règle en même temps que la nouvelle feature. Il est d'accord.

## Objectifs

1. Fixer le test d'acceptation qui échoue ("Should Reserve_all_seats_in_the_same_coach()")
2. Implémenter la nouvelle feature ("*Dans l'idéal, ne pas charger les voitures du train à plus de 70% de leur capacité*" (car les 30% restants sont pour les salariés de la HassanCehef)
3. Transformer l'API pour implémenter une architecture hexagonale

Note : il n'est pas interdit d'améliorer/refactorer le code en passant...
