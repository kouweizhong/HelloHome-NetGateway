using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
    public class ActionConfig : EntityTypeConfiguration<Action>
    {
        public ActionConfig()
        {
            ToTable("Action");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("actionId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TriggerId).HasColumnName("triggerId").IsRequired();
            Property(x => x.Sequence).HasColumnName("sequence").IsRequired();

            HasRequired(x => x.Trigger).WithMany(x => x.Actions).HasForeignKey(x => x.TriggerId);
            HasMany(x => x.Actuators)
                .WithMany(x => x.Actions)
                .Map(x => x.ToTable("ActionPort").MapLeftKey("actionId").MapRightKey("portId"));

            Map<TurnOnAction>(x => x.Requires("type").HasValue("N").HasColumnType("CHAR").HasMaxLength(1));
            Map<TurnOffAction>(x => x.Requires("type").HasValue("F"));
        }
    }
}