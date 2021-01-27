using MyCompany.Services.SurveyAPI.Configuration;
using MyCompany.Services.SurveyAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Services.SurveyAPI.Repositories
{
    public class SurveyRepository: ISurveyRepository
    {
        private readonly IMongoCollection<Survey> _surveys;

        public SurveyRepository(ISurveyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _surveys = database.GetCollection<Survey>(settings.SurveysCollectionName);
        }

        public async Task<List<Survey>> Get()
        {
            var surveys = await _surveys.FindAsync(survey => true);
            return surveys.ToList();
        }             

        public async Task<Survey> Get(string id)
        {
            var surveys = await _surveys.FindAsync<Survey>(survey => survey.Id == id);
            return surveys.FirstOrDefault();   
        }

        public async Task<Survey> Create(Survey survey)
        {
            await _surveys.InsertOneAsync(survey);
            return survey;
        }

        public Task Update(string id, Survey surveyIn) =>
            _surveys.ReplaceOneAsync(survey => survey.Id == id, surveyIn);

        public Task Remove(string id) => 
            _surveys.DeleteOneAsync(survey => survey.Id == id);
    }
}