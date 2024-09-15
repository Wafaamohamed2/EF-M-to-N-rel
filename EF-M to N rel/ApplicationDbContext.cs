

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEF
{
    public class ApplicationDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EFCore; Integrated Security = True");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Post>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(T => T.Posts)
            //    .UsingEntity(j => j.ToTable("PostsTagsTest"));




            // OR


            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(T => T.Posts)
                .UsingEntity<PostTag>(
                   j => j
                       .HasOne(pt => pt.Tag)
                       .WithMany(t => t.PostTags)
                       .HasForeignKey(pt => pt.TagId),

                   j => j
                        .HasOne(j => j.Post)
                        .WithMany(t => t.PostTags)
                        .HasForeignKey(j => j.PostId),
                   j =>
                   {
                     
                       j.HasKey(pt =>new { pt.PostId , pt.TagId});
                   }
                        
                );

          
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
    }


    // Many To Many Relation

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<PostTag> PostTags { get; set; }

        public ICollection<Tag> Tags { get; set; }

    }

    public class Tag 
    {
        public string TagId { get; set; }

        public ICollection<Post> Posts { get; set; }
        public List<PostTag>  PostTags { get; set; }


    }

    public class PostTag
    {
        public int PostId { get; set; }

        public Post Post { get; set; }

        public string TagId { get; set; }

        public Tag Tag { get; set; }    

        public DateTime DateTime { get; set; }
    }
}
