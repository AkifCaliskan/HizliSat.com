using Microsoft.AspNetCore.Authorization;
using Sahibinden.Business.Abstract;
using Sahibinden.Entities.Enums;

namespace Sahibinden.AdminPanel.Middleware
{
    public class AdminAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;

        public AdminAuthMiddleware(RequestDelegate next, ICacheService cacheService)
        {
            _next = next;
            _cacheService = cacheService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            // 📌 1️⃣ Eğer istek /Auth sayfasına gidiyorsa middleware işlemi atla
            if (context.Request.Path.StartsWithSegments("/Auth"))
            {
                await _next(context);
                return;
            }

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            var userIdClaim = context.User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                context.Response.Redirect("/Auth/Index");
                return; // 🔥 Sonsuz döngüyü engellemek için burada işlemi durduruyoruz!
            }

            if (!_cacheService.TryGetUser(userId, out var cachedUser))
            {
                context.Response.Redirect("/Auth/Index");
                return; // 🔥 Sonsuz döngüyü engellemek için burada işlemi durduruyoruz!
            }

            if (cachedUser.User.UserType != UserType.Admin && cachedUser.User.UserType != UserType.SuperAdmin)
            {
                context.Response.Redirect("/Pages/NotAuthorized");
                return; // 🔥 Sonsuz döngüyü engellemek için burada işlemi durduruyoruz!
            }

            await _next(context);
        }
    }
}
