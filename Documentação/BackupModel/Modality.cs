﻿using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Modality
    {
        public Modality()
        {
            ModalityClasses = new HashSet<ModalityClass>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int DaysInWeek { get; set; }

        public virtual ICollection<ModalityClass> ModalityClasses { get; set; }
    }
}
