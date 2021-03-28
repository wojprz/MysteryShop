using MysteryShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MysteryShop.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; protected set; }
        public Guid ProductId { get; protected set; }
        public int NumberOfVotes { get; protected set; }
        public int SumOfVotes { get; protected set; }
        public double AvarageOfVotes { get; protected set; }

        public ICollection<User> Users { get; protected set; } = new HashSet<User>();

        public Rating()
        {
            Id = Guid.NewGuid();
            NumberOfVotes = 0;
            SumOfVotes = 0;
            AvarageOfVotes = 0;
        }

        public Rating(Guid productId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            NumberOfVotes = 0;
            SumOfVotes = 0;
            AvarageOfVotes = 0;
        }

        public void AddVote(User user, int ocena)
        {
            if (ocena > 5 || ocena < 1) throw new NewException(NewCodes.WrongRating);
            var userN = Users.FirstOrDefault(u => u == user);
            if (userN != null)
                throw new NewException(NewCodes.UserVoted);
            SumOfVotes = SumOfVotes + ocena;
            NumberOfVotes++;
            AvarageOfVotes = Math.Round((double)SumOfVotes / (double)NumberOfVotes, 2);
            Users.Add(user);
        }
    }
}

