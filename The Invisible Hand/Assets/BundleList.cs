using System;
using System.Collections.Generic;

    public class BundleList
    {
       
        // public Dictionary<Dictionary<ResourceAmount, int>, int> Bundles;
        public Dictionary<Dictionary<string, int>, int> Bundles;
        public Dictionary<string, int> priceTable;
        //public List<ResourceAmount> playerStorage;
        public List<string> resources;
        // int lower = 1;
        //int upper = 5;
        //private int avgResourceQuantity;
        public int lowestBundleListSize = 1;
        public int highestBundleListSize;
        public BundleList(List<string> resources, Dictionary<string, int> priceTable)
        {
            this.priceTable = new Dictionary<string, int>(priceTable);
            this.resources = new List<string>(resources);
            highestBundleListSize = genBundleListSize();
            //avgResourceQuantity = avgResource();

            //Bundles = new Dictionary<Dictionary<ResourceAmount, int>, int>();
            Bundles = new Dictionary<Dictionary<string, int>, int>();
            Random rnd = new Random();
            int size = rnd.Next(lowestBundleListSize, highestBundleListSize);
            for (int i = 0; i < size; i++)
            {
                addBundle();
            }




        }

        public Dictionary<Dictionary<string, int>, int> getBundles() //ideally should be the only method called from other scripts
        {
            return Bundles;
        }

        /*public void makePriceTable() //generates constant price for each resource
        {
            
            List<ResourceAmount> storage = playerStorage;
            
            foreach (ResourceAmount resource in playerStorage)
            {



                priceTable.Add(resource.resourceName, weightedRandom(resource.amount)); //wasn't sure how to balance or streamline this
            }

            
        } */

        /*  public int weightedRandom(int amount)
          {

              amount = amount * 2; //to be replaced by a log or sin functon later
              Random rnd = new Random();
              amount += rnd.Next(lower, upper);
              return amount;


          }*/

        public int genBundleListSize()
        {
            return resources.Count;
        }

        public void addBundle() //adds a Bundle to the list of all Bundles
        {
            int maxBundleSize = 4;
            Random rnd = new Random();
            int bundleSize = rnd.Next(2, maxBundleSize);
            List<string> itemsInBundle = new List<string>();
            //Dictionary<ResourceAmount,int> variationDict = new Dictionary<ResourceAmount, int>();
            //Dictionary<string, int> variationDict = new Dictionary<string, int>();

            itemsInBundle = genItemsInBundle(bundleSize);




            Dictionary<string, int> itemAmounts = new Dictionary<string, int>();
            foreach (string item in itemsInBundle)
            {
                itemAmounts.Add(item, genAmount(bundleSize, rnd));

            }

            int price = 0;

            foreach (string item in itemAmounts.Keys)
            {
                price += (int)(priceTable[item]); //should yield a positive int

            }

            Console.WriteLine("test");

            Bundles.Add(itemAmounts, price);




        }

        /* public int avgResource()
         {
             int mean = 0;
             foreach (ResourceAmount resource in playerStorage)
             {
                 mean += resource.amount;
             }
            // Console.WriteLine(mean / playerStorage.Count);
             return mean / playerStorage.Count;

         }*/

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

        private int genAmount(int bundleSize, Random rnd)//logic for deciding the quanitity of a resource in a bundle
        {
            int coefficent = 10;
            //should use priceTable in this code
            return rnd.Next(1, coefficent / bundleSize);

        }

<<<<<<< HEAD:The Invisible Hand/Assets/BundleList.cs
    }
=======
        private List<string> genItemsInBundle(int bundleSize)
        {
            List<string> shuffled = new List<string>(resources);
            shuffled = shuffle(shuffled);
            List<string> result = new List<string>();
            for (int i = 0; i < bundleSize; i++)
            {
                result.Add(shuffled[i]);
            }
            return result;

        }
    }
}
>>>>>>> origin/BundleListDictionaryGeneration:The Invisible Hand/Assets/Class1.cs
