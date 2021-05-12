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
    public class OpportunityTypeRepositoryTest
    {
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly Mock<IRepositoryConnection> _repositoryConnectionMock;

        public OpportunityTypeRepositoryTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _repositoryConnectionMock = new Mock<IRepositoryConnection>();
        }

        [TestMethod]
        public void GetOpportunityTypeById_Ok()
        {
            var jsonDataTable = @"[
                    {
                        'opportunityTypeId': '1',
                        'opportunityName': 'teste',
                        'DateRegister': '2021-05-05T00:00:00',
                        'opportunityTypeStatus': 'true',
                    }    
                ]";
            var opportunityTypeId = 1;

            _repositoryConnectionMock.Setup(x => x.SearchCommand("GetOpportunityTypeById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new OpportunityTypeRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetOpportunityTypeById(opportunityTypeId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOpportunityTypeById_ArgumentException()
        {
            var jsonDataTable = @"[{}]";

            var opportunityTypeId = 1;

            _repositoryConnectionMock.Setup(x => x.SearchCommand("GetOpportunityTypeById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new OpportunityTypeRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetOpportunityTypeById(opportunityTypeId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetOpportunityTypeById_ArgumentNullException()
        {
            var opportunityTypeId = 1;

            var repo = new OpportunityTypeRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetOpportunityTypeById(opportunityTypeId);

            Assert.IsNull(result);
        }
    }
}
