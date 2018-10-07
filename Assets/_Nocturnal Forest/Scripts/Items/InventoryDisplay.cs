using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public ItemDisplay itemDisplayPrefab;

    public Inventory Inventory
    {
        get;
        private set;
    }

    public void Prime(Inventory inventory)
    {
        Inventory = inventory;

        Refresh();
    }

    public void Refresh()
    {
        DestroyDisplays();
        CreateDisplays();
    }

    private void CreateDisplays()
    {
        Inventory?.items?.ForEach(x => CreateItemDisplay(x));
    }

    private void DestroyDisplays()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    private void CreateItemDisplay(Item item)
    {
        ItemDisplay display = Instantiate(itemDisplayPrefab, transform, false);
        display.Prime(item);
    }
}
