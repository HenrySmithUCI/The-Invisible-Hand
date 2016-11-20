using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class makeBundle : MonoBehaviour {



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    //simulates a bundle UI being generated
    public void showBundle(UnityEngine.UI.Button bundleButton)
    {
        //ResourceAmount[] priceTable = CostManager.Instance.priceTable;
        List<string> items = new List<string>();
        items.Add("iron");
        items.Add("wood");
        items.Add("stone");
        items.Add("wheat");
        Dictionary<string, int> prices = new Dictionary<string, int>();
        prices.Add("iron", 10);
        prices.Add("wood", 5);
        prices.Add("stone", 15);
        prices.Add("wheat", 20);

        Bundle bundle = new Bundle(items, 100);
        string textToShow = "";
        foreach (string item in bundle.getBundle().Keys)
        {
            string amt = (bundle.getBundle()[item]).ToString();
            textToShow += item + ":" + amt;
        }
        
        bundleButton.GetComponentInChildren<Text>().text = textToShow;


    }
}

