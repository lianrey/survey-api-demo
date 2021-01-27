using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using MyCompany.Services.SurveyAPI.Configuration;
using MyCompany.Services.SurveyAPI.Models;
using MyCompany.Services.SurveyAPI.Repositories;
using AutoMapper;
using MyCompany.Services.SurveyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyCompany.Services.SurveyAPI.Test
{
    [TestClass]
    public class SurveyControllerTests
    {
        private readonly Mock<ISurveyRepository> _surveyRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SurveyController _surveyController;

        public SurveyControllerTests()
        {
            _surveyRepositoryMock = new Mock<ISurveyRepository>();
            _mapperMock = new Mock<IMapper>();
            _surveyController = new SurveyController(_surveyRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Test_Get_WithoutParameters_ShouldCallFindAsyncOnce()
        {
            _surveyRepositoryMock.Setup(s => s.Get()).ReturnsAsync(new List<Survey>());

            var surveys = await _surveyController.Get();

            _surveyRepositoryMock.Verify(v => v.Get(), Times.Once);
        }

        [TestMethod]
        public async Task Test_Get_WithoutParameters_ShouldThrowError()
        {
            _surveyRepositoryMock.Setup(s => s.Get()).ThrowsAsync(new Exception());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _surveyController.Get());
        }

        [TestMethod]
        public async Task Test_Get_WithId_ShouldReturnNotFound()
        {
            Survey returnedObject = null;
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(returnedObject);

            var actionResult = await _surveyController.Get("test123");

            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Test_Get_WithId_ShouldReturnSurvey()
        {
            Survey returnedObject = new Survey() { Id = "test123", Title = "Survey 123" };
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(returnedObject);

            var actionResult = await _surveyController.Get("test123");

            Assert.IsNotNull(((OkObjectResult) actionResult.Result).Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            Assert.AreSame(returnedObject, ((OkObjectResult) actionResult.Result).Value);
        }

        public async Task Test_Get_WithId_ShouldThrowError()
        {
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ThrowsAsync(new Exception());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _surveyController.Get(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task Test_Post_WithSurveyParameter_ShouldCallCreateOnce()
        {
            _surveyRepositoryMock.Setup(s => s.Create(It.IsAny<Survey>())).ReturnsAsync(new Survey());
            _mapperMock.Setup(s => s.Map<Survey>(It.IsAny<SurveyInput>())).Returns(new Survey());

            var survey = await _surveyController.Post(new SurveyInput());

            _surveyRepositoryMock.Verify(v => v.Create(It.IsAny<Survey>()), Times.Once);
        }

        [TestMethod]
        public async Task Test_Post_WithParameters_ShouldThrowError()
        {
            _surveyRepositoryMock.Setup(s => s.Create(It.IsAny<Survey>())).ThrowsAsync(new Exception());
            _mapperMock.Setup(s => s.Map<Survey>(It.IsAny<SurveyInput>())).Returns(new Survey());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _surveyController.Post(new SurveyInput()));
        }

        [TestMethod]
        public async Task Test_Put_WithSurveyParameter_ShouldCallUpdateOnce()
        {
            _surveyRepositoryMock.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<Survey>())).Returns(Task.CompletedTask);
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Survey());
            _mapperMock.Setup(s => s.Map<Survey>(It.IsAny<SurveyInput>())).Returns(new Survey());

            var survey = await _surveyController.Put("test123", new SurveyInput());

            _surveyRepositoryMock.Verify(v => v.Update(It.IsAny<string>(), It.IsAny<Survey>()), Times.Once);
        }

        [TestMethod]
        public async Task Test_Put_WithSurveyParameter_ShouldReturnNotFound()
        {
            Survey returnedObject = null;
            _surveyRepositoryMock.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<Survey>())).Returns(Task.CompletedTask);
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(returnedObject);
            _mapperMock.Setup(s => s.Map<Survey>(It.IsAny<SurveyInput>())).Returns(new Survey());

            var actionResult = await _surveyController.Put("test123", new SurveyInput());

            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Test_Put_WithParameters_ShouldThrowError()
        {
            _surveyRepositoryMock.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<Survey>())).ThrowsAsync(new Exception());
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Survey());
            _mapperMock.Setup(s => s.Map<Survey>(It.IsAny<SurveyInput>())).Returns(new Survey());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _surveyController.Put("Test123", new SurveyInput()));
        }

        [TestMethod]
        public async Task Test_Delete_WithSurveyId_ShouldCallRemoveOnce()
        {
            _surveyRepositoryMock.Setup(s => s.Remove(It.IsAny<string>())).Returns(Task.CompletedTask);
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Survey());

            var survey = await _surveyController.Delete("test123");

            _surveyRepositoryMock.Verify(v => v.Remove(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task Test_Delete_WithSurveyId_ShouldReturnNotFound()
        {
            Survey returnedObject = null;
            _surveyRepositoryMock.Setup(s => s.Remove(It.IsAny<string>())).Returns(Task.CompletedTask);
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(returnedObject);

            var actionResult = await _surveyController.Delete("test123");

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Test_Delete_WithSurveyId_ShouldThrowError()
        {
            _surveyRepositoryMock.Setup(s => s.Remove(It.IsAny<string>())).ThrowsAsync(new Exception());
            _surveyRepositoryMock.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Survey());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _surveyController.Delete("Test123"));
        }
    }
}
