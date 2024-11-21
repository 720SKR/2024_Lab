using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUIManager : MonoBehaviour
{
    [SerializeField] Text[] SelectMass_Text;
    [SerializeField] Image[] SelectMass_Image;
    [SerializeField] Material[] Player_Material;
    // Start is called before the first frame update
    void Start()
    {
        Player_Material = new Material[2];
        Player_Material[0] = UIManager.SetM_P1();
        Player_Material[1] = UIManager.SetM_P2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMassBoard(int Value,int SelectID,int playerNo)
    {//�I�����ꂽ�}�X�ɑ΂���UI��ōs������
        SelectMass_Image[SelectID].material = Player_Material[playerNo];
        SelectMass_Text[SelectID].text = Value.ToString(); 
    }
}
