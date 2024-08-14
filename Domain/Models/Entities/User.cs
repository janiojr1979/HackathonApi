namespace Domain.Models.Entities
{
    /// <summary>
    /// Classe que representa a entidade Usuário
    /// </summary>
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
