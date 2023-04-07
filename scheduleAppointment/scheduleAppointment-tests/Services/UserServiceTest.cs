using Moq;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_service.Interface;
using scheduleAppointment.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace scheduleAppointment_tests.Services
{
    public class UserServiceTest
    {
        Mock<IAuthenticationService> authenticationMock;


        public UserServiceTest()
        {
            authenticationMock = new Mock<IAuthenticationService>();
        }

        [Fact]
        public async Task VerifySignInSucess()
        {
            OAuthRequest oAuthRequest = new OAuthRequest() { Username = "teste", Password = "1234" };
            TokenResponse TokenResponse = new TokenResponse
            {
                Username = "teste",
                Email = "",
                Token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNhbWlsYSIsInVzZXJJZCI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2NzQwNjAyNTEsImV4cCI6MTY3NDA4OTA1MSwiaWF0IjoxNjc0MDYwMjUxfQ.V2tPDYEYs14mAqMcYlHxNncB5-mvK6a1xZXN-AYb_UzMpdTtKa_RfdLlpq9jsKz7U7booohBh1AyXAU_GYHBk5qwzZ2fclcU_AtpPclk60ayfMZ0YbFURZFjL3fpjtKZXbBCi5P09sAWH_0WeD-WRu3EiV-cS8rOC4jH9OHd_-9Eq4x-PjsD8xv5zqJs_pbK5-HE8A20w9k_S8l_5FT7xrniFTC6gOHta-6pTVcOKZZnWViv2e_RL5g-Jdc-eSCh_-BL2KNNNgWwokMTG6yGWljg6WTyjcZ3A1YkHniwVTqqOTkkEes_rE7fzGky16liDNszYA4PuIXGNEOENqw1RA",
                RefreshToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNhbWlsYSIsInJlZnJlc2hfdG9rZW4iOiJ0cnVlIiwicmVmcmVzaF9wYXNzd29yZCI6IjEyMzQ1IiwibmJmIjoxNjc0MDYwMjUxLCJleHAiOjE2NzQxMTc4NTEsImlhdCI6MTY3NDA2MDI1MSwiaXNzIjoic2NoZWR1bGVfYXBwb2ludG1lbnQuSXNzdWVyIiwiYXVkIjoic2NoZWR1bGVfYXBwb2ludG1lbnQuQXVkaWVuY2VcIiwifQ.hEwF7JDV9u6XLCIR4-Rh8QzsHsIF6ampxRWlhCF4lCIfc0IvOWl0CYNVK7WZfrVIo5D1KO0sCF0ygCuZZtA4267lo1vd_GNYm6kzxVaI4D_6qgIVmjNtcxlyOgSpeku3RzkSKFD6vKhY8TKhi0E2xXkOUWWGMyna3R-mkwMC2fOXvbmc5C_29p-4PqQYExPivSZInmukztQsIy4PRAN6-MuBxP3XddxDQctV_IJnO6XA8KwWAA0w6OeRnKmPWNVzuZwc_VltMvVKQC9_JRfT7QgWqLmhkDFNPSAZKw1ItkYEEMhV5ZqHgCcsofW0EswXkfDu8sSYMVA6yZP3Q_I8Hg",
                Created = DateTime.Now,
                Expires = DateTime.Now,
                isAdmin = true,
            };

            AuthenticationController authenticationController = new AuthenticationController(authenticationMock.Object);

            authenticationMock.Setup(p => p.AuthenticateAsync(oAuthRequest)).ReturnsAsync(TokenResponse);

            TokenResponse result = await authenticationController.OAuthAsync(oAuthRequest);

            Assert.Equal("teste", result.Username);

        }

        [Fact]
        public async Task VerifySignInWithoutPassword()
        {
            OAuthRequest oAuthRequest = new OAuthRequest() { Username = "teste" };


            AuthenticationController authenticationController = new AuthenticationController(authenticationMock.Object);

            authenticationMock.Setup(p => p.AuthenticateAsync(oAuthRequest)); ;

            TokenResponse result = await authenticationController.OAuthAsync(oAuthRequest);

            Assert.Equal("teste", result.Username);

        }
    }
}
