using System.Collections.Generic;
using UnityEngine;

public class PlatingStepManager : MonoBehaviour
{
    // collect items to item list
    [SerializeField] private List<GameObject> itemList;

    public void CollectItemsToPlate()
    {
        var items = FindObjectsOfType<ItemToPlate>();

        foreach (var item in items)
        {
            itemList.Add(item.gameObject);
        }
    }

    public void Awake()
    {
        CollectItemsToPlate();
    }

    // plating progress increases as each item is added to the plate

    // when done plating, present dish with a finish screen


}
