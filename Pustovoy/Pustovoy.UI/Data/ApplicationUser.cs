using Microsoft.AspNetCore.Identity;

namespace Pustovoy.UI.Data
{
	public class ApplicationUser : IdentityUser
	{
		public byte[]? AvatarImage { get; set; }
	}
}
