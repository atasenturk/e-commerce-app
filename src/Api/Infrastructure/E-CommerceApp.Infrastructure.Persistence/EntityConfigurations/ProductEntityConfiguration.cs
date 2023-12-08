using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductEntityConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("products", ECommerceDbContext.DEFAULT_SCHEMA);
        }

    }
}
