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

        public ICollection<Guid> Users { get; protected set; } = new HashSet<Guid>();

        public Rating()
        {
            Id = Guid.NewGuid();
        }

        public Rating(Guid productId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
        }

        public void AddVote(Guid userID, int ocena)
        {
            if (ocena > 5 || ocena < 1) throw new Exception();
            var user = Users.FirstOrDefault(u => u == userID);
            if (user != null)
                throw new Exception();
            SumOfVotes = SumOfVotes + ocena;
            NumberOfVotes++;
            AvarageOfVotes = Math.Round((double)SumOfVotes / (double)NumberOfVotes, 2);
            Users.Add(userID);
        }
    }
}

