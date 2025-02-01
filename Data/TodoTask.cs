using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace kuba_web.Data;

public class TodoTask
{
    [Key]
    public int Id { get; set; }
    public int Order { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? Completed { get; set; }
    public bool IsCompleted { get; set; }
    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

}
