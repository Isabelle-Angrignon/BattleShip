 Communication comm = new Communication();
               nomJoueur = comm.getNomJoueur();
               nomEnemi = comm.getNomEnemi();
               ecrireAuLog("Bonjour" + nomJoueur + " ! ");
               ecrireAuLog("En attente du joueur " + nomEnemi + " pour commencer la partie... ");
               if(comm.CommencerPartie())
               {
                   debutPlacerFlotte();
               }
               else
               {
                   ecrireAuLog("En attente du joueur " + nomEnemi + "pour qu'il place ses bateaux... ");
                   comm.EnvoyerMessage("Pret");
                   debutPlacerFlotte();
               }