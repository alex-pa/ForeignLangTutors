using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForeignLangTutors.Models
{
    public partial class Tutors
    {
        public Tutors()
        {
            Groups = new HashSet<Groups>();
        }

        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        [Range(1, 9)]
        public int Grade { get; set; }
        [Required]
        public string Level { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
    }
}
