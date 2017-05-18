using EntityFramework.Filters;

namespace JonkerBudget.EntityFramework
{
    using DragonFire.Core.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Domain.Identity;
    using Auditing;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Domain.Entities;
    public class DataContext : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim>
    {
        public DataContext()
            : base("name=DataContextConnectionString")
        {
        }
        public static DataContext Create()
        {
            return new DataContext();
        }

        public virtual DbSet<UserManager> UserManagers { get; set; }
        public virtual DbSet<EscalationDetail> EscalationDetails { get; set; }
        public virtual DbSet<NotificationTask> NotificationTasks { get; set; }
        public virtual DbSet<NotificationTaskUpdate> NotificationTaskUpdates { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Auditing.Audit> Audits { get; set; }
        public virtual DbSet<AuditDetail> AuditDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Register filter interceptor
            DbInterception.Add(new FilterInterceptor());

            modelBuilder.Conventions.Add(
                    FilterConvention.Create<ISoftDelete, bool>("IsDeleted", (e, isDeleted) => e.IsDeleted == false));

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");            

            modelBuilder.Entity<User>().Property(r => r.Id);
            modelBuilder.Entity<UserClaim>().Property(r => r.Id);
            modelBuilder.Entity<Role>().Property(r => r.Id);
            modelBuilder.Entity<UserClaim>().Property(r => r.Id);
            modelBuilder.Entity<UserLogin>().Property(r => r.Id);
            modelBuilder.Entity<UserRole>().Property(r => r.Id);

            //Add index to the ObjectId Audit
            modelBuilder.Entity<Auditing.Audit>()
                .Property(e => e.ObjectId)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute()));
        }
    }

    public class JonkerBudgetContextConfiguration : DbConfiguration
    {
        public JonkerBudgetContextConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new DataContextExecutionStrategy());
        }
    }
}