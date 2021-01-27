using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyCompany.Services.SurveyAPI.Models
{
    public class SurveyInput
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public Guid ModifyBy { get; set;}
    }
}