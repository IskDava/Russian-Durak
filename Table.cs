using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using System;
using UnityEditor.Inspector.GraphicsSettingsInspectors;

public class Table : MonoBehaviour
{
    [SerializeField] private int PlayersCount = 2;

    public static List<Card> deck = new()
    {
        new Card("6", "spades", 6),  new Card("7", "spades", 7),  new Card("8", "spades", 8),
        new Card("9", "spades", 9),  new Card("10", "spades", 10), new Card("J", "spades", 11),
        new Card("Q", "spades", 12), new Card("K", "spades", 13), new Card("A", "spades", 14),

        new Card("6", "hearts", 6),  new Card("7", "hearts", 7),  new Card("8", "hearts", 8),
        new Card("9", "hearts", 9),  new Card("10", "hearts", 10), new Card("J", "hearts", 11),
        new Card("Q", "hearts", 12), new Card("K", "hearts", 13), new Card("A", "hearts", 14),

        new Card("6", "diamonds", 6),  new Card("7", "diamonds", 7),  new Card("8", "diamonds", 8),
        new Card("9", "diamonds", 9),  new Card("10", "diamonds", 10), new Card("J", "diamonds", 11),
        new Card("Q", "diamonds", 12), new Card("K", "diamonds", 13), new Card("A", "diamonds", 14),

        new Card("6", "clubs", 6),  new Card("7", "clubs", 7),  new Card("8", "clubs", 8),
        new Card("9", "clubs", 9),  new Card("10", "clubs", 10), new Card("J", "clubs", 11),
        new Card("Q", "clubs", 12), new Card("K", "clubs", 13), new Card("A", "clubs", 14)
    };
    public static string TrumpSiut;
    public static Card UnderBank;
    void Start()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]); 
        }
        for (int i = 0; i < PlayersCount; i++)
        {
            List<Card> cards = deck.GetRange(0, 6);
            new Player(cards);
            deck = deck.GetRange(6, deck.Count - 6);
        }
        UnderBank = deck[0];
        TrumpSiut = deck[0].suit;
        Card.SetTrumps();

        Logs(deck, Player.players);
    }
    private void Logs(List<Card> deck, List<Player> players)
    {
        string s = "Bank's cards:\n";
        foreach (Card card in deck)
        {
            s += card + "\n";
        }
        s += "\nPlayers:\n";
        foreach (Player player in players)
        {
            foreach (Card card in player.cards)
            {
                s += card + ", ";
            }
            s = s[..^2];
            s += "\n";
        }
        Debug.Log(s);
    }
}
public class Card
{
    public readonly string name, suit;
    public readonly int priority;
    public bool isTrump;
    public Card(string name, string suit, int priority)
    {
        this.name = name;
        this.suit = suit;
        this.priority = priority;
    }
    public static void SetTrumps()
    {
        foreach (Card card in Table.deck)
        {
            card.isTrump = false;
            if (Table.TrumpSiut == card.suit)
            {
                card.isTrump = true;
            }
        }
    }
    public bool Fight(Card opponent)
    {
        if (opponent.priority < priority || ( !opponent.isTrump && isTrump))
        {
            return true;
        }
        return false;
    }
    public override string ToString()
    {
        string isTrumpContent = isTrump ? "trump" : "not trump";
        return $"Card {name} of {suit} ({priority}) is {isTrumpContent}";
    }
}
public class Player
{
    public static List<Player> players = new();
    public readonly List<Card> cards;
    public readonly int ID;
    public Player(List<Card> cards)
    {
        this.cards = cards;
        ID = players.Count;
        players.Add(this);
    }
    public void Add(Card card) { 
        cards.Add(card);
    }
    public void Add(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            Add(card);
        }
    }
    public void Remove(Card card)
    {
        cards.Remove(card);
    }
    public override string ToString()
    {
        return $"Player {ID} with cards: {cards}";
    }
}