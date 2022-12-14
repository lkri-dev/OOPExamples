using OOPUNOExamples;
using OOPUNOExamples.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OOPUNOExamples.Classes
{
    public class Player : CardCollection
    {
        public Player(string name)
        {
            ID = IDCounter++;
            this.Name = name;
            if (Player.Deck == null)
            {
                DeckCreator deckCreator = new DeckCreator();
                Player.Deck = deckCreator.FactoryMethodNormalDeck();
            }
            DrawCards(7);
        }
        private static int IDCounter = 0;
        internal int ID { get; private set; }
        internal string Name { get; private set; }
        public int GetHandSize()
        {
            return Cards.Count;
        }
        public static Deck Deck;
        internal bool Uno
        {
            get => default;
            set
            {
            }
        }
        private void PrintCardsInHand()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < Cards.Count; i++)
            {
                if (Cards[i] == Cards.Last())
                {
                    sb.AppendFormat("{0}: {1}", i, Cards[i].ToString());
                }
                else
                {
                    sb.AppendFormat("{0}: {1}\n", i, Cards[i].ToString());
                }
            }
            Console.WriteLine(sb.ToString());
        }
        internal Card ChooseCard()
        {
            Card chosenCard = null;
            Card cardObj = null;

            string topCard = Deck.DiscardPile.GetTopCard().ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("The top card {0}; ", topCard);
            sb.Append("If there are no card that you can or want to play press 0.");

            CardMenu menu = new CardMenu(Name, sb.ToString(), Cards);
            chosenCard = menu.MenuControl();
            return chosenCard;
        }
        internal Card PlayCard()
        {
            Card card = null;
            bool success = false;
            card = ChooseCard();
            while (!success)
            {
                try
                {
                    bool LigalPlay = card.ToCompare(Deck.DiscardPile.GetTopCard());
                    bool IsInHand = Cards.Remove(card);
                    if (LigalPlay && IsInHand)
                    {
                        Deck.DiscardPile.AddCard(card);
                        success = card != null;
                        //TODO execute Card penalties
                    }
                    else
                    {
                        //TODO: what to do and better message
                        Cards.Add(card);
                        Console.WriteLine(LigalPlay.ToString() + " " + LigalPlay.ToString());
                        success = false;
                    }
                }
                catch (NullReferenceException)
                {
                    success = true;
                }
            }
            return card;
        }
        public Card DrawCard()
        {
            Card topCard = Deck.DealCard();
            this.Cards.Add(topCard);
            return topCard;
        }
        public List<Card> DrawCards(int Amount)
        {
            List<Card> topCards = new List<Card>();
            for (int i = 0; i < Amount; i++)
            {
                topCards.Add(DrawCard());
            }
            return topCards;
        }
    }
}