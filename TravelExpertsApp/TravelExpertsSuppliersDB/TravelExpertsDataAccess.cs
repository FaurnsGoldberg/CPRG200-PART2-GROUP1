using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TravelExpertsSuppliersDB.Models;

namespace TravelExpertsSuppliersDB;

public record SupplierDTO(string SupplierId, string SupplierName, int numContacts); // Data Transfer Object to plug into Forms

/// <summary>
/// Static class to access suppliers and products
/// </summary>
/// <author>Michael and Mikkel</author>
public static class TravelExpertsDataAccess
{
    static int highestSupplierId = 0;
    static int highestContactId = 0;
    private static TravelExpertsContext db = new();
    public static Supplier? FindSupplier(int supplierId)
    {
        TravelExpertsContext db = new();
        try
        {
            return db.Suppliers.Find(supplierId);
        }
        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }

    public static List<SupplierDTO> GetAllSuppliers() =>
        db.Suppliers
            .OrderBy(p => p.SupplierId)
            .Select(p => new SupplierDTO(p.SupplierId.ToString()!, p.SupName!,
                p.SupplierContacts.Count()!))
            .ToList();

    /// <summary>
    /// Creates a unique SupplierID thats at least 1 higher than the highest current supplierID in the DB
    /// </summary>
    /// <returns>supplierID</returns>
    public static int GetSupplierId()
    {
        int query = db.Suppliers.Max(x => x.SupplierId);
        if (query > highestSupplierId) highestSupplierId = query;
        return ++highestSupplierId;
    }


    public static int GetContactId()
    {
        int query = db.SupplierContacts.Max(x => x.SupplierContactId);
        if (query > highestContactId) highestContactId = query;
        return ++highestContactId;
    }
    public static List<SupplierContact> GetSupplierContacts(Supplier supplier) =>
             db.SupplierContacts
            .Where(p => p.SupplierId == supplier.SupplierId)
            .OrderBy(p => p.SupplierContactId)
            .Select(p => p)
            .ToList();

    public static void AddSupplier(Supplier supplier)
    {
        try
        {
            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }
        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }

    public static void UpdateSupplier(Supplier supplier) // Updates the passed supplier in the DB
    {
        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
            var state = db.Entry(supplier).State;
            throw CreateDataAccessException(state);
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }

        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }

    public static void RemoveSupplier(Supplier supplier) // deletes the passed supplier from the DB
    {
        try
        {
            if(supplier.SupplierId > highestSupplierId) highestSupplierId = supplier.SupplierId;
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
            var state = db.Entry(supplier).State;
            throw CreateDataAccessException(state);
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }

        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }

    public static void AddSupplierContact(SupplierContact contact)
    {
        try
        {
            db.SupplierContacts.Add(contact);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }
        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }
    public static void RemoveSupplierContact(Supplier supplier, SupplierContact contact) // deletes the passed supplier from the DB
    {
        try
        {
            if (contact.SupplierContactId > highestContactId) highestContactId = contact.SupplierContactId;
            supplier.SupplierContacts.Remove(contact);
            db.SupplierContacts.Remove(contact);
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
            var state = db.Entry(supplier).State;
            throw CreateDataAccessException(state);
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }

        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }

    public static void UpdateSupplierContact(SupplierContact contact) // Updates the passed supplier contact in the DB
    {
        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
            var state = db.Entry(contact).State;
            throw CreateDataAccessException(state);
        }
        catch (DbUpdateException ex)
        {
            throw CreateDataAccessException(ex);
        }

        catch (SqlException ex)
        {
            throw CreateDataAccessException(ex);
        }
    }
    public static void AddProduct(Product product)
    {
        db.Products.Add(product);
        db.SaveChanges();
    }

    public static void EditProduct(String newName,int ID)
    {
        var query = (from prod in db.Products where prod.ProductId == ID select prod).ToList();
        query[0].ProdName = newName;
        db.SaveChanges();
    }

    public static void DeleteProduct(int ID)
    {
        var query = db.Products.Where(x => x.ProductId == ID).Select(x => x).ToArray();
        if (query.Length >= 1)
        {
            db.Products.Remove(query[0]);
            db.SaveChanges();
        }
    }

    public static int GetProductIdFromName(string name)
    {
        return db.Products.Where(x=>x.ProdName == name).Select(x=>x.ProductId).FirstOrDefault();
    }

    public static int GetSupplierIdFromName(string name)
    {
        return db.Suppliers.Where(x => x.SupName == name).Select(x => x.SupplierId).FirstOrDefault();
    }

    public static void AddProductsSupplier(ProductsSupplier productsSupplier)
    {
        db.ProductsSuppliers.Add(productsSupplier);
        db.SaveChanges();
    }

    public static void DeleteProductsSupplier(int productId, int supplierId)
    {
        var productsSupplier = db.ProductsSuppliers.Where(x => x.ProductId == productId).Where(x => x.SupplierId == supplierId).Select(x => x).FirstOrDefault();
        db.ProductsSuppliers.Remove(productsSupplier);
        db.SaveChanges();
    }

    private static DataAccessException CreateDataAccessException( // Returns a DataAccessException based on the type passed
        EntityState state)
    {
        string msg = "";
        if (state == EntityState.Detached)
            msg = "Another user has deleted that record.";
        else
            msg = "Another user has updated that record.\n" +
            "The current database values will be displayed.";

        return new DataAccessException(msg, "Concurrency Error");
    }

    private static DataAccessException CreateDataAccessException( // Returns a DataAccessException based on the type passed
        DbUpdateException ex)
    {
        var sqlException = (SqlException)ex.InnerException!;
        return CreateDataAccessException(sqlException);
    }

    private static DataAccessException CreateDataAccessException(SqlException ex) // Returns a DataAccessException based on the type passed
    {
        string msg = "";
        foreach (SqlError error in ex.Errors)
        {
            msg += $"ERROR CODE {error.Number}: {error.Message}\n";
        }

        return new DataAccessException(msg, "Database Error");
    }
}
