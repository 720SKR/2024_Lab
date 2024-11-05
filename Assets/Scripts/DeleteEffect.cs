using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEffect : MonoBehaviour
{
    //インスタントしたパーティクルを削除する
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
