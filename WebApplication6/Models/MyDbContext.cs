using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipies { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<ContestPost> ContestPosts { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Subscripition { get; set; }


        public ICollection<ContestPost> ContestPosts { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

    }
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string recipeImage { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }

    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FeedbackDes { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
    public class Contest
    {
        public int ContestId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Prize { get; set; }
        public ICollection<ContestPost> ContestPosts { get; set; }

    }
    public class ContestPost
    {
        public int ContestPostId { get; set; }
        public string Recipe { get; set; }
        public string SubmitedAt { get; set; }

        [ForeignKey("Contest")]
        public int ContestId { get; set; }
        public Contest Contest { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
    public class Winner
    {
        public int WinnerId { get; set; }
        public int CreatedAt { get; set; }

        [ForeignKey("ContestPost")]
        public int ContestPostId { get; set; }
        public ContestPost ContestPost { get; set; }
    }
    public class FAQ
    {
        public int FAQId { get; set; }
        public string Ques { get; set; }
        public string Ans { get; set; }
    }
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string ClientImg { get; set; }
        public string Location { get; set; }
    }
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string Monthly { get; set; }
        public string Yearly { get; set; }
    }


}