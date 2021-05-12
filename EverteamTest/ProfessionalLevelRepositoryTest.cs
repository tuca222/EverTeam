using Everteam.Interfaces;
using Everteam.Models;
using Everteam.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EverteamTest
{
    [TestClass]
    public class ProfessionalLevelRepositoryTest
    {
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly Mock<IRepositoryConnection> _repositoryConnection;

        public ProfessionalLevelRepositoryTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _repositoryConnection = new Mock<IRepositoryConnection>();
        }

        [TestMethod]
        public void GetProfessionalLevelById_OK()
        {
            var jsonDataTable = @"[
                    {
                        'professionalLevelId': '1',
                        'professionalLevelName': 'teste',
                        'professionalLevelSection': '1',
                        'DateRegister': '2021-05-05T00:00:00',
                        'professionalLevelStatus': 'true',  
                    }    
                ]";

            var professionalLevelId = 1;

            _repositoryConnection.Setup(x => x.SearchCommand("GetProfessionalLevelById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new ProfessionalLevelRepository(_configurationMock.Object, _repositoryConnection.Object);

            var result = repo.GetProfessionalLevelById(professionalLevelId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProfessionalLevelById_ArgumentException()
        {
            var jsonDataTable = @"[{}]";

            var professionalLevelId = 1;

            _repositoryConnection.Setup(x => x.SearchCommand("GetProfessionalLevelById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new ProfessionalLevelRepository(_configurationMock.Object, _repositoryConnection.Object);

            var result = repo.GetProfessionalLevelById(professionalLevelId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetProfessionalLevelById_ArgumentNullException()
        {
            var professinalLevelId = 1;

            var repo = new ProfessionalLevelRepository(_configurationMock.Object, _repositoryConnection.Object);

            var result = repo.GetProfessionalLevelById(professinalLevelId);

            Assert.IsNull(result);
        }
    }
}
