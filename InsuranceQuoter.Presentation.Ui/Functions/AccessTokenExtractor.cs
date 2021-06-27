namespace InsuranceQuoter.Presentation.Ui.Functions
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Presentation.Ui.Exceptions;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

    public class AccessTokenExtractor
    {
        private readonly IAccessTokenProvider accessTokenProvider;

        public AccessTokenExtractor(IAccessTokenProvider accessTokenProvider)
        {
            this.accessTokenProvider = accessTokenProvider;
        }

        public async Task<string> Extract()
        {
            AccessTokenResult accessTokenResult = await accessTokenProvider.RequestAccessToken().ConfigureAwait(false);

            if (accessTokenResult.TryGetToken(out AccessToken accessToken))
            {
                return accessToken.Value;
            }

            throw new AccessTokenNotFoundException();
        }
    }
}