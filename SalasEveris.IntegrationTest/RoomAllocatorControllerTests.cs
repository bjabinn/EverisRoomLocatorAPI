using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;
using SalasEveris.Controllers;

namespace SalasEveris.IntegrationTests
{
    public class RoomAllocatorControllerTests : IntegrationTest
    {
        private readonly ILogger<RoomAllocatorController> _logger;
        private readonly RoomContext _context;

        public async Task GetAll_WithoutAnyRoom_ReturnsEmptyResponse()
        {
            //Arrange            

            //Act
            var contr = new RoomAllocatorController(_logger, _context);

            var response = await TestClient.GetAsync(contr.Get());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Room>>()).Should().BeEmpty();
        }        
    }
}
