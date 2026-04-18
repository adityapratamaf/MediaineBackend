namespace Mediaine.API.Extensions;

public static class CookieExtensions
{
    public static void SetAccessTokenCookie(this HttpResponse response, string token)
    {
        response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // ubah true saat production HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(15)
        });
    }

    public static void SetRefreshTokenCookie(this HttpResponse response, string token)
    {
        response.Cookies.Append("refresh_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // ubah true saat production HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }

    public static void ClearAuthCookies(this HttpResponse response)
    {
        response.Cookies.Delete("access_token");
        response.Cookies.Delete("refresh_token");
    }
}