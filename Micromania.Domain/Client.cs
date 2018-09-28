﻿using Micromania.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micromania.Domain
{
    public class Client : AggregateRoot
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }        
        public virtual int Points { get; protected set; }
        public virtual Status Status { get; protected set; }
        public virtual IList<Purchase> Purchases { get; protected set; } = new List<Purchase>();

        private int pointsToDiscount;

        public virtual int PointsToDiscount
        {
            get { return pointsToDiscount; }

            set
            {
                if (value < 0 || value > 2000)
                    throw new InvalidOperationException("Invalid value");
                pointsToDiscount = value;
            }
        }

        protected Client()
        {
        }

        private Client(string firstName, string lastName)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Status = Status.IsMegaCard;            
        }

        public static Client Create(string firstName, string lastName)
        {
            return new Client(firstName, lastName);
        }

        public virtual void BuyGame(Game game)
        {
            var purchase = Purchase.Create(game);

            //Buying a game increases the number of points on your card
            Points += game.Points;

            PointsToDiscount = 2000 - Points;

            Purchases.Add(purchase);
        }        
    }

    public enum Status
    {
        IsMegaCard,
        IsClassic,
        IsStar,
        IsPremium
    }
}
