using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Infrastracture.identity
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        public DbSet<Address> Address { get; set; }

        protected IdentityContext()
        {
        }
    }
}
