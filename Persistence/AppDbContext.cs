using Microsoft.EntityFrameworkCore;
using BidingManagementSystem.Domain.Entities;



namespace BidingManagementSystem.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<BidDocument> BidDocuments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<TenderDocument> TenderDocuments { get; set; }


    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
    .HasKey(u => u.UserId); 
    modelBuilder.Entity<User>()
    .Property(u => u.UserId)
    .ValueGeneratedOnAdd(); 
 modelBuilder.Entity<Tender>()
        .HasKey(t => t.TenderId);

    modelBuilder.Entity<Tender>()
        .Property(t => t.TenderId)
        .ValueGeneratedOnAdd();

  modelBuilder.Entity<Bid>()
        .HasKey(b => b.BidId);

    modelBuilder.Entity<Bid>()
        .Property(b => b.BidId)
        .ValueGeneratedOnAdd();

modelBuilder.Entity<BidDocument>()
        .HasKey(d => d.BidDocumentId);

    modelBuilder.Entity<BidDocument>()
        .Property(d => d.BidDocumentId)
        .ValueGeneratedOnAdd();

    // Evaluation
    modelBuilder.Entity<Evaluation>()
        .HasKey(e => e.EvaluationId);

    modelBuilder.Entity<Evaluation>()
        .Property(e => e.EvaluationId)
        .ValueGeneratedOnAdd();

                modelBuilder.Entity<User>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Tender>()
                .HasMany(t => t.Bids)
                .WithOne(b => b.Tender)
                .HasForeignKey(b => b.TenderId);

            modelBuilder.Entity<Bid>()
                .HasMany(b => b.Documents)
                .WithOne(d => d.Bid)
                .HasForeignKey(d => d.BidId);

            modelBuilder.Entity<Bid>()
                .HasMany(b => b.Evaluations)
                .WithOne(e => e.Bid)
                .HasForeignKey(e => e.BidId);
            
            modelBuilder.Entity<Tender>()
    .HasMany(t => t.Documents)
    .WithOne(d => d.Tender)
    .HasForeignKey(d => d.TenderId)
    .OnDelete(DeleteBehavior.Cascade); 

            
            
            
            
            
            
            }
}
}