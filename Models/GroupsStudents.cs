using System;
using System.Collections.Generic;

namespace ForeignLangTutors.Models
{
    public partial class GroupsStudents
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Students Student { get; set; }
    }
}
