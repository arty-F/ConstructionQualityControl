using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Web.Authentication;

namespace ConstructionQualityControl.Web.Extensions
{
    public static class UserExtensions
    {
        static bool IsCustomer(this User user) => user.Role == RolesManager.Customer;
        static bool IsBuilder(this User user) => user.Role == RolesManager.Builder;
        static bool IsAdmin(this User user) => user.Role == RolesManager.Admin;
    }
}
