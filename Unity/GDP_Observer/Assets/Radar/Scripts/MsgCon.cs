using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgCon : MonoBehaviour
{
    public Text messageText;   // ����

    private void Start()
    {
        //messageText = this.GetComponent<Text>();
        messageText.enabled = false;
    }

    public void SetMessage(GameObject obj)
    {
        messageText.text = obj.name + "ȹ��";
        messageText.enabled = true;
    }
}
