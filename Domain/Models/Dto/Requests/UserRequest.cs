namespace Domain.Models.Dto.Requests
{
    /// <summary>
    /// Classe de requisição que representa os dados do Usuário
    /// </summary>
    public class UserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
