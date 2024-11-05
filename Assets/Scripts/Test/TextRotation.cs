using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotation : MonoBehaviour
{
    
    RectTransform tex;
    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<RectTransform>();
        tex.anchoredPosition = new Vector2(Random.Range(-400,400),-800);
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        tex.Rotate(0, 1f, 0);
        tex.transform.position += new Vector3(0,Random.Range(0.01f,0.03f),0);

    }
}
