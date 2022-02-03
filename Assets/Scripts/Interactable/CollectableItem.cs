using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : Interactable
{
    public string itemName;

    public override void Interact()
    {
        Reference.Instance.PIM.PickUp(this);
    }
}
