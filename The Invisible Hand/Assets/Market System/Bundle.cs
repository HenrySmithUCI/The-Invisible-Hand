using System;
using UnityEngine;
using System.Collections.Generic;

public class Bundle
{
    public Dictionary<string, int> bundle;
  
    public List<string> resources;
    public int minPrice;
  
    public Bundle(List<string> resources, int minPrice)
    {
        this.resources = new List<string>(resources);
        this.minPrice = minPrice;
        bundle = genBundle(minPrice);




    }

    public Dictionary<string, int> getBundle() //ideally should be the only method called from other scripts
    {
        return bundle;
    }

   

    public Dictionary<string, int> genBundle(int minPrice) //adds a Bundle to the list of all Bundles
    {
        Dictionary<string, int> itemAmounts = new Dictionary<string, int>();
        resources = shuffle(resources);
        foreach( string resource in resources)
        {
            itemAmounts.Add(resource, 0);
        }

        int index = 0;
        while (minPrice >= 0)
        {
          
            if (index == resources.Count)
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
        List<string> taken = new List<string>(lst);
        for (int i = 0; i < lst.Count; i++)
        {
            int k = UnityEngine.Random.Range(0, taken.Count);
            lst[i] = taken[k];
            taken.Remove(taken[k]);

        }
        return lst;





    }

    private int genAmount(string resource)//logic for deciding the quanitity of a resource in a bundle
    {
        int coefficent = Mathf.CeilToInt(minPrice / CostManager.Instance.getPrice(resource));
        return UnityEngine.Random.Range(1, coefficent);
    }

}
