using Microsoft.EntityFrameworkCore;
using WebBlog.Domain.Entities;
using WebBog.Domain.Entities;

namespace WebBog.Infrastructure
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
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Comments__C3B4DFAA0BE8A289");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CommentID");
                entity.Property(e => e.CommentText)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.CommentType)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PostID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Comments__PostID__123EB7A3");

                entity.HasOne(d => d.User).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Comments__UserID__114A936A");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Conversa__C050D8971D24ED06");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ConversationID");
                entity.Property(e => e.ConversationName).HasMaxLength(50);
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Follower__E85940F9272F7E33");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FollowerID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FollowerUserID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.FollowerUser).WithMany(p => p.FollowerFollowerUsers)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Followers__Follo__160F4887");

                entity.HasOne(d => d.User).WithMany(p => p.FollowerUsers)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Followers__UserI__151B244E");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Messages__C87C037C33563219");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MessageID");
                entity.Property(e => e.Content).HasColumnType("text");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ConversationID");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.SenderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SenderID");

                entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Conver__208CD6FA");

                entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Messages__Sender__1F98B2C1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Posts__AA126038F3C4B4DD");

                entity.HasIndex(e => e.Title, "idx_Posts_Title");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PostID");
                entity.Property(e => e.PostType)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Thumbnail).HasMaxLength(200);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.Posts)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Posts__UserID__0E6E26BF");

                entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                    .UsingEntity<Dictionary<string, object>>(
                        "PostTag",
                        r => r.HasOne<Tag>().WithMany()
                            .HasForeignKey("Id")
                            .HasConstraintName("FK__PostTags__TagID__29221CFB"),
                        l => l.HasOne<Post>().WithMany()
                            .HasForeignKey("Id")
                            .HasConstraintName("FK__PostTags__PostID__282DF8C2"),
                        j =>
                        {
                            j.HasKey("Id", "Id").HasName("PK__PostTags__7C45AF9C6C8BF9FC");
                            j.ToTable("PostTags");
                            j.IndexerProperty<string>("Id")
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnName("PostID");
                            j.IndexerProperty<string>("Id")
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnName("TagID");
                        });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tags__657CFA4C092E585F");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TagID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tokens__3214EC0767666394");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(30);

                entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Tokens)
                    .HasPrincipalKey(p => p.Email)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Tokens__Email__0B91BA14");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__1788CCACDB11A4B0");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B8061B03").IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
                entity.Property(e => e.Avatar).HasMaxLength(100);
                entity.Property(e => e.Describe).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(30);
                entity.Property(e => e.Fullname).HasMaxLength(30);
                entity.Property(e => e.Password).HasMaxLength(500);
            });

            modelBuilder.Entity<UserConversation>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ConversationID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Conversation).WithMany()
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__UserConve__Conve__236943A5");

                entity.HasOne(d => d.User).WithMany()
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__UserConve__UserI__22751F6C");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Votes__52F015E2D479287D");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VoteID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PostID");
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post).WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Votes__PostID__19DFD96B");

                entity.HasOne(d => d.User).WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Votes__UserID__18EBB532");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
