using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IPackagesRepository
{
    Task<List<PackagesTable>> GetAllPackagesAsync();
    Task<PackagesTable> GetPackageByCommandNameAsync(string commandName);
    Task AddPackageAsync(PackagesTable package);
    Task UpdatePackageAsync(PackagesTable package);
    Task DeletePackageAsync(int id);
}