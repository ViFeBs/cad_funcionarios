public class Funcionario
{
    public int IdFunc { get; set; }
    public string NomeFunc { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public float Salario { get; set; }
    public int? IdLogin { get; set; }

    public Login? Login { get; set; }
}