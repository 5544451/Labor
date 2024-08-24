using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class NPC_Interaction : MonoBehaviour
{
    GameObject line, InterActionBtn;

    public TextMeshProUGUI dialog;
    bool dialogType = true;

    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetChild(0).gameObject;
        InterActionBtn = transform.GetChild(1).gameObject;

        //dialog = line.GetComponent<TextMeshProUGUI>();

        line.SetActive(false);
        InterActionBtn.SetActive(false);
        
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("NPC"), true);
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
        if (Input.GetKey(KeyCode.LeftControl)) 
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

    IEnumerator TypeWriter()
    {
        dialogType = false;
        string typeTxt = "Intesraction TEST";
        dialog.text = "";
        for (int i = 0; i < typeTxt.Length; i++)
        {
            dialog.text += typeTxt[i].ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
