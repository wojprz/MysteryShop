using MysteryShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int Status { get; protected set; }
        public float Price { get; protected set; }
        public DateTime DateOfAddition { get; protected set; }
        public User User { get; protected set; }
        public virtual Rating Rating { get; protected set; }
        public ICollection<Comment> Comments { get; protected set; }

        protected Product() { }

        public Product(User user, string title, string description, Rating rating, int status = 1)
        {
            Id = Guid.NewGuid();
            User = user;
            Rating = rating;
            Comments = new HashSet<Comment>();
            DateOfAddition = DateTime.Now;
            SetTitle(title);
            SetDescription(description);
            SetStatus(status);
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void SetTitle(string title)
        {
            if (title.Length > 50) throw new NewException(NewCodes.LongTitle);
            if (title.Length < 3) throw new NewException(NewCodes.ShortTitle);
            Title = title;
        }

        public void SetDescription(string description)
        {
            if (description.Length > 2500) throw new NewException(NewCodes.LongDescryption);
            if (description.Length < 5) throw new NewException(NewCodes.ShortDescryption);
            Description = description;
        }

        public void SetStatus(int status)
        {
            if (status == 1 | status == 0) 
                Status = status;
            else
                throw new NewException(NewCodes.WrongStatus);
        }
    }
}

