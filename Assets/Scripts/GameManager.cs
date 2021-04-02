using System;
using System.Collections;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏管理者，游戏入口
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager istance = null;
    public Text timeCountDown;      // 计时器Text引用
    public Root r;                  // 游戏资源对象
    private int timeLeft;           // 游戏剩余时间，单位为秒
    void Awake()
    {

        //获取设置当前屏幕分辩率 

        // 初始化单例
        istance = this;
        // 获取Json数据
        GetJson();
        // 从Json里获取游戏的倒计时
        timeLeft = r.countDown;
        StartCoroutine(TimeCountDown());

    }

    private void Update()
    {

    }

    /// <summary>
    /// 获取JSON中的数据
    /// </summary>
    private void GetJson()
    {
        StreamReader streamreader = new StreamReader( Application.streamingAssetsPath+Constant.JSON_PATH);//读取数据，转换成数据流
        JsonReader js = new JsonReader(streamreader);//再转换成json数据
        r = JsonMapper.ToObject<Root>(js);//读取
    }
    
    /// <summary>
    /// 倒计时功能
    /// </summary>
    /// <returns>等待1s</returns>
    IEnumerator TimeCountDown()
    {
        while (timeLeft>0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            int day = timeLeft / 86400;
            int hour = (timeLeft % 86400) / 3600;
            int minute = (timeLeft % 86400 % 3600) / 60;
            int second = timeLeft % 86400 % 3600 % 60;
            
            // 对计时器文本格式化输出  Ends in:10d 23h 09m 22s
            timeCountDown.text = "Ends in:"+day+"d "+hour+"h "+minute + "m " + second+"s";
        }
    }
}
