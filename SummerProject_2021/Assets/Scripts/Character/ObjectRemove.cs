using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ObjectRemove : MonoBehaviour
{
    // Start is called before the first frame update
    public Image _image;
    public float alpha;
    public int During = 5;
    void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
    }

    public void ButtonRemove()
    {
        StartCoroutine(ButtonFade());
        //image.CrossFadeAlpha(0,1,true);
    }

    IEnumerator ButtonFade()
    {
        //_image.CrossFadeAlpha(1,1,true);
        while (_image.color.a > 0)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.SetActive(false);
        StopCoroutine(ButtonFade());
    }

    // Update is called once per frame
    void Update()
    {
        alpha = _image.color.a;
    }
}
