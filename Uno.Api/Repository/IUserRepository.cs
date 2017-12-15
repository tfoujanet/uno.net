namespace Uno.Api.Repository
{
    public interface IUserRepository
    {
        string GetUtilisateurIdByName(string name);

        void AjouterUtilisateur(string id, string name);
    }
}