using System.Collections.Concurrent;

namespace Uno.Api.Repository
{
    public class Utilisateur
    {
        public string Id { get; set; }

        public string Nom { get; set; }
    }

    public interface IUserRepository
    {
        Utilisateur GetUtilisateurById(string id);

         void AjouterUtilisateur(Utilisateur user);
    }

    public class MemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, Utilisateur> utilisateurs;

        public MemoryUserRepository()
        {
            utilisateurs = new ConcurrentDictionary<string, Utilisateur>();
        }

        public void AjouterUtilisateur(Utilisateur user)
        {
            utilisateurs.TryAdd(user.Id, user);
        }

        public Utilisateur GetUtilisateurById(string id)
        {
            Utilisateur utilisateur;
            return utilisateurs.TryGetValue(id, out utilisateur) ? utilisateur : null;
        }
    }
}