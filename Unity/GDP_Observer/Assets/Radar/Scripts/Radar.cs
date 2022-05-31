using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RadarObject
{
    public Image icon { get; set; }

    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    // 나의 현재 위치(플레이어)
    // 맵 화면 비율에 따른 스케일 값
    // 레이더안에 들어온 객체들을 담아줄 리스트

    public Transform playerTrm;

    float mapScale = 2.0f;

    public static List<RadarObject> radObjList = new List<RadarObject>();

    private void Update()
    {
        DrawRadarDots();
    }

    void DrawRadarDots()
    {
        foreach(RadarObject radObj in radObjList)
        {
            Vector3 radarPos = (radObj.owner.transform.position - playerTrm.position);

            float distToObj = Vector3.Distance(playerTrm.position, radObj.owner.transform.position) * mapScale;
            float deltaY = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerTrm.eulerAngles.y;

            radarPos.x = distToObj * Mathf.Cos(deltaY * Mathf.Deg2Rad);
            radarPos.z = distToObj * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            radObj.icon.transform.SetParent(this.transform);
            RectTransform rt = this.GetComponent<RectTransform>();

            radObj.icon.transform.position = new Vector3(radarPos.x + rt.pivot.x, 
                radarPos.z + rt.pivot.y, 0) + this.transform.position;
        }
    }

    public void ItemDropped(GameObject obj)
    {
        print("ItemDropped");
        RegisterRadarObj(obj, obj.GetComponent<Item>().icon);
    }

    public void ItemPickupped(GameObject obj)
    {
        print("ItemPickupped");
        UnRegisterRadarObj(obj);
    }

    void RegisterRadarObj(GameObject obj, Image img)
    {
        Image image = Instantiate(img);

        radObjList.Add(new RadarObject() { owner = obj, icon = image });
    }

    void UnRegisterRadarObj(GameObject obj)
    {
        //RadarObject radObj = radObjList.Find(x => x.owner == obj);
        //radObjList.Remove(radObj);
        //Destroy(obj);

        List<RadarObject> newList = new List<RadarObject>();
        for(int i = 0; i < radObjList.Count; i++)
        {
            if(radObjList[i].owner == obj)
            {
                Destroy(radObjList[i].icon);
                continue;
            }
            else
            {
                newList.Add(radObjList[i]);
            }
        }

        radObjList.RemoveRange(0, radObjList.Count);
        radObjList.AddRange(newList);

        Destroy(obj);
    }
}
