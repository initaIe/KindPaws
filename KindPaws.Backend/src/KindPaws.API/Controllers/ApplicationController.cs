using Microsoft.AspNetCore.Mvc;

namespace KindPaws.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase;