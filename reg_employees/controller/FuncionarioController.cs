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
    public async Task<IActionResult> PostFuncionario(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFuncionario), new { idFunc = funcionario.IdFunc }, funcionario);
    }

    [HttpGet("{idFunc}")]
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

    [HttpPut("{idFunc}")]
    public async Task<IActionResult> PutFuncionario(int idFunc, Funcionario funcionario)
    {
        if (idFunc != funcionario.IdFunc)
            return BadRequest("O ID do funcionário na URL não corresponde ao ID no corpo da requisição.");

        var editFuncionario = await _context.Funcionarios.FindAsync(idFunc);
        if (editFuncionario == null)
            return NotFound();

        editFuncionario.NomeFunc = funcionario.NomeFunc;
        editFuncionario.Salario = funcionario.Salario;
        editFuncionario.Cargo = funcionario.Cargo;
        editFuncionario.Email = funcionario.Email;

        _context.Entry(editFuncionario).State = EntityState.Modified;

        try
        {
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Verifica se o funcionário ainda existe no banco
            if (!_context.Funcionarios.Any(e => e.IdFunc == idFunc))
                return NotFound();
            else
                throw;
        }

        // Retorna 204 No Content para indicar sucesso
        return NoContent();
    }

    [HttpDelete("{idFunc}")]
    public async Task<IActionResult> DeleteFuncionario(int idFunc)
    {
        var funcionario = await _context.Funcionarios.FindAsync(idFunc);
        if (funcionario == null)
                return NotFound();
        
        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}