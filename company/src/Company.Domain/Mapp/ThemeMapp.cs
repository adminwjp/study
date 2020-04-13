using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Company.Domain.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Company.Domain.Mapp
{
    public class ThemeMapp : IEntityTypeConfiguration<ThemeInfo>
    {
        public void Configure(EntityTypeBuilder<ThemeInfo> builder)
        {
            builder.HasKey(it=>it.Id);
            builder.Property(it => it.Id).HasColumnName("id");
            builder.Property(it => it.CreateDate).HasColumnName("create_date");
            builder.Property(it => it.Href).HasColumnName("href");
            builder.Property(it => it.ModifyDate).HasColumnName("modify_date").IsRequired();
        }
    }
}
