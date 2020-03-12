using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForeignLangTutors.Models
{
    public partial class Classes : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Start date and time cannot be empty")]
        public DateTime BeginningTime { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "End date and time cannot be empty")]
        public DateTime СompletionTime { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Rooms Room { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (BeginningTime < DateTime.Now)
            {
                results.Add(new ValidationResult("Start date and time must be greater than current time", new[] { "BeginningTime" }));
            }

            if (СompletionTime <= BeginningTime)
            {
                results.Add(new ValidationResult("EndDateTime must be greater that StartDateTime", new[] { "СompletionTime" }));
            }

            if((СompletionTime.Subtract(BeginningTime)).TotalMinutes < 30)
            {
                results.Add(new ValidationResult("Занятие должно длиться больше 30 минут!", new[] { "СompletionTime" }));
            }

            if ((СompletionTime.Subtract(BeginningTime)).TotalMinutes > 120)
            {
                results.Add(new ValidationResult("Занятие должно длиться меньше 2 часов", new[] { "СompletionTime" }));
            }

            return results;
        }
    }
}
