using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 小窗口控制类
/// </summary>
public class WindowControl : MonoBehaviour
{
    public Button windowShowButton;
    public GameObject windowGO;
    private bool windowShowFlag = true;         // 窗口展示与关闭的判断标志位
    
    void Start()
    {
        // 按钮添加点击监听
        windowShowButton.onClick.AddListener (OnClick);
    }
    
    /// <summary>
    /// 窗口按钮点击监听
    /// </summary>
    private void OnClick(){
        windowGO.SetActive(windowShowFlag);
        windowShowFlag = !windowShowFlag;
    }
}
