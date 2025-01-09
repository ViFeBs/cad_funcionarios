public class Login
{
    public int IdLogin { get; set; }
    public string login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty; // Hash da senha

    public ICollection<Funcionario>? Funcionarios { get; set; }
}