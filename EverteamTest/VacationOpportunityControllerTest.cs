using Everteam.Controllers;
using Everteam.Interfaces;
using Everteam.Models;
using Everteam.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EverteamTest
{
    [TestClass]
    public class VacationOpportunityControllerTest
    {
        private readonly Mock<IVacationOpportunityService> _vacationOpportunityServiceMock;

        public VacationOpportunityControllerTest()
        {
            _vacationOpportunityServiceMock = new Mock<IVacationOpportunityService>();
        }

        [TestMethod]
        public void GetAllVacationOpportunities_Ok()
        {
            var jsonDataTable = @"[
                    {
                        'VacationOpportunityId': '1',
                        'VacationOpeningNumber': 'PRE - 2020 - 0001234',
                        'VacationOpeningDate': '2021-05-05',
                        'VacationOfferLetterDate':'2021-05-05',
                        'VacationLeader': 'Thomas Anjos',
                        'VacationCancellationdate': '2021-05-05',
                        'VacationOpportunityStatus': 'true',
                        'CareerId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                    },
                    {
                        'VacationOpportunityId': '2',
                        'VacationOpeningNumber': 'PRE - 2020 - 0001234',
                        'VacationOpeningDate': '2021-05-05',
                        'VacationOfferLetterDate':'2021-05-05',
                        'VacationLeader': 'Thomas Anjos',
                        'VacationCancellationdate': '2021-05-05',
                        'VacationOpportunityStatus': 'false',
                        'CareerId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                    }
                ]";

            var listVacationOpportunities = JsonConvert.DeserializeObject<List<VacationOpportunity>>(jsonDataTable);

            _vacationOpportunityServiceMock.Setup(x => x.GetAllVacationOpportunities()).Returns(listVacationOpportunities);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.GetAllVacationOpportunities();

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetVacationOpportunityByVacationLeader_Ok()
        {
            var jsonVacationOpportunity = @"
                    {
                        'VacationOpportunityId': '1',
                        'VacationOpeningNumber': 'PRE - 2020 - 0001234',
                        'VacationOpeningDate': '2021-05-05',
                        'VacationOfferLetterDate':'2021-05-05',
                        'VacationLeader': 'Thomas Anjos',
                        'VacationCancellationdate': '2021-05-05',
                        'VacationOpportunityStatus': 'true',
                        'CareerId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                    }";

            var vacationLeader = "Thomas Anjos";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            _vacationOpportunityServiceMock.Setup(x => x.GetVacationOpportunityByVacationLeader(vacationLeader)).Returns(vacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.GetVacationOpportunityByVacationLeader(vacationLeader);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetVacationOpportunityByVacationLeader_BadRequest()
        {
            var vacationLeader = "";

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.GetVacationOpportunityByVacationLeader(vacationLeader);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void InsertVacationOpportunity_Ok()
        {
            var jsonVacationOpportunity = @"{
                        'vcationOpportunityId': '1',
                        'vacationOpeningNumber': 'PRE - 2020 - 0001234',
                        'vacationOpeningDate': '2021-05-05',
                        'vacationOfferLetterDate':'2021-05-05',
                        'vacationLeader': 'Thomas Anjos',
                        'vacationCancellationdate': '2021-05-05',
                        'vacationOpportunityStatus': 'true',
                        'career': {
                            'careerId': '1'
                        },
                        'professionalLevel': {
                            'professionalLevelId': '1'
                        },
                        'opportunityType': {
                            'opportunityTypeId': 1
                        }
                        }";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.InsertVacationOpportunity(vacationOpportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void InsertVacationOpportunity_BadRequest()
        {
            var jsonVacationOpportunity = @"";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.InsertVacationOpportunity(vacationOpportunity);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void UpdateVacationOpportunity_Ok()
        {
            var jsonVacationOpportunity = @"{
                        'VacationOpportunityId': '1',
                        'VacationOpeningNumber': 'PRE - 2020 - 0001234',
                        'VacationOpeningDate': '2021-05-05',
                        'VacationOfferLetterDate':'2021-05-05',
                        'VacationLeader': 'Thomas Anjos',
                        'VacationCancellationdate': '2021-05-05',
                        'VacationOpportunityStatus': 'true',
                        'CareerId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                        }";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.UpdateVacationOpportunity(vacationOpportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void UpdateVacationOpportunity_BadRequest()
        {
            var jsonVacationOpportunity = @"";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.UpdateVacationOpportunity(vacationOpportunity);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void DeleteVacationOpportunity_Ok()
        {
            var jsonVacationOpportunity = @"{
                'vacationOpportunityId': '1'
                }";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.DeleteVacationOpportunity(vacationOpportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void DeleteVacationOpportunity_BadRequest()
        {
            var jsonVacationOpportunity = @"";

            var vacationOpportunity = JsonConvert.DeserializeObject<VacationOpportunity>(jsonVacationOpportunity);

            var control = new VacationOpportunityController(_vacationOpportunityServiceMock.Object);

            var result = control.DeleteVacationOpportunity(vacationOpportunity);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
    }
}
