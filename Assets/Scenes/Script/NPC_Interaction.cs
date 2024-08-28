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

        //엔피씨가 시작할때 필요한 대사를 미리 저장해서 읽어놔야겠지요
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

    void readJSON(string json) // 제이슨 파일을 읽는 함수
    {
        //1.제이슨 파일을 열어서
        //2. 내부에서 필요한 대사들을 읽고, 배열에 저장한다
/*        return npc가 뱉을 대사들의 배열집

        대사 = [안녕 내이름은 엔피씨얏, << Index 0
            나느 지금 서있아, Index 1 i++
            잘가 ] index 2 i++*/
    }


    IEnumerator TypeWriter()
    {
        //typeTxt 내용을 다음 읽을 대사로 자동으로 바꾸도록 조정한다. (for문이 2겹이 되겠죠?)
        dialogType = false;
        string typeTxt = "Intesraction TEST";
        dialog.text = "";
        for (int i = 0; i < typeTxt.Length; i++)
        {
            dialog.text += typeTxt[i].ToString();
            yield return new WaitForSeconds(0.1f);
        }
        //한줄이 다 끝나면 
        // if (Input.GetKey(KeyCode.LeftControl)) 
        // 받으면 다음 루프문이 돌아가야함.

    }
}
