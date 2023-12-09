using E_CommerceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceApp.Infrastructure.Persistence.EntityConfigurations;

public class ShoppingCartItemEntityConfiguration : BaseEntityConfiguration<ShoppingCartItem>
{
    public override void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("shopping_cart_items");

        builder.HasOne(si => si.ShoppingCart)
            .WithMany(s => s.ShoppingCartItems)
            .HasForeignKey(si => si.ShoppingCartId);

        builder.HasOne(si => si.Product)
            .WithMany(p => p.ShoppingCartItems)
            .HasForeignKey(si => si.ProductId);

    }
}