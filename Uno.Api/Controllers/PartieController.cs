using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Uno.Api.Hubs;
using Uno.Api.Repository;
using Uno.Interfaces;

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
        public IActionResult RejoindrePartie(Utilisateur user)
        {
            partie.JoueurAjoute += async(joueur) =>
            {
                if (joueur.Nom == user.Nom)
                {
                    repository.AjouterUtilisateur(user);
                    await context.Clients.Client(user.Id).InvokeAsync("partieRejointe", partie.Joueurs.Count);
                }
            };
            partie.AjouterJoueur(new ValueObjects.Joueur(user.Nom));
            return Ok();
        }

        [HttpPost("Commencer")]
        public IActionResult CommencerPartie()
        {
            return Ok();
        }
    }
}