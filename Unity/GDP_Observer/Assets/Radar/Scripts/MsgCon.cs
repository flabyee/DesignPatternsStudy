using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgCon : MonoBehaviour
{
    public Text messageText;   // °ø°£

    private void Start()
    {
        //messageText = this.GetComponent<Text>();
        messageText.enabled = false;
    }

    public void SetMessage(GameObject obj)
    {
        messageText.text = obj.name + "È¹µæ";
        messageText.enabled = true;
    }
}
