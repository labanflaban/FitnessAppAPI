using System;
using System.Collections.Generic;

namespace FittnessAppAPI.Models;

public partial class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MuscleGroupId { get; set; }


}
