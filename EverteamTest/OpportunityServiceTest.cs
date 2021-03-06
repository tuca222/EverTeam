using Everteam.Interfaces;
using Everteam.Models;
using Everteam.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverteamTest
{
    [TestClass]
    public class OpportunityServiceTest
    {
        private readonly Mock<IOpportunityRepository> _opportunityRepositoryMock;

        public OpportunityServiceTest()
        {
            _opportunityRepositoryMock = new Mock<IOpportunityRepository>();
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

            var listOpporunities = JsonConvert.DeserializeObject<List<Opportunity>>(jsonDataTable);

            _opportunityRepositoryMock.Setup(x => x.GetAllOpportunities()).Returns(listOpporunities);

            var service = new OpportunityService(_opportunityRepositoryMock.Object);

            var result = service.GetAllOpportunities();

            Assert.IsNotNull(result);
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

            _opportunityRepositoryMock.Setup(x => x.GetOpportunityByName(opportunityName)).Returns(opportunity);

            var service = new OpportunityService(_opportunityRepositoryMock.Object);

            var result = service.GetOpportunityByName(opportunityName);

            Assert.IsNotNull(result);
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

            var service = new OpportunityService(_opportunityRepositoryMock.Object);

            service.InsertOpportunity(opportunity);

            Assert.IsTrue(true);
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

            var service = new OpportunityService(_opportunityRepositoryMock.Object);

            service.UpdateOpportunity(opportunity);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DeleteOpportunity_Ok()
        {
            var jsonOpportunity = @"{      
                'OpportunityId': 0
                }";

            var opportunity = JsonConvert.DeserializeObject<Opportunity>(jsonOpportunity);

            var service = new OpportunityService(_opportunityRepositoryMock.Object);

            service.DeleteOpportunity(opportunity);

            Assert.IsTrue(true);
        }
    }
}
