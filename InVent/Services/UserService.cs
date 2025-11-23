using InVent.Components.Account;
using InVent.Data;
using Microsoft.AspNetCore.Components;

namespace InVent.Services
{
    public class UserService()
    {
        [Inject]
        private IdentityUserAccessor userAccessor { get; set; }

        //[CascadingParameter]
        //private HttpContext HttpContext { get; set; } = default!;



        public async Task<ApplicationUser> GetUser(HttpContext HttpContext)
        {
            var res = await userAccessor.GetRequiredUserAsync(HttpContext);
            return res;
        }

        //public void SetUser (ApplicationUser? user)
        //{
        //    this.User = user;
        //}
        //public ApplicationUser? GetUser()
        //{
        //    return this.User ;
        //}
    }
}
