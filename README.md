# kata-LegacyTrain
Kata on how to refactor a typical legacy code base (directly inspired by [Emily Bache's kata](https://github.com/emilybache/KataTrainReservation).

Too many projects have layered-based...

## Contexte: 
SSII a gagné un appel d'offre pour mise en oeuvre rapide d'un logiciel de réservation de sièges dans les trains.
Après avoir développé une première verison de l'appli, la SSII a continuée a faire evoluer le système jusqu'à arriver à une situation de blocage: le client demande une modification de l'algoithme de reservation ce qui semble impossible à la SSII (qui plus est, à perdu entre-temps tous ses développeurs partis faire autre chose de plus intéressant).
La SSII a jetée l'éponge en produisant un avenant/devis hors de prix pour le client qui nous sollicite pour "reprendre le dossier".

Nous arrivons donc sur une code base assez moche, pour laquelle nous n'avons plus de développeurs pour nous expliquer leurs intentions initiales et justifier de leurs choix. Heureusement pour nous, le client mets à notre disposition un expert du domaine pendant 3 heures pour répondre à nos questions.

On est assez inquiet par la difficulté potentielle, mais comme on est joeur on a accepté cette mission mais on compte sur vous pour nous aider.

La nouvelle feature est de supporter une autre façon de reserver des places de trains pour un autre grand distributeur qui n'aime pas du tout notre format JSON de retour => on va donc lui exposer un nouveau entry point pour supporter son besoin.
TrainTrainCorp se rends compte qu'avec l'arrivée de ce clients et les volumes énormes corrspondant attendus => mettre à jour l'algo de réservation en introduisant une nouvelle règle : "Dans l'idéal, ne pas charger les voitures du train à plus de 70% de leur capacité."

Couplet sur le contexte organisationel : 
 - On a externalisé le TrainDataService pour des questions de scalabilité
 - On a externalisé le BookingReferenceService pour répondre à une contrainte réglementaire Européene (trouver un truc rigolo et absurde).

## Description de l'architecture
On a récupéré le diagramme suivant qui a l'air d'etre à jour et qu'on vous commente.


Montrer dessin.


## Etapes

1. On rajoute des tests d'acceptance sur les uses cases intiaux (Pas plus de 70% du train et pas de reservation a cheval sur 2 voitures)
	- Surprise : la deuxieme regle (non chevauchement) n'est pas implémentée... Discussion avec l'expert qui n'en croit pas ses yeux.
	- On propose au client d'implémenter cette règle en même temps que la nouvelle feature (la règle sera valable quelque soit les modalités de réservation (historique et nouveau).

2. On rajoute 1 test d'acceptance sur la nouvelle feature -> nouveau format de retour sur scenarii avec moins de 70% de charge par voiture

3. On fait émerger le concept de Coach qui n'existait pas dans le code existant
4. On sépare bien le code du domaine (plus du tout anémique) avec le code technique
5. On extrait le format de sérialization du domaine métier en le situant dans des adapteurs à la périphérie du système (archi hexagonale FTW)

En route : 
 - on se sera débarrassé le l'état des trains persistés/cachés dans le service TrainTrain (à tord, car d'autres gens peuvent modifier les reservations des trains via le TrainData service)
 - on se sera débarrassé de l'ORM
 

 Suite à notre refactoring, on peut commencer à avoir des discussions intéressantes avec le métier sur un autre problème qui le taraude: la modification de la topologies des trains à postériori qui était problématique avant a cause du caching des trains du service TrainTrain.



T-Shirts:
 - le client à toujours raison (bleu acier)
 - l'équipe du Train-Train (rose pétant)



 sklmk
