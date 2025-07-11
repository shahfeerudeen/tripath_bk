using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tripath.Models
{
    public class UserManagementResponse
    {
        public string? UserId { get; set; }
        public string? UserMasterId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserMobileNo { get; set; }
      //  public string? OrganizationId { get; set; }
       public string? OrganizationName { get; set; }
        public string? UserBearerToken { get; set; }
        public string? UserMasterRole { get; set; }
    }
}
