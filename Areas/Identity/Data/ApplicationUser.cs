using Microsoft.AspNetCore.Identity;
using System;

namespace ColorApplication.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the IdentitytUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
