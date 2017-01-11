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
        public int price;
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

        itemCost.price = Mathf.FloorToInt(flatPrice * (UnityEngine.Random.Range(0.8f, 1.4f) + (ResourceStorage.Instance.checkResource("Reputation") / 10f)));
        return itemCost;
    }

    public EventObject makeQuest()
    {
        EventObject QuestEvent = new EventObject();


        return QuestEvent;
    }

    public static EventObject makeSell()
    {
        ItemCostTuple item = getRandomItem();

        EventObject SellEvent = ScriptableObject.CreateInstance<EventObject>();
        EventObject Agree = ScriptableObject.CreateInstance<EventObject>();
        EventObject NoThanks = ScriptableObject.CreateInstance<EventObject>();

        Agree.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They smile as they hand you the {0}. They take the gold and leave.", item.item.name), new ResourceAmount[0], null, null)};
        Agree.prerequisites = new ResourceAmount[] {new ResourceAmount("Gold",item.price)};
        List<ResourceAmount> totalEffect = new List<ResourceAmount>(item.item.cost);
        totalEffect.Add(new ResourceAmount("Gold", item.price * -1));
        Agree.effects = totalEffect.ToArray();
        Agree.title = "Buy from Customer";

        NoThanks.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They frown as they take the {0} and leave.", item.item.name), new ResourceAmount[0], null, null) };
        NoThanks.title = "Buy from Customer";
        NoThanks.effects = new ResourceAmount[0];

        SellEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking if they can sell their {0}.", item.item.name), item.item.cost.ToArray(), new ResourceAmount("Gold", item.price * -1), null) };
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
        totalEffect.Add(new ResourceAmount("Gold", item.price));
        Agree.effects = totalEffect.ToArray();
        Agree.title = "Sell to Customer";

        NoThanks.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("They frown as they take their gold and leave."), new ResourceAmount[0], null, null) };
        NoThanks.title = "Sell to Customer";
        NoThanks.effects = new ResourceAmount[0];

        BuyEvent.textEvents = new EventObject.TextEvent[] { new EventObject.TextEvent(string.Format("A customer walks in asking if they can buy one {0}.", item.item.name), resourceEffect.ToArray(), new ResourceAmount("Gold", item.price), null) };
        BuyEvent.connectedOptions = new EventObject.EventGroup[] { new EventObject.EventGroup(Agree, "Sure, I\'ll sell."), new EventObject.EventGroup(NoThanks, "I can\'t sell you that.") };
        BuyEvent.title = "Sell to Customer";
        BuyEvent.effects = new ResourceAmount[0];

        return BuyEvent;
    }
}
