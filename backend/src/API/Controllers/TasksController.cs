using Microsoft.AspNetCore.Mvc;
using QH.Application.DTOs;
using QH.Application.Interfaces;

namespace QH.API.Controllers;

[Route("api/houses/{houseId}/tasks")]
public class TasksController : ApiController
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByHouseId(int houseId)
    {
        var tasks = await _taskService.GetByHouseIdAsync(houseId);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int houseId, [FromBody] CreateTaskRequest request)
    {
        // Model validation sker automatisk via [ApiController] — invalid requests returnerer 400
        // TODO: implementer ITaskService.CreateAsync når vi bygger write-operationer
        return StatusCode(501);
    }
}
