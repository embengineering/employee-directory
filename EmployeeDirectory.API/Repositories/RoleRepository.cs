﻿using System;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;

namespace EmployeeDirectory.API.Repositories
{
    public class RoleRepository : IDisposable
    {
        private readonly AppContext _ctx;

        private readonly RoleStore<ApplicationRole> _roleStore;

        public RoleRepository()
        {
            _ctx = new AppContext();
            _roleStore = new RoleStore<ApplicationRole>();
        }

        public ApplicationRole GetRoleById(string id)
        {
            return _roleStore.Roles.SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _roleStore.Dispose();
        }
    }
}