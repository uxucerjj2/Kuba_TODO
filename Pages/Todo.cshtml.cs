using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using kuba_web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace kuba_web.Pages;

[Authorize]
public class Todo(ApplicationDbContext dbContext, ILogger<Todo> logger) : PageModel
{
    public IList<TodoTaskDTO> Tasks = [];

    [BindProperty]
    [Required]
    [MinLength(3)]
    public string NewTaskName { get; set; } = "";

    public class TodoTaskDTO {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? Completed { get; set; }
        public bool IsCompleted { get; set; }
    }

    public async Task OnGetAsync()
    {
        var user = await dbContext.Users.SingleAsync(u => u.UserName == User.Identity!.Name);

        Tasks = await dbContext.Tasks.Where(t => t.UserId == user.Id).Select(e => new TodoTaskDTO {
            Id = e.Id,
            Order = e.Order,
            Name = e.Name,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            Completed = e.Completed,
            IsCompleted = e.IsCompleted,

        }).ToListAsync();
        logger.LogInformation("Tasks: {Tasks}", JsonSerializer.Serialize(Tasks));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await dbContext.Users.SingleAsync(u => u.UserName == User.Identity!.Name);
            await dbContext.Tasks.AddAsync(new TodoTask()
            {
                Name = NewTaskName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = user!.Id
            });

            await dbContext.SaveChangesAsync();
        }

        return RedirectToAction("");
    }

    public async Task<IActionResult> OnGetDelete(int id)
    {
        var user = await dbContext.Users.SingleAsync(u => u.UserName == User.Identity!.Name);
        var task = await dbContext.Tasks.SingleOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task != null)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("");
    }
    public async Task<IActionResult> OnGetToggle(int id)
    {
        var user = await dbContext.Users.SingleAsync(u => u.UserName == User.Identity!.Name);
        var task = await dbContext.Tasks.SingleOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task != null)
        {
            if (task.IsCompleted)
            {
                task.IsCompleted = false;
                task.Completed = null;
            }
            else
            {
                task.IsCompleted = true;
                task.Completed = DateTime.Now;
            }

            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("");
    }

    public string CompletedStyle(bool isCompleted)
    {
        return isCompleted ? "text-decoration: line-through" : "";
    }
}

