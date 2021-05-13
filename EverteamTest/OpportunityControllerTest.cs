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
    public class OpportunityControllerTest
    {
        private readonly Mock<IOpportunityService> _opportunityServiceMock;

        public OpportunityControllerTest()
        {
            _opportunityServiceMock = new Mock<IOpportunityService>();
        }

        [TestMethod]
        public void GetAllOpportunities_Ok()
        {
            var jsonDataTable = @"[
                    {
                        'OpportunityId': '1',
                        'OpportunityName': 'Squad Care',
                        'OpportunityRequirements': '.NET Core',
                        'DesirableRequirements': 'Conhecimento em Kafka',
                        'DateRegister': '2021-05-05T00:00:00',
                        'ClosingDate': '2021-05-05T00:00:00',
                        'CancellationDate': '2021-05-05T00:00:00',
                        'OpportunityStatus': true,
                        'CareerId': '1',
                        'ServiceId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                    },
                    {
                        'OpportunityId': '2',
                        'OpportunityName': 'Squad Care',
                        'OpportunityRequirements': '.NET Core',
                        'DesirableRequirements': 'Conhecimento em Kafka',
                        'DateRegister': '2021-05-05T00:00:00',
                        'ClosingDate': '2021-05-05T00:00:00',
                        'CancellationDate': '2021-05-05T00:00:00',
                        'OpportunityStatus': 'false',
                        'CareerId': '1',
                        'ServiceId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',    
                    }
                ]";

            var listOpportunities = JsonConvert.DeserializeObject<List<Opportunity>>(jsonDataTable);

            _opportunityServiceMock.Setup(x => x.GetAllOpportunities()).Returns(listOpportunities);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.GetAllOpportunities();

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }



        [TestMethod]
        public void GetOpportunityByName_Ok()
        {
            var jsonOpportunity = @"
                    {
                        'OpportunityId': '1',
                        'OpportunityName': 'Squad Care',
                        'OpportunityRequirements': '.NET Core',
                        'DesirableRequirements': 'Conhecimento em Kafka',
                        'DateRegister': '2021-05-05T00:00:00',
                        'ClosingDate': '2021-05-05T00:00:00',
                        'CancellationDate': '2021-05-05T00:00:00',
                        'OpportunityStatus': false,
                        'CareerId': '1',
                        'ServiceId': '1',
                        'ProfessionalLevelId': '1',
                        'OpportunityTypeId': '1',
                    }";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var opportunityName = "Squad Care";

            _opportunityServiceMock.Setup(x => x.GetOpportunityByName(opportunityName)).Returns(opportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.GetOpportunityByName(opportunityName);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetOpportunityByName_BadRequest()
        {

            var opportunityName = "";

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.GetOpportunityByName(opportunityName);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void InsertOpportunity_Ok()
        {
            var jsonOpportunity = @"{
                'opportunityId': 1,
                'opportunityName': 'Squad Care alterado',
                'opportunityRequirements': '.NET Core',
                'desirableRequirements': 'Conhecimento em Kafka',
                'dateRegister': '2021-05-05T00:00:00',
                'closingDate': '2021-05-05T00:00:00',
                'cancellationDate': '2021-05-05T00:00:00',
                'opportunityStatus': false,
                'career': {
                    'careerId': 1
                },
                'service': {
                    'serviceId': 1
                },
                'professionalLevel': {
                    'professionalLevelId': 1
                },
                'opportunityType': {
                    'opportunityTypeId': 1
                }
                }";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.InsertOpportunity(opportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void InsertOpportunity_BadRequest()
        {
            var jsonOpportunity = @"";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.InsertOpportunity(opportunity);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void UpdateOpportunity_Ok()
        {
            var jsonOpportunity = @"{      
                'opportunityId': 1,
		        'opportunityName': 'Squad Care alterado',
		        'opportunityRequirements': '.NET Core',
		        'desirableRequirements': 'Conhecimento em Kafka',
		        'dateRegister':'2021-05-05',
		        'closingDate':'2021-05-05',
		        'cancellationDate':'2021-05-05',
                'opportunityStatus': true
                }";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.UpdateOpportunity(opportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void UpdateOpportunity_BadRequest()
        {
            var jsonOpportunity = @"";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.UpdateOpportunity(opportunity);

            var badRequestResult = result as BadRequestResult;

            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void DeleteOpportunity_Ok()
        {
            var jsonOpportunity = @"{      
                'OpportunityId': 1
                }";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.DeleteOpportunity(opportunity);

            var okResult = result as OkResult;

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void DeleteOpportunity_BadRequest()
        {
            var jsonOpportunity = @"";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var control = new OpportunityController(_opportunityServiceMock.Object);

            var result = control.DeleteOpportunity(opportunity);

            var badResquestResult = result as BadRequestResult;

            Assert.AreEqual(400, badResquestResult.StatusCode);
        }
    }
}
