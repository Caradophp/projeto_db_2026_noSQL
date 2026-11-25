
using Microsoft.AspNetCore.Mvc;
using projeto.Security;
using projeto.Service;

namespace projeto.Controllers;

public class LoginController(
    UserService userService,
    Jwt jwt
    ) : Controller
{
    private readonly UserService _userService = userService;
    private readonly Jwt _jwt = jwt;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CheckUserLogin(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(new ProblemDetails
            {
                Detail = "E-mail deve ser informado."
            });
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return BadRequest(new ProblemDetails
            {
                Detail = "Senha deve ser informada."
            });
        }

        try
        {
            var foundUser = _userService.CheckUserLogin(email, password);

            if (string.IsNullOrEmpty(foundUser.Id))
            {
                return StatusCode(500, new ProblemDetails
                {
                    Detail = "Erro ao recuperar ID do usuário."
                });
            }

            var token = _jwt.GenerateJwtToken(email, "");

            return Ok(new
            {
                token = token,
                userId = foundUser.Id
            });
        }
        catch (KeyNotFoundException)
        {
            return Unauthorized(new ProblemDetails
            {
                Detail = "E-mail ou senha inválidos."
            });
        }
    }

    [HttpPost]
    public IActionResult SendEmailForRetrivePassword(string email)
    {
        _userService.SendEmailForRetrivePassword(email);

        return Ok("sucesso");
    }

    [HttpPost]
    public IActionResult CheckCode(int codigo, string email)
    {
        bool isValid = _userService.CheckCode(codigo, email);

        if (isValid)
        {
            return Ok("sucesso");
        }

        return Unauthorized(new ProblemDetails
        {
            Detail = "Código inválido ou expirado."
        });
    }

    [HttpPatch]
    public IActionResult ChangeUserPassword(
        string email,
        string password,
        string confirmPassword,
        int codigo)
    {
        _userService.ChangePass(
            email,
            password,
            confirmPassword,
            codigo
        );

        return Ok("sucesso");
    }
}
