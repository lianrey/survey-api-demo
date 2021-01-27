using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Services.SurveyAPI.Models;
using MyCompany.Services.SurveyAPI.Repositories;

namespace MyCompany.Services.SurveyAPI.Controllers
{
    [Route("api/surveys")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Survey>>> Get()
        {
            var surveys = await _surveyRepository.Get();

            return Ok(surveys);
        }

        [HttpGet("{surveyId}", Name = "GetSurvey")]
        public async Task<ActionResult<Models.Survey>> Get(string surveyId)
        {
            var survey = await _surveyRepository.Get(surveyId);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Survey>> Post([FromBody] SurveyInput surveyInput)
        {
            var survey = _mapper.Map<Models.Survey>(surveyInput);
            survey.CreatedAt = DateTime.Now;
            var createdSurvey = await _surveyRepository.Create(survey);
            
            return CreatedAtRoute("GetSurvey", new { surveyId = survey.Id }, createdSurvey);
        }

        [HttpPut("{surveyId}")]
        public async Task<ActionResult<Models.Survey>> Put(string surveyId, [FromBody] SurveyInput surveyInput)
        {
            var survey = await _surveyRepository.Get(surveyId);
            if (survey == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Models.Survey>(surveyInput);
            result.Id = survey.Id;
            result.CreatedAt = survey.CreatedAt;
            await _surveyRepository.Update(surveyId, result);

            return Ok(result);
        }

        [HttpDelete("{surveyId}")]
        public async Task<ActionResult> Delete(string surveyId)
        {
            if (await _surveyRepository.Get(surveyId) == null)
            {
                return NotFound();
            }

            await _surveyRepository.Remove(surveyId);
            
            return Ok();
        }
    }
}