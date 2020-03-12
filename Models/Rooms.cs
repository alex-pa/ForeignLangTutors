using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForeignLangTutors.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Classes = new HashSet<Classes>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Range(1, 20)]
        [Required]
        public int Capacity { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Classes> Classes { get; set; }
    }
}
