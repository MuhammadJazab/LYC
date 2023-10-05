using LYC.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace LYC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<Allergy> Allergy { get; set; }
        public DbSet<BookingExtra> BookingExtra { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentUserAssociation> DepartmentUserAssociation { get; set; }
        public DbSet<EmergencyContact> EmergencyContact { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Finance> Finance { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodIngredient> FoodIngredient { get; set; }
        public DbSet<FoodNutration> FoodNutration { get; set; }
        public DbSet<FoodType> FoodType { get; set; }
        public DbSet<HistoryTrack> HistoryTrack { get; set; }
        public DbSet<HistoryType> HistoryType { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MealOrder> MealOrder { get; set; }
        public DbSet<MealType> MealType { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Nursery> Nursery { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductBranchAssociation> ProductBranchAssociation { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomServiceAssociation> RoomServiceAssociation { get; set; }
        public DbSet<RoomUnavailability> RoomUnavailability { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceDayAssociation> ServiceDayAssociation { get; set; }
        public DbSet<StaffBranchAssociation> StaffBranchAssociation { get; set; }
    }
}