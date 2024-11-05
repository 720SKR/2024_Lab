using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mousetracking : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform _canvasTransform;//���e�p��Canvas��RectTransform
    [SerializeField] RectTransform _cutsorTransform;//�}�E�X�|�C���^�\�p
    [SerializeField] GameObject clickeffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out var mousePosition);

        _cutsorTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(clickeffect, _cutsorTransform);
            //Destroy(clickeffect, 0.5f);
        }
    }
}
