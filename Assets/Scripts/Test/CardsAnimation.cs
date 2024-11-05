using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsAnimation : MonoBehaviour
{
    [SerializeField] RectTransform CardsPanel;
    [SerializeField] RectTransform CardsPanel2;//Player2

    [SerializeField] float Ftime;
    [SerializeField] RectTransform[] Cards;
    [SerializeField] RectTransform[] Cards2;

    [SerializeField] GameObject[] MyCards;//カード自体
    [SerializeField] GameObject[] MyCards2;//カード自体
    [SerializeField] AudioClip CardNoise;
    public bool TurnStart_Animation_P1;//始めのカードアニメーションbool
    public bool TurnStart_Animation_P2;
    public bool TurnEnd_Animation2_P1;//終わりのカードアニメーションbool
    public bool TurnEnd_Animation2_P2;
    // Start is called before the first frame update
    void Start()
    {
        //CardsAnim_S();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoolChangeTurnCP1()
    {
        TurnStart_Animation_P1 = false;
    }

    public void BoolChangeTurnCP2()
    {
        TurnStart_Animation_P2 = false;
    }

    public void BoolChangeTurnECP1()
    {
        TurnEnd_Animation2_P1 = false;
    }
    public void BoolChangeTurnECP2()
    {
        TurnEnd_Animation2_P2 = false;
    }

    public async void CardsAnim_S()//Player1
    {
        Debug.Log("StartP1");
        if (!TurnStart_Animation_P1)
        {
            Debug.Log("A");
            CardsPanel.DOAnchorPos(new Vector2(CardsPanel.anchoredPosition.x, -175), 0.5f).SetEase(Ease.InOutBack);
            for(int i = 0; i < 10; i++)
            {
                if (MyCards[i] == null) continue;
                GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(CardNoise);
                Cards[i].DOAnchorPos(new Vector2(Cards[i].anchoredPosition.x, -175),0.5f).SetEase(Ease.InOutBack);
                await UniTask.Delay(System.TimeSpan.FromSeconds(Ftime));
            }
            TurnStart_Animation_P1 = true;
        }

    }

    public async void CardsAnim_S2()//Player2
    {
        Debug.Log("StartP2");
        if (!TurnStart_Animation_P2)
        {
            Debug.Log("A_1");
            CardsPanel2.DOAnchorPos(new Vector2(CardsPanel2.anchoredPosition.x, -175), 0.5f).SetEase(Ease.InOutBack);
            for (int i = 0; i < 10; i++)
            {
                if (MyCards2[i] == null) continue;
                GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(CardNoise);
                Cards2[i].DOAnchorPos(new Vector2(Cards2[i].anchoredPosition.x, -175), 0.5f).SetEase(Ease.InOutBack);
                await UniTask.Delay(System.TimeSpan.FromSeconds(Ftime));
            }
            TurnStart_Animation_P2 = true;
        }

    }

    public async void CardsAnim_E()//Player1 EndAnimation
    {
        Debug.Log("EndP1");
        if (!TurnEnd_Animation2_P1)
        {
            Debug.Log("B");
            for (int i = 0; i < 10; i++)
            {
                if (MyCards[i] == null) continue;
                Cards[i].DOAnchorPos(new Vector2(Cards[i].anchoredPosition.x, -290), 0.5f).SetEase(Ease.InOutBack);
                await UniTask.Delay(System.TimeSpan.FromSeconds(Ftime));
            }
            CardsPanel.DOAnchorPos(new Vector2(CardsPanel.anchoredPosition.x, -290), 0.5f).SetEase(Ease.InOutBack);
            TurnEnd_Animation2_P1 = true;
        }

    }

    public async void CardsAnim_E2()// Player2 EndAnimation
    {
        Debug.Log("EndP2");
        if (!TurnEnd_Animation2_P2)
        {
            Debug.Log("B_1");
            for (int i = 0; i < 10; i++)
            {
                if (MyCards2[i] == null) continue;
                Cards2[i].DOAnchorPos(new Vector2(Cards2[i].anchoredPosition.x, -290), 0.5f).SetEase(Ease.InOutBack);
                await UniTask.Delay(System.TimeSpan.FromSeconds(Ftime));
            }
            CardsPanel2.DOAnchorPos(new Vector2(CardsPanel2.anchoredPosition.x, -290), 0.5f).SetEase(Ease.InOutBack);
            TurnEnd_Animation2_P2 = true;
        }
    }
}
