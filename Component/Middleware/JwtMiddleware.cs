namespace TaiLieuWebsiteBackend.Component.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtTokenUtil _jwtTokenUtil;

        public JwtMiddleware(RequestDelegate next, JwtTokenUtil jwtTokenUtil)
        {
            _next = next;
            _jwtTokenUtil = jwtTokenUtil;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var principal = _jwtTokenUtil.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }
            catch
            {
                // Do nothing if token validation fails
                // User is not attached to context so request won't have access to secure routes
            }
        }
    }
}
