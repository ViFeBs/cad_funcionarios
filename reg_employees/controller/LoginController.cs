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
}