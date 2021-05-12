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
    public class ServiceRepositoryTest
    {
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly Mock<IRepositoryConnection> _repositoryConnectionMock;

        public ServiceRepositoryTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _repositoryConnectionMock = new Mock<IRepositoryConnection>();
        }

        [TestMethod]
        public void GetServiceById_Ok()
        {
            var jsonDataTable = @"[
                    {
                        'serviceId': '1',
                        'serviceName': 'teste',
                        'DateRegister': '2021-05-05T00:00:00',
                        'serviceStatus': 'true',
                    }    
                ]";

            var serviceId = 1;

            _repositoryConnectionMock.Setup(x => x.SearchCommand("GetServiceById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new ServiceRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetServiceById(serviceId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetServiceById_ArgumentException()
        {
            var jsonDataTable = @"[{}]";

            var serviceId = 1;

            _repositoryConnectionMock.Setup(x => x.SearchCommand("GetServiceById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new ServiceRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetServiceById(serviceId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetServiceById_ArgumentNullException()
        {
            var serviceId = 1;

            var repo = new ServiceRepository(_configurationMock.Object, _repositoryConnectionMock.Object);

            var result = repo.GetServiceById(serviceId);

            Assert.IsNull(result);
        }
    }
}
