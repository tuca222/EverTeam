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
    public class CareerRepositoryTest
    {
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly Mock<IRepositoryConnection> _repositryConnectionMock;

        public CareerRepositoryTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _repositryConnectionMock = new Mock<IRepositoryConnection>();
        }

        [TestMethod]
        public void GetCareerById_Ok()
        {
            var jsonDataTable = @"[
                    {
                        'careerId': '1',
                        'careerName': 'teste',
                        'DateRegister': '2021-05-05T00:00:00',
                        'careerStatus': 'true',
                    }    
                ]";

            var carrerId = 1;

            _repositryConnectionMock.Setup(x => x.SearchCommand("GetCareerById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new CareerRepository(_configurationMock.Object, _repositryConnectionMock.Object);

            var result = repo.GetCareerById(carrerId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCareerById_ArgumentException()
        {
            var jsonDataTable = @"[{}]";

            var carrerId = 1;

            _repositryConnectionMock.Setup(x => x.SearchCommand("GetCareerById", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            var repo = new CareerRepository(_configurationMock.Object, _repositryConnectionMock.Object);

            var result = repo.GetCareerById(carrerId);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCareerById_ArgumentNullException()
        {
            var carrerId = 1;

            var repo = new CareerRepository(_configurationMock.Object, _repositryConnectionMock.Object);

            var result = repo.GetCareerById(carrerId);

            Assert.IsNull(result);
        }

    }
}
