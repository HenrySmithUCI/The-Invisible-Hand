using System;
using System.Collections.Generic;

    public class BundleList
    {
        public Dictionary<Dictionary<ResourceAmount, int>, int> Bundles;
        public Dictionary<string, int> priceTable = new Dictionary<string, int>();
        public List<ResourceAmount> playerStorage;
        int lower = 1;
        int upper = 5;
        private int avgResourceQuantity;
        public int lowestBundleListSize = 1;
        public int highestBundleListSize;
        public BundleList(List<ResourceAmount> storage){
            this.playerStorage = new List<ResourceAmount>(storage);
            highestBundleListSize = playerStorage.Count;
            avgResourceQuantity = avgResource();
            makePriceTable();
            Bundles = new Dictionary<Dictionary<ResourceAmount, int>, int>();
            Random rnd = new Random();
            int size = rnd.Next(lowestBundleListSize, highestBundleListSize);
            for (int i = 0;  i < size; i++)
            {
                addBundle();
            }




        }

        public Dictionary<Dictionary<ResourceAmount, int>, int> getBundles() //ideally should be the only method called from other scripts
        {
            return Bundles;
        }

        public void makePriceTable() //generates constant price for each resource
        {
            
            List<ResourceAmount> storage = playerStorage;
            
            foreach (ResourceAmount resource in playerStorage)
            {



                priceTable.Add(resource.resourceName, weightedRandom(resource.amount)); //wasn't sure how to balance or streamline this
            }

            
        }

        public int weightedRandom(int amount)
        {
            
            amount = amount * 2; //to be replaced by a log or sin functon later
            Random rnd = new Random();
            amount += rnd.Next(lower, upper);
            return amount;

            
        }

        public void addBundle() //adds a Bundle to the list of all Bundles
        {
            int maxBundleSize = 4;
            Random rnd = new Random();
            int bundleSize = rnd.Next(2, maxBundleSize);
            List<ResourceAmount> itemsInBundle = new List<ResourceAmount>();
            Dictionary<ResourceAmount,int> variationDict = new Dictionary<ResourceAmount, int>();
             shuffle();
            foreach (ResourceAmount resource in playerStorage)
            {
                int variation = resource.amount - avgResourceQuantity;
                if (variation < avgResourceQuantity/2)
                {
                    variationDict.Add(resource, variation); //if the difference between the Resource amount and the average amount of a Resource is small enough it will be added to Bundle
                }


            }



            foreach (ResourceAmount key in variationDict.Keys) 
            {
                if (itemsInBundle.Count > bundleSize)
                {
                    break;

                }
                itemsInBundle.Add(key);


            }

            Dictionary<ResourceAmount, int> itemAmounts = new Dictionary<ResourceAmount, int>();
            foreach (ResourceAmount item in itemsInBundle)
            {
                itemAmounts.Add(item, genAmount(item, rnd));

            }

            int price = 0;

            foreach (ResourceAmount item in itemAmounts.Keys)
            {
                price += (int)(variationDict[item] + priceTable[item.resourceName] + avgResourceQuantity); //should yield a positive int
                Console.WriteLine(item.resourceName);
            }

            Console.WriteLine("test");

            Bundles.Add(itemAmounts, price);




        }

        public int avgResource()
        {
            int mean = 0;
            foreach (ResourceAmount resource in playerStorage)
            {
                mean += resource.amount;
            }
           // Console.WriteLine(mean / playerStorage.Count);
            return mean / playerStorage.Count;

        }

        public void shuffle( ) //shuffles the playeStorage list
        {
            Random rnd = new Random();
            List<ResourceAmount> taken = new List<ResourceAmount>(playerStorage);
            for(int i = 0; i < playerStorage.Count; i++)
            {
                int k = rnd.Next(taken.Count);
                playerStorage[i] = taken[k];
                taken.Remove(taken[k]);

            }

            
            


        }

        private int genAmount(ResourceAmount resource, Random rnd)//logic for deciding the quanitity of a resource in a bundle
        {
            return rnd.Next(1, resource.amount + avgResourceQuantity/2);

        }

    }