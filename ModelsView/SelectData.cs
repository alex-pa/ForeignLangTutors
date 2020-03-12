using System.Collections.Generic;

namespace ForeignLangTutorsMVC.ModelsView
{
    public class SelectData
    {
        public List<string> languages = new List<string>
        {
            "английский",
            "французский",
            "немецкий",
            "итальянский",
            "китайский",
            "испанский",
            "португальский"
        };
        public List<string> levels = new List<string>
        {
            "(А1) Elementary",
            "(А2) Pre-Intermediate",
            "(В1) Intermediate",
            "(В2) Upper-Intermediate",
            "(С1) Advanced",
            "(С2) Proficiency"
        };
    }
}
