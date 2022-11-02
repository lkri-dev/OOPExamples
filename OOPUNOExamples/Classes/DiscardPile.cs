﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPUNOExamples.Classes
{
    internal class DiscardPile : CardCollection
    {
        internal DiscardPile(Card topCard)
        {
            this.Cards = new List<Card>();
            this.Cards.Add(topCard);
        }
        internal DiscardPile()
        {
            this.Cards = new List<Card>();
        }

        internal Card GetTopCard()
        {
            return Cards.Last();
        }

        internal void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}