using LSS.HCM.Core.Api;
using LSS.HCM.Core.Security.Handlers;
using LSS.HCM.UnitTest.DependencyResolver.Resolver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace HCM.Core.Security.UnitTest
{
    public class JwtTokenHandlerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
        public JwtTokenHandlerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void PrepareTokenFromAccessToekn()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void VerifyJwtSecurityToken()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            var (isVerified, transactionId) = jwtTokenHandler.VerifyJwtSecurityToken(token);
            Assert.True(isVerified);
        }

        [Fact]
        public void VerifyJwtTokenContainsTransactionId()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            var (isVerified, transactionId) = jwtTokenHandler.VerifyJwtSecurityToken(token);
            Assert.NotEmpty(transactionId);
        }
    }
}
