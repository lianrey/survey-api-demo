using MyCompany.Services.SurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.Services.SurveyAPI.Repositories
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> Get();
        Task<Survey> Get(string id) ;
        Task<Survey> Create(Survey survey);
        Task Update(string id, Survey surveyIn);
        Task Remove(string id);
    }
}