using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FittnessAppAPI.Models;

public partial class MuscleGroup
{

    [Key]
    public int Id { get; set; }


    [Required]
    public string Name { get; set; } = null!;
}
