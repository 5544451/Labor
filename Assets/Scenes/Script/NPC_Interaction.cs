using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class NPC_Interaction : MonoBehaviour
{
    GameObject line, InterActionBtn;

    [SerializeField] TextMeshProUGUI dialog;
    bool dialogType = true;

    // Start is called before the first frame update
    void Start()
    {
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
