using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly CriptografiaService _criptografiaService;
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context, CriptografiaService criptografiaService)
    {
        _criptografiaService = criptografiaService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLogin(Login login)
    {
        login.Senha = _criptografiaService.Criptografar(login.Senha);
        _context.Login.Add(login);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLogin), new { id = login.IdLogin }, login);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] Login loginInput)
    {
        // Busca o login no banco pelo nome de usuário
        var login = await _context.Login.FirstOrDefaultAsync(l => l.login == loginInput.login);

        // Verifica se o login existe
        if (login == null)
            return Unauthorized("Login ou senha inválidos.");

        // Descriptografa a senha armazenada para comparar
        var senhaDescriptografada = _criptografiaService.Descriptografar(login.Senha);

        // Compara as senhas
        if (senhaDescriptografada != loginInput.Senha)
            return Unauthorized("Login ou senha inválidos.");

        // Login autenticado com sucesso
        return Ok(new { Message = "Autenticado com sucesso.", LoginId = login.IdLogin });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLogin(int id)
    {
        var login = await _context.Login.FindAsync(id);
        if (login == null)
            return NotFound();

        login.Senha = _criptografiaService.Descriptografar(login.Senha);
        return Ok(login);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLogins()
    {
        var login = await _context.Login.ToListAsync();
        if (login == null)
            return NotFound();

        foreach (Login l in login) { 
            l.Senha = _criptografiaService.Descriptografar(l.Senha); 
        }
        return Ok(login);
    }

    [HttpPut("{idLog}")]
    public async Task<IActionResult> PutLogin(int idlog, Login log)
    {
        if (idlog != log.IdLogin)
            return BadRequest("O ID do login na URL não corresponde ao ID no corpo da requisição.");

        var editFuncionario = await _context.Login.FindAsync(idlog);
        if (editFuncionario == null)
            return NotFound();

        editFuncionario.login = log.login;
        editFuncionario.Senha = _criptografiaService.Criptografar(log.Senha);


        _context.Entry(editFuncionario).State = EntityState.Modified;

        try
        {
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Verifica se o login ainda existe no banco
            if (!_context.Login.Any(e => e.IdLogin == idlog))
                return NotFound();
            else
                throw;
        }

        // Retorna 204 No Content para indicar sucesso
        return NoContent();
    }

    [HttpDelete("{idLog}")]
    public async Task<IActionResult> DeleteLogin(int idLog)
    {
        var login = await _context.Login.FindAsync(idLog);
        if (login == null)
                return NotFound();
        
        _context.Login.Remove(login);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
 