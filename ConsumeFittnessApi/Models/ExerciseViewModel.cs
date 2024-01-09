using System.ComponentModel.DataAnnotations;

namespace ConsumeFittnessApi.Models
{
    public class ExerciseViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MuscleGroupId { get; set; }
    }
}
