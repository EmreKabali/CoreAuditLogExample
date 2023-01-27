using System;
using CoreAuditLogExample.Enums;
using CoreAuditLogExample.Utils;
using Microsoft.EntityFrameworkCore;

namespace CoreAuditLogExample.Models
{
	public class ProductDbContext:DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\Kabali;Database=CoreAuditLogExample;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            //normal savechanges
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.UserId = entry.Property("CreatedBy").CurrentValue != null ? entry.Property("CreatedBy").CurrentValue.ToString() : "Null";
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }

        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Product> Product { get; set; }
	}
}

