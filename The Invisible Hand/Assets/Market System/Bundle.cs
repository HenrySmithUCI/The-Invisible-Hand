using System;
using UnityEngine;
using System.Collections.Generic;

public class Bundle
{
    public Dictionary<string, int> bundle;
    public int totalPrice = 0;

  
    
    
   
  
    public Bundle(List<string> resources, int minPrice) //returns item and amounts
    {
        
        
        bundle = genBundle(minPrice, resources);

        


    }

    public int getPrice() //returns price of Bundle
    {
        return totalPrice;
    }



    

    public Dictionary<string, int> getBundle() //ideally should be the only method called from other scripts
    {
        return bundle;
    }

   

    public Dictionary<string, int> genBundle(int minPrice, List<string> itemTypes) 
    {
        Dictionary<string, int> itemAmounts = new Dictionary<string, int>();
        List<string> resources = new List<string>(itemTypes);
        resources = Shuffle.shuffle<string>(resources);
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

            int amt = genAmount(resource, minPrice);
            totalPrice += Mathf.CeilToInt(amt * priceModify(CostManager.Instance.getPrice(resource)));
            itemAmounts[resource] += amt;
            minPrice -= amt;
            index++;





        }


        return itemAmounts;




    }

    private int genAmount(string resource, int minPrice)//logic for deciding the quanitity of a resource in a bundle
    {

        int coefficent = Mathf.CeilToInt(minPrice / CostManager.Instance.getPrice(resource));
        int amt = UnityEngine.Random.Range(1, coefficent);
        return amt;

    }

    private float priceModify(float amount)
    {
        return amount / 2;
    }

    

}
