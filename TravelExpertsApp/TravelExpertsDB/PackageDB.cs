using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDB
{
    public static class PackageDB
    {
        /// <summary>
        /// Returns a list of all packages on the database.
        /// </summary>
        /// <param name="db">db context</param>
        /// <returns>List of packages from database</returns>
        public static List<Package> GetPackages(TravelExpertsContext db)
        {
            List<Package> packages = db.Packages.ToList();
            return packages;
        }

        /// <summary>
        /// Returns a package with the matching package id
        /// </summary>
        /// <param name="db">db context</param>
        /// <param name="id">package id</param>
        /// <returns>Package object matching the provided package id</returns>
        public static Package GetPackageById(TravelExpertsContext db, int id)
        {
            Package? pck = db.Packages.FirstOrDefault(p => p.PackageId == id);
            return pck!;
        }
    }
}
