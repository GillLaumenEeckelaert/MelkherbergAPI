using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Framework.Authentication.Google;

/*public class GoogleAuthentication(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;
    
    public record TokenDto(string AccessToken, string RefreshToken);

    public async Task<TokenDto> AuthenticatieWithGoogle(string accessToken, string idToken)
    {
        // Validate the ID token and retrieve the payload
        var payload = await ValidateGoogleTokenAsync(idToken);

        // Fetch additional user profile information using the access token
        var userProfile = await GetGoogleUserProfileAsync(accessToken);

        // Check if the user already exists in the database
        //var userExists = await identityUserRepository.IsExistsAsync(payload.Email);
        var userExists = true;

        // If user doesn't exist, create a new user
        if (!userExists) 
            await CreateNewUserAsync(userProfile);

        // Prepare the sign-in DTO for login
        var signInDto = new SignInDto(
            Email: userProfile.Email,
            Password: string.Empty,  // No password for external logins
            SignUpMethod: SignUpMethod.External
        );

        // Use the login service to issue a token
        var token = await connectService.LoginAsync(signInDto);

        return token;
    }
    
    private static async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

        if (payload == null || string.IsNullOrEmpty(payload.Email)) 
            throw new UnauthorizedAccessException("Invalid Google Token");

        return payload;
    }
    
    public class GoogleUserProfile
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Picture { get; set; } = default!;
    }
    
    private async Task<GoogleUserProfile> GetGoogleUserProfileAsync(string accessToken)
    {
        using var httpClient = new HttpClient();
    
        // Send GET request to Google UserInfo API
        var response = await httpClient.GetAsync($"{_configuration["Authentication:Google:UserInfoUrl"]}?access_token={accessToken}");
        response.EnsureSuccessStatusCode();

        // Parse the JSON response
        var content = await response.Content.ReadAsStringAsync();
        var userProfile = JsonConvert.DeserializeObject<GoogleUserProfile>(content);

        return userProfile!;
    }
    
    private async Task CreateNewUserAsync(GoogleUserProfile userProfile)
    {
        var newUser = new IdentityUser
        {
            UserName = userProfile.Email,
            Email = userProfile.Email,
            //Name = userProfile.Name,
            //ProfilePictureUrl = userProfile.Picture
        };

        // Add the new user to the repository and save
        //await identityUserRepository.AddAsync(newUser);
        //await identityUserRepository.SaveChangesAsync();

        await Task.CompletedTask;
    }
}*/