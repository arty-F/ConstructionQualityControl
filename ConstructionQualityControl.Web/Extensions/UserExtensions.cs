using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Web.Authentication;

namespace ConstructionQualityControl.Web.Extensions
{
    public static class UserExtensions
    {
        public static bool IsCustomer(this User user) => user.Role == RolesManager.Customer;
        public static bool IsBuilder(this User user) => user.Role == RolesManager.Builder;
        public static bool IsAdmin(this User user) => user.Role == RolesManager.Admin;
    }
}
