using System;
using System.Collections.Generic;

namespace FittnessAppAPI.Models;

public partial class ExcerciseDay
{
    public int Id { get; set; }

    public int Day { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
