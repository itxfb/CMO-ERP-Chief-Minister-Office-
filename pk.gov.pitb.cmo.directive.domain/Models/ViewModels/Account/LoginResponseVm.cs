

using pk.gov.pitb.cmo.directive.domain.Models.Common;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account
{
    public class LoginResponseVm : IdObject
    {
        
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public object UserRolesPermissions { get; set; }
        public object UserPermissions { get; set; }
        

    }

    public class AccessibleRoutes
    {
        public string Route { get; set; }
        public string Type { get; set; } //Module - Component

        public List<string> Permissions { get; set; }


        /*
         * AppFeature
         * Dashboard
         * Directive
         * - Add, Edit, Delte
         *
         * AppFeature:Dashboard
         * AppFeature:Directive:Listing
         * AppFeature:Directive:Add
         * AppFeature:Directive:Edit
         *
         *
         *
         *
         *
         */
    }
}
