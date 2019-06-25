using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ShoppingOnline.Models;

[assembly: OwinStartupAttribute(typeof(ShoppingOnline.Startup))]
namespace ShoppingOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        public void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // in startup creating frist ADMIN and creating a default ADMIN Users
            if (!roleManager.RoleExists("ADMIN"))
            {
                // đầu tiên tạo quyền cao nhất admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ADMIN";
                roleManager.Create(role);
                // tạo super admin, người có quyền cao nhất
                var user = new ApplicationUser();
                user.UserName = "admin@giangdayit.com";
                user.Email = "admin@giangdayit.com";
                string userPWD = "123456";
                var chkUser = UserManager.Create(user, userPWD);
                // thêm user đầu tiên vào là administrator
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "ADMIN");
                }
            }
            // in startup creating frist MANAGER and creating a default ADMIN MANAGER
            if (!roleManager.RoleExists("MANAGER"))
            {
                // Đầu tiên, tạo quyền Manager
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MANAGER";
                roleManager.Create(role);
                // tạo tài khoản manager thuộc quyền MANAGER
                var uManager = new ApplicationUser();
                uManager.UserName = "manager@giangdayit.com";
                uManager.Email = "manager@giangdayit.com";
                string userManagerPWD = "123456";
                var chkUserManager = UserManager.Create(uManager, userManagerPWD);
                // thêm user manager
                if (chkUserManager.Succeeded)
                {
                    var result = UserManager.AddToRole(uManager.Id, "MANAGER");
                }
                // tạo tài khoan editor thuộc quyền MANAGER
                var userEditor = new ApplicationUser();
                userEditor.UserName = "editor@giangdayit.com";
                userEditor.Email = "editor@giangdayit.com";
                string userEditorPWD = "123456";
                var chkUserEditor = UserManager.Create(userEditor, userEditorPWD);
                // thêm user editor
                if (chkUserEditor.Succeeded)
                {
                    var result = UserManager.AddToRole(userEditor.Id, "MANAGER");
                }
            }
            // in startup creating frist MANAGER
            if (!roleManager.RoleExists("MEMBER"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "MEMBER";
                roleManager.Create(role);
            }
        }
    }
}
