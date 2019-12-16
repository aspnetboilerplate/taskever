using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate;
using Taskever.Security.Roles;
using Taskever.Security.Tenants;
using Taskever.Security.Users;

namespace Taskever.Infrastructure.EntityFramework.Migrations.SeedData
{
    public class DefaultTenantRoleAndUserBuilder
    {
        private readonly TaskeverDbContext _context;

        public DefaultTenantRoleAndUserBuilder(TaskeverDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Admin role for tenancy owner

            var adminRoleForTenancyOwner = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == "Admin");
            if (adminRoleForTenancyOwner == null)
            {
                adminRoleForTenancyOwner = _context.Roles.Add(new TaskeverRole() { Name = "Admin", DisplayName = "Admin" });
                _context.SaveChanges();
            }

            //Admin user for tenancy owner

            var adminUserForTenancyOwner = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == "admin");
            if (adminUserForTenancyOwner == null)
            {
                adminUserForTenancyOwner = _context.Users.Add(
                    new TaskeverUser
                    {
                        TenantId = null,
                        UserName = "admin",
                        Name = "System",
                        Surname = "Administrator",
                        NormalizedUserName = "ADMIN",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        NormalizedEmailAddress = "ADMIN@ASPNETBOILERPLATE.COM",
                        IsEmailConfirmed = true,
                        Password = "AOn+T5+T7t+Uk0cEAvfMGfhtsJx/39Y5tV9FjG50fEMd0GWhBHCXpr3r67lk6q9smQ==" //123qwe
                    });

                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(null, adminUserForTenancyOwner.Id, adminRoleForTenancyOwner.Id));

                _context.SaveChanges();
            }

            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == AppConsts.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = _context.Tenants.Add(new TaskeverTenant { TenancyName = AppConsts.DefaultTenantName, Name = AppConsts.DefaultTenantName });
                _context.SaveChanges();
            }

            //Admin role for 'Default' tenant

            var adminRoleForDefaultTenant = _context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "Admin");
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = _context.Roles.Add(new TaskeverRole { TenantId = defaultTenant.Id, Name = "Admin", DisplayName = "Admin" });
                _context.SaveChanges();
            }

            //Admin for 'Default' tenant

            var adminUserForDefaultTenant = _context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "admin");
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = _context.Users.Add(
                    new TaskeverUser
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "admin",
                        Name = "System",
                        Surname = "Administrator",
                        NormalizedUserName = "ADMIN",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        NormalizedEmailAddress = "ADMIN@ASPNETBOILERPLATE.COM",
                        IsEmailConfirmed = true,
                        Password = "AOn+T5+T7t+Uk0cEAvfMGfhtsJx/39Y5tV9FjG50fEMd0GWhBHCXpr3r67lk6q9smQ==" //123qwe
                    });
                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(defaultTenant.Id, adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                _context.SaveChanges();
            }
        }
    }
}
