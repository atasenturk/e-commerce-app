using E_CommerceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceApp.Infrastructure.Persistence.EntityConfigurations
{
    public class ShoppingCartEntityConfiguration : BaseEntityConfiguration<ShoppingCart>
    {
        public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            base.Configure(builder);

            builder.ToTable("shopping_carts");
        }
    }
}
