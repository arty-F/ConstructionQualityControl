using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Web.Authentication;
using System.Linq;

namespace ConstructionQualityControl.Web.Extensions
{
    public static class UserExtensions
    {
        static bool IsCustomer(this User user) => user.Roles.Select(r => r.Name).Contains(RolesManager.Customer);
        static bool IsBuilder(this User user) => user.Roles.Select(r => r.Name).Contains(RolesManager.Builder);
        static bool IsAdmin(this User user) => user.Roles.Select(r => r.Name).Contains(RolesManager.Admin);
    }
}
