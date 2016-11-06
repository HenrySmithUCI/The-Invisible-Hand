using System;

public class Bundle
{
    // public Dictionary<Dictionary<ResourceAmount, int>, int> Bundles;
    //public Dictionary<Dictionary<string, int>, int> Bundles;
    public Dictionary<string, int> Bundle;

    //public List<ResourceAmount> playerStorage;
    public List<string> resources;
    public int minPrice;

    // int lower = 1;
    //int upper = 5;
    //private int avgResourceQuantity;
    public Bundle(List<string> resources, int minPrice)
    {

        //this.costs = new List<string>(costs);
        // this.priceTable = new Dictionary<string, int>(priceTable);
        this.resources = new List<string>(resources);
        this.minPrice = minPrice;
        Bundle = genBundle(minPrice);




    }

    public Dictionary<string, int> getBundle() //ideally should be the only method called from other scripts
    {
        return Bundle;
    }

   

    public Dictionary<string, int> genBundle(int minPrice) //adds a Bundle to the list of all Bundles
    {
        Random rnd = new Random();
        //Dictionary<ResourceAmount,int> variationDict = new Dictionary<ResourceAmount, int>();
        //Dictionary<string, int> variationDict = new Dictionary<string, int>(

        
        

        Dictionary<string, int> itemAmounts = new Dictionary<string, int>();
        resources = shuffle(resources);
        foreach( string resource in resources)
        {
            itemAmounts.Add(resource, 0);
        }

        int index = 0;
        while (minPrice >= 0)
        {
          
            if (index = resources.Count)
            {
                index = 0;
            }
            string resource = resources[index];

            int amt = genAmount(resource);
            itemAmounts[resource] += amt;
            minPrice -= amt;
            index++;





        }


        return itemAmounts;




    }

   

    public List<string> shuffle(List<string> lst) //shuffles a list
    {
        Random rnd = new Random();
        List<string> taken = new List<string>(lst);
        for (int i = 0; i < lst.Count; i++)
        {
            int k = rnd.Next(taken.Count);
            lst[i] = taken[k];
            taken.Remove(taken[k]);

        }
        return lst;





    }

    private int genAmount(string resource, Random rnd)//logic for deciding the quanitity of a resource in a bundle
    {
        int coefficent = minterm / priceTable[resource] ;
        //should use priceTable in this code
        return rnd.Next(1, coefficent);
    }

}
