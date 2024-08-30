using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System.IO;
using TMPro.Examples;


public class NPC_Interaction : MonoBehaviour
{
    GameObject line, InterActionBtn;

    //public TextAsset dataGemeList;
    [SerializeField]TextMeshProUGUI dialog;
    bool dialogType = true;
    List<string> Dialog = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetChild(0).gameObject;
        InterActionBtn = transform.GetChild(1).gameObject;

        line.SetActive(false);
        InterActionBtn.SetActive(false);


        //엔피씨가 시작할때 필요한 대사를 미리 저장해서 읽어놔야겠지요
        readJSON();
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

    void readJSON() // 제이슨 파일을 읽는 함수
    {
        TextAsset textAsset = Resources.Load<TextAsset>("825test");
        JsonLines jsonData = JsonUtility.FromJson<JsonLines>(textAsset.text);
        foreach (JsonLine lt in jsonData.Lines)
        {
            Dialog.Add(lt.line);
        }
    }

    IEnumerator TypeWriter()
    {
        //typeTxt 내용을 다음 읽을 대사로 자동으로 바꾸도록 조정한다. (for문이 2겹이 되겠죠?)
        dialogType = false;
        //string typeTxt = "Intesraction TEST";
        for (int index = 0; index < Dialog.Count; index++)
        {
            dialog.text = "";
            for (int i = 0; i < Dialog[index].Length; i++)
            {
                dialog.text += Dialog[index][i].ToString();
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitUntil(() => Input.GetKey(KeyCode.LeftControl));
        }
    }
}
