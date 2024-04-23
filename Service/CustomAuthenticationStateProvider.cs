using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace LoginAndRegistration.Services
{
    // Custom authentication state provider for managing user authentication
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // Retrieves the current authentication state asynchronously
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Retrieve user email from secure storage
            var userEmail = await SecureStorage.GetAsync("userEmail");
            
            // Create a claims identity if user email exists
            ClaimsIdentity identity = new ClaimsIdentity();
            if (!string.IsNullOrEmpty(userEmail))
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userEmail),
                }, "CustomAuthenticationType");
            }

            // Create a claims principal using the identity
            ClaimsPrincipal user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        // Marks the user as authenticated with the provided email
        public async Task MarkUserAsAuthenticated(string email)
        {
            // Store user email securely
            await SecureStorage.SetAsync("userEmail", email);
            // Notify about authentication state change
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // Marks the user as logged out
        public async Task MarkUserAsLoggedOut()
        {
            // Remove user email from storage
            SecureStorage.Remove("userEmail");
            // Notify about authentication state change
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

