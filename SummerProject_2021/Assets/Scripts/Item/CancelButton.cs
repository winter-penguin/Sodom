using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void Cancel()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
