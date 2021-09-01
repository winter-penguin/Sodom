using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    public GameObject beforeSprite;
    public GameObject afterSprite;
    public bool isDoorOpen = true;

    public void BeforAfterChange()
    {
        if (isDoorOpen)
        {
            afterSprite.SetActive(false);
            beforeSprite.SetActive(true);
            isDoorOpen = false;
        }
        else
        {
            beforeSprite.SetActive(false);
            afterSprite.SetActive(true);
            isDoorOpen = true;
        }
    }
}
