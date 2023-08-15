using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Models;

namespace TrendyShop.Data
{
    public class DataContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpendableProduct> ExpendableProducts { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<GastronomicProduct> GastronomicProducts { get; set; }
        public DbSet<Incidence> Incidences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ExpendableProductStorage> ExpendableProductStorage { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<StorageRow> Storage { get; set; }
        public DbSet<PurchasePerLodging> PurchasePerLodgings{get; set;}
        public DbSet<RoomProduct> RoomProducts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<StockTaking> Stocks { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<MoneyOperation> MoneyOperations { get; set; }
        public DbSet<HouseExpense> HouseExpenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<FundMoneyOperation> FundMoneyOperations { get; set; }
        public DbSet<YearStatistics> YearStatistics { get; set; }
        public DbSet<GastronomicProductOperation> GastronomicProductOperations { get; set; }
        public DbSet<ExpendableProductOperation> ExpendableProductOperations { get; set; }
        public DbSet<LodgingIncidence> LodgingIncidences { get; set; }
        public DbSet<DailyClosing> DailyClosings { get; set; }
        public DbSet<DateOfClosing> DatesOfClosings { get; set; }
        public DbSet<MOType> MOTypes { get; set; }
        public DbSet<MissingRecord> Missings { get; set; }
        public DbSet<RepositionRecord> Repositions { get; set; }
        public DbSet<ProductOperationType> ProductOperationTypes { get; set; }
        public DbSet<DefaultPrice> SystemDefaultPrices { get; set; }

        public DbSet<EmployeePaymentRecords> EmployeePaymentRecords { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasKey(r => new { r.RoomId, r.Date });
            modelBuilder.Entity<ExpendableProductStorage>().HasKey(p => new { p.Cost, p.Name });
            modelBuilder.Entity<ExpendableProduct>().HasKey(p => new { p.Cost, p.Name});
            modelBuilder.Entity<Package>().HasKey(p => new { p.Cost, p.Name});
            modelBuilder.Entity<GastronomicProduct>().HasKey(gp => new { gp.Cost, gp.Name });
            modelBuilder.Entity<Lodging>().HasKey(l => new { l.RoomId, l.Date });
            modelBuilder.Entity<PurchasePerLodging>().HasKey (pl => new { pl.Name, pl.Cost, pl.RoomId, pl.Date});
            modelBuilder.Entity<RoomProduct>().HasKey(rp => new { rp.Name, rp.Cost, rp.RoomId });
            modelBuilder.Entity<StorageRow>().HasKey(sr => new { sr.Name, sr.Cost });
            modelBuilder.Entity<YearStatistics>().HasNoKey();
            modelBuilder.Entity<GastronomicProductOperation>().HasKey(p => new { p.Cost, p.Name, p.Date });
            modelBuilder.Entity<ExpendableProductOperation>().HasKey(p => new { p.Cost, p.Name, p.Date });
            modelBuilder.Entity<LodgingIncidence>().HasKey(li => new { li.RoomId, li.Date, li.IncidenceId });
            modelBuilder.Entity<DailyClosing>().HasKey(dc => new { dc.Date, dc.Name, dc.Cost });
            modelBuilder.Entity<MissingRecord>().HasKey(mr => new { mr.Date, mr.Cost, mr.Name });
            modelBuilder.Entity<RepositionRecord>().HasKey(r => new { r.Date, r.Cost, r.Name });
        }

    }
}
