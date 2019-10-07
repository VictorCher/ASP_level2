using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;

namespace WebStore.Clients.Users
{
    public class RolesClient : BaseClient, IRoleStore<IdentityRole>
    {
        public RolesClient(IConfiguration configuration) : base(configuration, "api/roles") { }

        #region Implementation of IRoleStore<IdentityRole>

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync(_ServiceAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PutAsync(_ServiceAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/Delete", role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleId", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            role.Name = name;
            await PostAsync($"{_ServiceAddress}/SetRoleName/{name}", role, cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetnormalizedRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            role.NormalizedName = name;
            await PostAsync($"{_ServiceAddress}/SetNormalizedRoleName/{name}", role, cancel);
        }

        public async Task<IdentityRole> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{_ServiceAddress}/FindById/{id}", cancel);

        public async Task<IdentityRole> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{_ServiceAddress}/FindByName/{name}", cancel);

        #endregion
    }
}