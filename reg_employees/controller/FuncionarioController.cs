using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class FuncionariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public FuncionariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFuncionario(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.IdFunc }, funcionario);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFuncionario(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null)
            return NotFound();

        return Ok(funcionario);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFuncionario()
    {
        var funcionario = await _context.Funcionarios.ToListAsync();
        if (funcionario == null)
            return NotFound();

        return Ok(funcionario);
    }
}