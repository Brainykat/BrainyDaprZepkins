using System.Security.Claims;

namespace AuthorizeAuthenticate
{
    public static class ClaimsGuardService
    {
        public static Guid GetUserId(this IEnumerable<Claim> claims)
        {
            var userID = claims?.FirstOrDefault(
              x =>
              x.Type.Equals("sub", StringComparison.OrdinalIgnoreCase));
            if (userID == null) { throw new AccessViolationException("User has No ID claim"); }
            var isValid = Guid.TryParse(userID.Value, out Guid compId);
            if (isValid)
            {
                if (compId == Guid.Empty) { throw new AccessViolationException("User has Invalid ID claim"); }
                else { return compId; }
            }
            else
            {
                throw new AccessViolationException("User has Invalid ID claim");
            }
        }
        public static Guid GetUserClientId(this IEnumerable<Claim> claims)
        {
            var userID = claims?.FirstOrDefault(
              x =>
              x.Type.Equals("PetroStationBranchID", StringComparison.OrdinalIgnoreCase));
            if (userID == null) { throw new AccessViolationException("User has No Agency Branch claim"); }
            var isValid = Guid.TryParse(userID.Value, out Guid compId);
            if (isValid)
            {
                if (compId == Guid.Empty) { throw new AccessViolationException("User has Invalid Agency Branch claim"); }
                else { return compId; }
            }
            else
            {
                throw new AccessViolationException("User has Invalid Agency Branch claim");
            }
        }
        public static Guid GetUserPetroStationId(this IEnumerable<Claim> claims)
        {
            var userID = claims?.FirstOrDefault(
              x =>
              x.Type.Equals("PetroStationID", StringComparison.OrdinalIgnoreCase));
            if (userID == null) { throw new AccessViolationException("User has No Agency  claim"); }
            var isValid = Guid.TryParse(userID.Value, out Guid compId);
            if (isValid)
            {
                if (compId == Guid.Empty) { throw new AccessViolationException("User has Invalid Agency claim"); }
                else { return compId; }
            }
            else
            {
                throw new AccessViolationException("User has Invalid Agency claim");
            }
        }


        public static List<string> GetUserRoles(this IEnumerable<Claim> claims)
        {
            return claims.RoleClaims().Select(z => z.Value).ToList();
        }
        private static IEnumerable<Claim> RoleClaims(this IEnumerable<Claim> claims)
        {
            var roleclaims = claims?.Where(x => x.Type.Equals("Role", StringComparison.OrdinalIgnoreCase));
            if (!roleclaims.Any()) throw new AccessViolationException("User has No role claims Defined");
            return roleclaims;
        }
    }
}