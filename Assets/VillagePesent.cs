using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoreType { TradingPost, Inn, Tavern, Blacksmith}
public enum Species { Human, Ork, Goblin, Alien, Mage, Farmer}
public enum Attitude
{
    Snarky,
    Polite,
    Dorky,
    Lazy,
    Impatient,
    Friendly,
    Detached,
    Nice,
    Condescending,
    Curt
}

public class VillagePesent : ChatAgent
{
    public StoreType StoreType;
    public Species Species;
    public Attitude Attitude;
    public string Name;
    public string[] SaleItems;

    protected override void Start()
    {
        Name = "Bob";
        Attitude = (Attitude) UnityEngine.Random.Range(0,Enum.GetValues(typeof(Attitude)).Length);

        Debug.Log($"NPC is a {Attitude} {Species} named {Name}");
        base.Start();
    }

    public override string GetSystemPrompt()
    {
        return $"You are a(n) {Species} store clerk named {Name} in a medieval village. " +
        $"Your store is a(n) {StoreType}. " +
        $"The items you are stelling are {string.Join(", ", SaleItems)}. " +
        $"A customer (the player) has just entered the store. " +
        $"Great them in a(n) {Attitude} manner (your dialouge only)";
    }
}