using E_CommerceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Infrastructure.Persistence.EntityConfigurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(q=>q.Id);
            builder.Property(q=>q.Id).ValueGeneratedOnAdd();
            builder.Property(q=>q.CreatedDate).ValueGeneratedOnAdd();
        }
    }
}
