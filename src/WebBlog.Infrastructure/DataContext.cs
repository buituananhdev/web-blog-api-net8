using Microsoft.EntityFrameworkCore;
using WebBlog.Domain.Entities;

namespace WebBlog.Infrastructure
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Conversation> Conversations { get; set; }

        public virtual DbSet<Follower> Followers { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Token> Tokens { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserConversation> UserConversations { get; set; }

        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>()
                .HasOne(f => f.FollowerUser)
                .WithMany(u => u.FollowerFollowerUsers)
                .HasForeignKey(f => f.FollowerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.User)
                .WithMany(u => u.FollowerUsers)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
