using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Uno.Api.Hubs;
using Uno.Api.Repository;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno.Api.Controllers
{
    [Route("api/[controller]")]
    public class PartieController : Controller
    {
        private readonly IPartie partie;
        private readonly IUserRepository repository;
        private readonly IHubContext<UnoHub> context;

        public PartieController(IHubContext<UnoHub> context, IPartie partie, IUserRepository repository)
        {
            this.context = context;
            this.repository = repository;
            this.partie = partie;
        }

        [HttpGet]
        public IActionResult EtatPartie()
        {
            return Ok(new
            {
                NombreJoueurs = partie.Joueurs.Count
            });
        }

        [HttpPost("Rejoindre")]
        public IActionResult RejoindrePartie(string user)
        {
            partie.JoueurAjoute += async(joueur) =>
            {
                if (joueur.Nom == user)
                {
                    await context.Clients.All.InvokeAsync("utilisateurAjoute", user);
                }
            };
            partie.AjouterJoueur(new Joueur(user));
            return Ok();
        }

        [HttpPost("Commencer")]
        public IActionResult CommencerPartie()
        {
            partie.PartieCommencee += (joueurs) =>
            {
                var listeMains = InitialiserMains(joueurs);
                var envoiTaches = listeMains.Select(_ => new Task(async() =>
                {
                    await context.Clients.Client(_.Key).InvokeAsync("partieInitialisee", _.Value);
                })).ToArray();
                Task.WaitAll(envoiTaches);
            };
            partie.CommencerPartie();
            return Ok();
        }

        private IDictionary<string, List<Carte>> InitialiserMains(IEnumerable<Joueur> joueurs)
        {
            var mainsJoueurs = from joueur in joueurs
            let connexionJoueur = repository.GetUtilisateurIdByName(joueur.Nom)
            select new { Key = connexionJoueur, Main = joueur.Main };
            return mainsJoueurs.ToDictionary(_ => _.Key, _ => _.Main);
        }
    }
}