using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System.IO;
using LitJson;
using TMPro.Examples;


public class NPC_Interaction : MonoBehaviour
{
    GameObject line, InterActionBtn;

    //public TextAsset dataGemeList;
    [SerializeField]TextMeshProUGUI dialog;
    bool dialogType = true;
    string data;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Scenes/Script/825test.json";
        string json = File.ReadAllText(path);
        JsonData myData = JsonMapper.ToObject(json);
        Debug.Log(myData);
        //JsonData Lines = JsonMapper.ToObject(myData[0]["Lines"]);
        for (int i = 0; i < myData.Count; i++)
        {
            string line = myData[i]["line"].ToString();
            Debug.Log(line);
        }

        line = transform.GetChild(0).gameObject;
        InterActionBtn = transform.GetChild(1).gameObject;

        line.SetActive(false);
        InterActionBtn.SetActive(false);

        //���Ǿ��� �����Ҷ� �ʿ��� ��縦 �̸� �����ؼ� �о���߰�����
        //readJSON();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            InterActionBtn.SetActive(true);
        }

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.LeftControl)) 
        { 
            InterActionBtn.SetActive(false) ;
            line.SetActive(true);
            if(dialogType)
            {
                StartCoroutine(TypeWriter());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            line.SetActive(false);
            InterActionBtn.SetActive(false);
        }

        StopAllCoroutines();
        dialogType = true;
    }

    void readJSON(string json) // ���̽� ������ �д� �Լ�
    {
        //1.���̽� ������ ���
        //2. ���ο��� �ʿ��� ������ �а�, �迭�� �����Ѵ�
/*        return npc�� ���� ������ �迭��

        ��� = [�ȳ� ���̸��� ���Ǿ���, << Index 0
            ���� ���� ���־�, Index 1 i++
            �߰� ] index 2 i++*/
    }


    IEnumerator TypeWriter()
    {
        //typeTxt ������ ���� ���� ���� �ڵ����� �ٲٵ��� �����Ѵ�. (for���� 2���� �ǰ���?)
        dialogType = false;
        string typeTxt = "Intesraction TEST";
        dialog.text = "";
        for (int i = 0; i < typeTxt.Length; i++)
        {
            dialog.text += typeTxt[i].ToString();
            yield return new WaitForSeconds(0.1f);
        }
        //������ �� ������ 
        // if (Input.GetKey(KeyCode.LeftControl)) 
        // ������ ���� �������� ���ư�����.

    }
}
