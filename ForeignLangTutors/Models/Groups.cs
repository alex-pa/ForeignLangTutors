using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForeignLangTutors.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Classes = new HashSet<Classes>();
            GroupsStudents = new HashSet<GroupsStudents>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TutorId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        [Range(1, 20)]
        public int NumberOfStudents { get; set; }

        public virtual Tutors Tutor { get; set; }
        public virtual ICollection<Classes> Classes { get; set; }
        public virtual ICollection<GroupsStudents> GroupsStudents { get; set; }
    }
}
