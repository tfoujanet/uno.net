using System.Collections.Concurrent;

namespace Uno.Api.Repository
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, string> utilisateurs;

        public MemoryUserRepository()
        {
            utilisateurs = new ConcurrentDictionary<string, string>();
        }

        public void AjouterUtilisateur(string id, string name)
        {
            utilisateurs.TryAdd(name, id);
        }

        public string GetUtilisateurIdByName(string name)
        {
            string id;
            return utilisateurs.TryGetValue(name, out id) ? id : null;
        }
    }
}