using HOVLaneViolation.Entities;
using Microsoft.EntityFrameworkCore;

namespace HOVLaneViolation.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<HOVMasterData> HOVMasters => Set<HOVMasterData>();
    public DbSet<HOVCustomerData> HOVCustomers => Set<HOVCustomerData>();
    public DbSet<HOVTransactionData> HOVTransactions => Set<HOVTransactionData>();
    public DbSet<HOVRule> HOVRules => Set<HOVRule>();
}
