using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPackageData;
using TravelPackageData.Models;

namespace TravelPackageData
{

    public record ProductSupplierDTO(int ProductSupplierId, int ProductId, string ProductName, int SupplierId, string SupplierName)
    {
        public override string ToString()
        {
            return $"{ProductName} - {SupplierName}";
        }
    }
    public class TravelExpertsDataConnection
    {
        private TravelExpertsContext db = new(); // The Database
        public Package? selectedPackage;
        public Product? FindProduct(int productId)
        {

            return db.Products.Find(productId);

        }
        public List<ProductSupplierDTO> GetAllProductsAndSuppliers() =>
        (from p in db.Products
         join ps in db.ProductsSuppliers on p.ProductId equals ps.ProductId
         join s in db.Suppliers on ps.SupplierId equals s.SupplierId
         orderby p.ProductId
         select new ProductSupplierDTO(
             ps.ProductSupplierId,
             p.ProductId,
             p.ProdName,
             s.SupplierId,
             s.SupName
         )).ToList();

        public List<ProductSupplierDTO> FilterProductsAndSuppliersByProductType(int productId) =>
        (from p in db.Products
        join ps in db.ProductsSuppliers on p.ProductId equals ps.ProductId
        join s in db.Suppliers on ps.SupplierId equals s.SupplierId
        orderby ps.ProductSupplierId
        where p.ProductId == productId
        select new ProductSupplierDTO(
            ps.ProductSupplierId,
            p.ProductId,
            p.ProdName,
            s.SupplierId,
            s.SupName
        )).ToList();

        public List<ProductSupplierDTO> GetProductsAndSuppliersOfSelectedPackage(Package selectedPackage) =>
            (from p in db.Products
             join ps in db.ProductsSuppliers on p.ProductId equals ps.ProductId
             join s in db.Suppliers on ps.SupplierId equals s.SupplierId
             join pps in db.PackagesProductsSuppliers on ps.ProductSupplierId equals pps.ProductSupplierId
             join pk in db.Packages on pps.PackageId equals pk.PackageId
             where pk.PackageId == selectedPackage!.PackageId
             orderby p.ProductId
             select new ProductSupplierDTO(
                 ps.ProductSupplierId,
                 p.ProductId,
                 p.ProdName,
                 s.SupplierId,
                 s.SupName
             )).ToList();

        public List<ProductSupplierDTO> GetOnlyNewProducts(List<ProductSupplierDTO> initialProds, List<ProductSupplierDTO> newProds)
        {
            var newProducts = from np in newProds
                              where !initialProds.Any(ip => ip.ProductSupplierId == np.ProductSupplierId)
                              select np;

            return newProducts.ToList();
        }

        public List<ProductSupplierDTO> GetOnlyOldProducts(List<ProductSupplierDTO> initialProds, List<ProductSupplierDTO> newProds)
        {
            var oldProducts = from ip in initialProds
                              where !newProds.Any(np => np.ProductSupplierId == ip.ProductSupplierId)
                              select ip;

            return oldProducts.ToList();
        }
    }
}
