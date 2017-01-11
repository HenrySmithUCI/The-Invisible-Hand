using UnityEngine;
using System.Collections.Generic;

public class RandomEventGenerator : MonoBehaviour {

    private struct ItemTuple
    {
        public string name;
        public List<ResourceAmount> cost;
        public ItemTuple(string name, List<ResourceAmount> cost)
        {
            this.name = name;
            this.cost = cost;
        }
    }

    private struct ItemCostTuple
    {
        public ItemTuple item;
        public int priceUpper;
        public int priceLower;
    }
    //Apple, wood, cloth, herb, stone, leather, steel, fairy, gem
    private static List<ItemTuple> ResourceItems = new List<ItemTuple>()
    {
        //Single Resource Items
        new ItemTuple("Wood Shipment", new List<ResourceAmount>() { new ResourceAmount("Wood", 12) }),
        new ItemTuple("Wood Axe", new List<ResourceAmount>() { new ResourceAmount("Wood", 4) }),
        new ItemTuple("Single Log", new List<ResourceAmount>() { new ResourceAmount("Wood", 1) }),
        new ItemTuple("Wooden Toy", new List<ResourceAmount>() { new ResourceAmount("Wood", 2) }),
        new ItemTuple("Apple Crop", new List<ResourceAmount>() { new ResourceAmount("Apple", 12) }),
        new ItemTuple("Apple Cake", new List<ResourceAmount>() { new ResourceAmount("Apple", 4) }),
        new ItemTuple("Apple Treat", new List<ResourceAmount>() { new ResourceAmount("Apple", 2) }),
        new ItemTuple("Sweet Apple", new List<ResourceAmount>() { new ResourceAmount("Apple", 1) }),
        new ItemTuple("Stone Brick", new List<ResourceAmount>() { new ResourceAmount("Stone", 4) }),
        new ItemTuple("Stone Block", new List<ResourceAmount>() { new ResourceAmount("Stone", 2) }),
        new ItemTuple("Steel Bar", new List<ResourceAmount>() { new ResourceAmount("Steel", 4) }),
        new ItemTuple("Steel Weight", new List<ResourceAmount>() { new ResourceAmount("Steel", 1) }),
        new ItemTuple("Cloth Blanket", new List<ResourceAmount>() { new ResourceAmount("Cloth", 4) }),
        new ItemTuple("Cloth Shirt", new List<ResourceAmount>() { new ResourceAmount("Cloth", 2) }),
        new ItemTuple("Cloth Scrap", new List<ResourceAmount>() { new ResourceAmount("Cloth", 1) }),
        new ItemTuple("Leather Pants", new List<ResourceAmount>() { new ResourceAmount("Leather", 4) }),
        new ItemTuple("Leather Boots", new List<ResourceAmount>() { new ResourceAmount("Leather", 2) }),
        new ItemTuple("Herb Pouch", new List<ResourceAmount>() { new ResourceAmount("Herb", 2) }),
        new ItemTuple("Fresh Herb", new List<ResourceAmount>() { new ResourceAmount("Herb", 1) }),
        new ItemTuple("Fairy Bottle", new List<ResourceAmount>() { new ResourceAmount("Fairy", 1) }),
        new ItemTuple("Shiny Gem", new List<ResourceAmount>() { new ResourceAmount("Gem", 1) }),
        //Double Resource Items
        new ItemTuple("Wooden Doll" , new List<ResourceAmount>() { new ResourceAmount("Wood", 1), new ResourceAmount("Cloth", 1) }),
        new ItemTuple("Applewood Plank" , new List<ResourceAmount>() { new ResourceAmount("Wood", 1), new ResourceAmount("Apple", 2) }),
        new ItemTuple("Leather Shield" , new List<ResourceAmount>() { new ResourceAmount("Leather", 1), new ResourceAmount("Wood", 2) }),
        new ItemTuple("Metal Shield" , new List<ResourceAmount>() { new ResourceAmount("Leather", 1), new ResourceAmount("Steel", 2) }),
        new ItemTuple("Strength Potion" , new List<ResourceAmount>() { new ResourceAmount("Herb", 1), new ResourceAmount("Stone", 3) }),
        new ItemTuple("Light Potion" , new List<ResourceAmount>() { new ResourceAmount("Herb", 1), new ResourceAmount("Cloth", 3) }),
        new ItemTuple("Health Potion" , new List<ResourceAmount>() { new ResourceAmount("Herb", 1), new ResourceAmount("Leather", 3) }),
        new ItemTuple("Magic Potion" , new List<ResourceAmount>() { new ResourceAmount("Herb", 3), new ResourceAmount("Fairy", 1) }),
        new ItemTuple("Wealth Potion" , new List<ResourceAmount>() { new ResourceAmount("Herb", 3), new ResourceAmount("Gem", 1) }),
        new ItemTuple("Sweet Tea" , new List<ResourceAmount>() { new ResourceAmount("Herb", 2), new ResourceAmount("Apple", 2) }),
        new ItemTuple("Magic Gem" , new List<ResourceAmount>() { new ResourceAmount("Gem", 1), new ResourceAmount("Fairy", 2) }),
        new ItemTuple("Stone Axe" , new List<ResourceAmount>() { new ResourceAmount("Stone", 2), new ResourceAmount("Wood", 2) }),
        new ItemTuple("Steel Sword" , new List<ResourceAmount>() { new ResourceAmount("Steel", 2), new ResourceAmount("Wood", 2) }),
        new ItemTuple("Stone Wheel" , new List<ResourceAmount>() { new ResourceAmount("Steel", 1), new ResourceAmount("Stone", 2) }),
        new ItemTuple("Steel Armor" , new List<ResourceAmount>() { new ResourceAmount("Steel", 2), new ResourceAmount("Cloth", 3) }),
        new ItemTuple("Magic Rock" , new List<ResourceAmount>() { new ResourceAmount("Fairy", 1), new ResourceAmount("Stone", 3) }),
    };

    private static ItemCostTuple getRandomItem()
    {
        ItemTuple item = ResourceItems[UnityEngine.Random.Range(0, ResourceItems.Count)];
        ItemCostTuple itemCost = new ItemCostTuple();
        itemCost.item = item;

        float flatPrice = 0;

        foreach(ResourceAmount amount in item.cost)
        {
            flatPrice += CostManager.Instance.getPrice(amount.resourceName) * amount.amount;
        }

        itemCost.priceUpper = Mathf.FloorToInt(flatPrice * (UnityEngine.Random.Range(1f, 1.4f) + (ResourceStorage.Instance.checkResource("Reputation") / 10f)));
        itemCost.priceLower = Mathf.FloorToInt(flatPrice * (UnityEngine.Random.Range(0.8f, 1f) - (ResourceStorage.Instance.checkResource("Reputation") / 20f)));
        return itemCost;
    }

    public static EventObject makeQuest()
    {
        ItemCostTuple item = getRandomItem();
        ItemCostTuple item2 = getRandomItem();

        EventObject QuestEvent = ScriptableObject.CreateInstance<EventObject>();
        EventObject Agree = ScriptableObject.CreateInstance<EventObject>();
        EventObject NoThanks = ScriptableObject.CreateInstance<EventObject>();
        EventObject OnEnd = ScriptableObject.CreateInstance<EventObject>();
        EventObject QuestSucess = ScriptableObject.CreateInstance<EventObject>();
        EventObject QuestFailed = ScriptableObject.CreateInstance<EventObject>();
        EventObject TooManyQuests = ScriptableObject.CreateInstance<EventObject>();
        QuestObject quest = ScriptableObject.CreateInstance<QuestObject>();

        

        List<ResourceAmount> effects = new List<ResourceAmount>();
        for (int i = 0; i < item.item.cost.Count; i++)
        {
            effects.Add(item.item.cost[i]);
        }

        for (int i = 0; i < item2.item.cost.Count; i++)
        {
            if (effects.Exists(ra => ra.resourceName == item2.item.cost[i].resourceName))
            {
                effects.Find(ra => ra.resourceName == item2.item.cost[i].resourceName).amount += item2.item.cost[i].amount;
            }
            else
            {
                effects.Add(item2.item.cost[i]);
            }
        }

        quest.maxTurns = 2;
        quest.cost = effects.ToArray();

        List<ResourceAmount> copyEffects = new List<ResourceAmount>(effects);
        for (int i = 0; i < copyEffects.Count; i++)
        {
            copyEffects[i] = new ResourceAmount(copyEffects[i].resourceName, copyEffects[i].amount * -1);
        }
        quest.reward = new ResourceAmount("Gold", item.priceUpper + item2.priceUpper);
        quest.OnQuestEnd = OnEnd;
        
        QuestEvent.title = "Customer Quest";
        if (item.item.name == item2.item.name)
        {
            QuestEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking for two {0}s in two days", item.item.name), copyEffects.ToArray(), new ResourceAmount("Turn", 2), new ResourceAmount("Gold", item.priceUpper + item2.priceUpper)) };
            quest.description = string.Format("Get two {0}s", item.item.name);
            OnEnd.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("The customer from two days ago returns asking for their two {0}s", item.item.name), copyEffects.ToArray(), null, new ResourceAmount("Gold", item.priceUpper + item2.priceUpper)) };
        }
        else
        {
            QuestEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking for one {0} and one {1} in two days.", item.item.name, item2.item.name), copyEffects.ToArray(), new ResourceAmount("Turn", 2), new ResourceAmount("Gold", item.priceUpper + item2.priceUpper)) };
            quest.description = string.Format("Get one {0} and one {1}", item.item.name, item2.item.name);
            OnEnd.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("The customer from two days ago returns asking for their {0} and {1}", item.item.name, item2.item.name), copyEffects.ToArray(), null, new ResourceAmount("Gold", item.priceUpper + item2.priceUpper)) };
        }
        QuestEvent.effects = new ResourceAmount[0];
        QuestEvent.connectedOptions = new EventObject.EventGroup[] { new EventObject.EventGroup(Agree, "Sure, I can make that order"), new EventObject.EventGroup(NoThanks, "I can\'t make that order") };

        Agree.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent("\"Fantastic! I'll be back in two days.\" They leave the store.", new ResourceAmount[0], null, null) };
        Agree.assignedQuest = quest;
        Agree.title = "Customer Quest";
        Agree.onTooManyQuests = TooManyQuests;
        Agree.effects = new ResourceAmount[0];

        NoThanks.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They frown and leave.", item.item.name), new ResourceAmount[0], null, null) };
        NoThanks.title = "Customer Quest";
        NoThanks.effects = new ResourceAmount[0];

        TooManyQuests.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("Unfortunately, you have too much on your plate (too many active quests)! You tell the customer you cannot accept their request. They leave looking disappointed."), new ResourceAmount[0], null, null) };
        TooManyQuests.title = "Customer Quest";
        TooManyQuests.effects = new ResourceAmount[0];
        
        OnEnd.title = "!!End Customer Quest!!";
        OnEnd.effects = new ResourceAmount[0];
        OnEnd.connectedOptions = new EventObject.EventGroup[] { new EventObject.EventGroup(QuestSucess, "Here are your items!"), new EventObject.EventGroup(QuestFailed, "I\'m sorry I can\'t give you those items.") };

        QuestSucess.title = "!!End Customer Quest!!";
        QuestSucess.prerequisites = effects.ToArray();
        
        copyEffects.Add(new ResourceAmount("Gold", item.priceUpper + item2.priceUpper));
        copyEffects.Add(new ResourceAmount("Reputation", 1));
        QuestSucess.effects = copyEffects.ToArray();
        QuestSucess.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They smile as they take their items. They put the gold on your desk, thank you, and leave."), new ResourceAmount[0], null, null) };

        QuestFailed.title = "!!End Customer Quest!!";
        QuestFailed.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("The customer leaves your store fuming with rage."), new ResourceAmount[0], null, null) };
        QuestFailed.effects = new ResourceAmount[] { new ResourceAmount("Reputation", -1) };

        return QuestEvent;
    }

    public static EventObject makeSell()
    {
        ItemCostTuple item = getRandomItem();

        EventObject SellEvent = ScriptableObject.CreateInstance<EventObject>();
        EventObject Agree = ScriptableObject.CreateInstance<EventObject>();
        EventObject NoThanks = ScriptableObject.CreateInstance<EventObject>();

        Agree.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They smile as they hand you the {0}. They take the gold and leave.", item.item.name), new ResourceAmount[0], null, null)};
        Agree.prerequisites = new ResourceAmount[] {new ResourceAmount("Gold",item.priceLower)};
        List<ResourceAmount> totalEffect = new List<ResourceAmount>(item.item.cost);
        totalEffect.Add(new ResourceAmount("Gold", item.priceLower * -1));
        Agree.effects = totalEffect.ToArray();
        Agree.title = "Buy from Customer";

        NoThanks.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They frown as they take the {0} and leave.", item.item.name), new ResourceAmount[0], null, null) };
        NoThanks.title = "Buy from Customer";
        NoThanks.effects = new ResourceAmount[0];

        SellEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking if they can sell their {0}.", item.item.name), item.item.cost.ToArray(), new ResourceAmount("Gold", item.priceLower * -1), null) };
        SellEvent.connectedOptions = new EventObject.EventGroup[] { new EventObject.EventGroup(Agree, "Sure, I\'ll buy."), new EventObject.EventGroup(NoThanks, "I can\'t buy that from you.") };
        SellEvent.title = "Buy from Customer";
        SellEvent.effects = new ResourceAmount[0];

        return SellEvent;
    }

    public static EventObject makeBuy()
    {
        ItemCostTuple item = getRandomItem();

        EventObject BuyEvent = ScriptableObject.CreateInstance<EventObject>();
        EventObject Agree = ScriptableObject.CreateInstance<EventObject>();
        EventObject NoThanks = ScriptableObject.CreateInstance<EventObject>();

        Agree.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They smile as they hand you the gold. They take the {0} and leave.", item.item.name), new ResourceAmount[0], null, null) };
        Agree.prerequisites = item.item.cost.ToArray();
        List<ResourceAmount> resourceEffect = new List<ResourceAmount>();
        for(int i = 0; i < item.item.cost.Count; i++)
        {
            resourceEffect.Add(new ResourceAmount(item.item.cost[i].resourceName, item.item.cost[i].amount * -1));
        }
        List<ResourceAmount> totalEffect = new List<ResourceAmount>(resourceEffect);
        totalEffect.Add(new ResourceAmount("Gold", item.priceUpper));
        Agree.effects = totalEffect.ToArray();
        Agree.title = "Sell to Customer";

        NoThanks.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They frown as they take their gold and leave."), new ResourceAmount[0], null, null) };
        NoThanks.title = "Sell to Customer";
        NoThanks.effects = new ResourceAmount[0];

        BuyEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking if they can buy one {0}.", item.item.name), resourceEffect.ToArray(), new ResourceAmount("Gold", item.priceUpper), null) };
        BuyEvent.connectedOptions = new EventObject.EventGroup[] { new EventObject.EventGroup(Agree, "Sure, I\'ll sell."), new EventObject.EventGroup(NoThanks, "I can\'t sell you that.") };
        BuyEvent.title = "Sell to Customer";
        BuyEvent.effects = new ResourceAmount[0];

        return BuyEvent;
    }
}
