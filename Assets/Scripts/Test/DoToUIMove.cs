using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoToUIMove : MonoBehaviour
{
    public bool EndAnim;
    [SerializeField] Image GamesetUI;
    [SerializeField] Image Line_1;
    [SerializeField] Image Line_2;
    [SerializeField] RectTransform Line1;
    [SerializeField] RectTransform Line2;
    [SerializeField] Text GameSet;
    [SerializeField] Color Greens;
    // Start is called before the first frame update
    void Start()
    {
        //GameUIMove();
        EndAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void GameUIMove()
    {
        //色の設定
        GamesetUI.color = Greens;
        GameSet.color = new Color(255, 255, 255, 0);
        Line_1.color = new Color(255, 255, 255, 0);
        Line_2.color = new Color(255, 255, 255, 0);
        //以下アニメーション
        var seq = DOTween.Sequence();
        seq.Append(GamesetUI.DOFade(1f, 0.5f).SetEase(Ease.InOutSine))
           .Append(Line1.DOAnchorPos(new Vector2(800, 0), 0.8f).SetEase(Ease.InOutSine))
           .Join(Line2.DOAnchorPos(new Vector2(-800, 0), 0.8f).SetEase(Ease.InOutSine))
           .Join(Line_1.DOFade(1f, 1f).SetEase(Ease.InOutSine))
           .Join(Line_2.DOFade(1f, 1f).SetEase(Ease.InOutSine))
           .Append(GameSet.DOFade(1f, 1f).SetEase(Ease.InOutSine));
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        seq.Append(GameSet.DOFade(0f, 1f).SetEase(Ease.InOutSine))
           .Append(Line1.DOAnchorPos(new Vector2(0, 800), 0.8f).SetEase(Ease.InOutSine))
           .Join(Line2.DOAnchorPos(new Vector2(0, -800), 0.8f).SetEase(Ease.InOutSine))
           .Join(Line_1.DOFade(0f, 1f).SetEase(Ease.InOutSine))
           .Join(Line_2.DOFade(0f, 1f).SetEase(Ease.InOutSine))
           .Append(GamesetUI.DOFade(0f, 0.5f).SetEase(Ease.InOutSine));
        EndAnim = true;
    }

}
