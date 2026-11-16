using BookRentalSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {            
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Author).HasMaxLength(100);
            builder.Property(b => b.Title).HasMaxLength(200);
            builder.Property(b => b.ISBN).HasMaxLength(20);
            builder.HasIndex(b => b.ISBN).IsUnique();
            builder.Property(b => b.AvailableCopies).IsConcurrencyToken();
        }
    }
}
