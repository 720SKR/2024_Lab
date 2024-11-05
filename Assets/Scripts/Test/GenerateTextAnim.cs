using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTextAnim : MonoBehaviour
{
    [SerializeField] GameObject Texts;
    [SerializeField] RectTransform Parent;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenText",1,Random.Range(1,5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenText()
    {
        Instantiate(Texts,Parent);//ê∂ê¨
    }
}
