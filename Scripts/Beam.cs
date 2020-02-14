using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public static Action OnBeamConsumed;
    Beamable.ItemType itemGrabbed;

    private void Start()
    {
        //StartCoroutine(Deactivate(this.gameObject));
    }

    public void ItemPickedUp(Beamable.ItemType newItem)
    {
        //update the item grabbed;
        itemGrabbed = newItem;

        if (OnBeamConsumed != null)
            OnBeamConsumed();
    }

    public Beamable.ItemType GetItemGrabbed()
    {
        return itemGrabbed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            SoundManager.instance.PlayRaw("PickUp");
            ItemPickedUp(other.gameObject.GetComponent<Beamable>().pickUpType);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Deactivate(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
    }
}
