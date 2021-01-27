using System.Collections.Generic;

namespace MyCompany.Services.SurveyAPI.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }
}