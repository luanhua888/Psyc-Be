using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PsychologicalCounseling.Models
{
    public partial class PsychologicalCouselingContext : DbContext
    {
        public PsychologicalCouselingContext()
        {
        }

        public PsychologicalCouselingContext(DbContextOptions<PsychologicalCouselingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleZodiac> ArticleZodiacs { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Consultant> Consultants { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DailyHoroscope> DailyHoroscopes { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<PlanetHouse> PlanetHouses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ReceiveAccount> ReceiveAccounts { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<SlotBooking> SlotBookings { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<SpecializationType> SpecializationTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Zodiac> Zodiacs { get; set; }
        public virtual DbSet<ZodiacHouse> ZodiacHouses { get; set; }
        public virtual DbSet<ZodiacPlanet> ZodiacPlanets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=PsychologicalCouseling.mssql.somee.com;packet size=4096;user id=ttrungta2031_SQLLogin_5;pwd=snv4p3gu5n;data source=PsychologicalCouseling.mssql.somee.com;persist security info=False;initial catalog=PsychologicalCouseling");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.CreateDay).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<ArticleZodiac>(entity =>
            {
                entity.ToTable("ArticleZodiac");

                entity.Property(e => e.CreateDay).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleZodiacs)
                    .HasForeignKey(d => d.Articleid)
                    .HasConstraintName("FK_ArticleZodiac_Article");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.ArticleZodiacs)
                    .HasForeignKey(d => d.Zodiacid)
                    .HasConstraintName("FK_ArticleZodiac_Zodiac");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Duration)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Feedback).HasMaxLength(255);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Booking_Customers");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Booking_Payment");
            });

            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.ToTable("Consultant");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.AvartarUrl).IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.HourBirth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.MinuteBirth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondBirth)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DailyHoroscope>(entity =>
            {
                entity.ToTable("DailyHoroscope");

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.GoodTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.ShouldNotThing).HasMaxLength(255);

                entity.Property(e => e.ShouldThing).HasMaxLength(255);

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.DailyHoroscopes)
                    .HasForeignKey(d => d.ZodiacId)
                    .HasConstraintName("FK_DailyHoroscope_Zodiac");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.ToTable("Deposit");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.ReceiveAccount)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.ReceiveAccountid)
                    .HasConstraintName("FK_Deposit_ReceiveAccount");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK_Deposit_Wallet");
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.ToTable("House");

                entity.Property(e => e.Element).HasMaxLength(25);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Tag).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CreateDay).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customers");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Order_Payment");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_OrderDetail_Item");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.ToTable("Planet");

                entity.Property(e => e.Element).HasMaxLength(25);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Tag).HasMaxLength(50);
            });

            modelBuilder.Entity<PlanetHouse>(entity =>
            {
                entity.ToTable("PlanetHouse");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.PlanetHouses)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("FK_PlanetHouse_House");

                entity.HasOne(d => d.Planet)
                    .WithMany(p => p.PlanetHouses)
                    .HasForeignKey(d => d.PlanetId)
                    .HasConstraintName("FK_PlanetHouse_Planet");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreateDay).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_Item_Shop1");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ProductTypes)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemType_Item");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.ProductTypes)
                    .HasForeignKey(d => d.ZodiacId)
                    .HasConstraintName("FK_ItemType_Zodiac");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.HourBirth)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MinuteBirth)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Profile_Customers");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("FK_Profile_House");

                entity.HasOne(d => d.Planet)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.PlanetId)
                    .HasConstraintName("FK_Profile_Planet");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.ZodiacId)
                    .HasConstraintName("FK_Profile_Zodiac");
            });

            modelBuilder.Entity<ReceiveAccount>(entity =>
            {
                entity.ToTable("ReceiveAccount");

                entity.Property(e => e.BankNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.QrCode).IsUnicode(false);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.ConsultantId)
                    .HasConstraintName("FK_Shop_Consultant");
            });

            modelBuilder.Entity<SlotBooking>(entity =>
            {
                entity.HasKey(e => e.SlotId);

                entity.ToTable("SlotBooking");

                entity.Property(e => e.DateBooking).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStart)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.SlotBookings)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_SlotBooking_Booking");

                entity.HasOne(d => d.BookingNavigation)
                    .WithMany(p => p.SlotBookings)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_SlotBooking_Consultant");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.Specializations)
                    .HasForeignKey(d => d.ConsultantId)
                    .HasConstraintName("FK_Specialization_Consultant");

                entity.HasOne(d => d.SpecializationType)
                    .WithMany(p => p.Specializations)
                    .HasForeignKey(d => d.SpecializationTypeId)
                    .HasConstraintName("FK_Specialization_SpecializationType");
            });

            modelBuilder.Entity<SpecializationType>(entity =>
            {
                entity.ToTable("SpecializationType");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DateCreate).HasColumnType("date");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Transaction)
                    .HasForeignKey<Transaction>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Consultant");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Transaction_Payment");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK_Transaction_Wallet");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.HistoryTrans).HasColumnType("date");

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.ConsultantId)
                    .HasConstraintName("FK_Wallet_Consultant1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Wallet_Customers1");
            });

            modelBuilder.Entity<Zodiac>(entity =>
            {
                entity.ToTable("Zodiac");

                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.DescriptionShort).HasMaxLength(200);

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ZodiacHouse>(entity =>
            {
                entity.ToTable("ZodiacHouse");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.ZodiacHouses)
                    .HasForeignKey(d => d.HouseId)
                    .HasConstraintName("FK_ZodiacHouse_House");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.ZodiacHouses)
                    .HasForeignKey(d => d.ZodiacId)
                    .HasConstraintName("FK_ZodiacHouse_Zodiac");
            });

            modelBuilder.Entity<ZodiacPlanet>(entity =>
            {
                entity.ToTable("ZodiacPlanet");

                entity.HasOne(d => d.Planet)
                    .WithMany(p => p.ZodiacPlanets)
                    .HasForeignKey(d => d.PlanetId)
                    .HasConstraintName("FK_ZodiacPlanet_Planet");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.ZodiacPlanets)
                    .HasForeignKey(d => d.ZodiacId)
                    .HasConstraintName("FK_ZodiacPlanet_Zodiac");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
