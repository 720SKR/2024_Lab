using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomValueUIMove : MonoBehaviour
{
    [SerializeField] Image FadePanel;//背景ぼかし用UI
    [SerializeField] RectTransform RandmaizerUI;//UIオブジェクト
    [SerializeField] Text ValueText;
    [SerializeField] Text FirstOrSecond;//先攻後攻決めテキスト
    [SerializeField] int VictoryValue;
    [SerializeField] int Interval;
    [SerializeField] float Speed;
    [SerializeField] int RandV;
    [SerializeField] AudioClip tin;
    [SerializeField] AudioClip tw;
    public bool isfinish;//Animation終わったかどうか
    //AudioSource ass;
    int RandAnimUI;
    int test;
    // Start is called before the first frame update
    void Start()
    {

        //test = Random.Range(10, 35);
        //ass = GetComponent<AudioSource>();
        //RandomAnimetionS(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomAnimetionS(int V)//目標値設定 boardManagerから目標値を受け取って始動する。
    {
        isfinish = false;
        VictoryValue = V;
        RandAnimation();
    }

    public async void RandAnimation()
    {
        RandV = GameObject.Find("GameSystem").GetComponent<GameManager>().GetRandV();
        switch (RandV)
        {
            case 0:
                FirstOrSecond.material = UIManager.SetM_P1();
                break;
            case 1:
                FirstOrSecond.material = UIManager.SetM_P2();
                break;
        }
        FirstOrSecond.text = null;
        var seq = DOTween.Sequence();
        seq.Append(FadePanel.DOFade(1f, 1f).SetEase(Ease.OutSine))
           .Join(RandmaizerUI.DOAnchorPos(new Vector2(0,0),0.5f).SetEase(Ease.OutBack));
        await UniTask.WaitForSeconds(1);
        for (int i= 0; i < Interval; i++)
        {
            //ass.PlayOneShot(tin);
            GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(tin);
            ValueText.text = Random.Range(10, 35).ToString();
            await UniTask.WaitForSeconds(0.01f+Speed);
            Speed += 0.02f;
        }
        await UniTask.WaitForSeconds(1);
        GameObject.Find("SEManager").GetComponent<AudioSource>().PlayOneShot(tw);
        ValueText.text = VictoryValue.ToString();
        FirstOrSecond.text = "First Player : " + (RandV+1).ToString();
        await UniTask.WaitForSeconds(2);
        seq.Append(RandmaizerUI.DOAnchorPos(new Vector2(-1000, 0), 0.5f).SetEase(Ease.InBack))
           .Join(FadePanel.DOFade(0f, 1f).SetEase(Ease.InSine));
        seq.Kill();
        await UniTask.WaitForSeconds(2);
        FadePanel.enabled = false;
        isfinish = true;
    }
}
