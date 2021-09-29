using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class ExerciseCategory
    {
        public ExerciseCategory()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
