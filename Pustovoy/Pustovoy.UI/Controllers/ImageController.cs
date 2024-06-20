using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustovoy.UI.Data;
using System.Security.Claims;

namespace Pustovoy.UI.Controllers
{
    public class ImageController(UserManager<ApplicationUser>userManager) : Controller
    {

        public async Task<IActionResult> GetAvatar()
        {
            var email = User.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Avatar != null)
                return File(user.Avatar, user.MimeType);
            var imagePath = Path.Combine("Images", "default-profile-picture.png");
            return File(imagePath, "image/png");
        }

    }
}
