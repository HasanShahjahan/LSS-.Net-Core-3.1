namespace LSS.HCM.Core.Security.Handlers
{
    public interface IJwtTokenHandler
    {
        string PrepareTokenFromAccessToekn(string accessToken);
        string GenerateJwtSecurityToken(string userId);
        (bool, string) VerifyJwtSecurityToken(string token);
    }
}
