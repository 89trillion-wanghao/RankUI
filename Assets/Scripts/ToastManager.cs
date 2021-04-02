using UnityEngine;

/// <summary>
/// Toast 管理类
/// </summary>
public class ToastManager : MonoBehaviour
{
    public RectTransform rectTransform;
    public ToastItem toastItem;
    private GameObject toast;
    
    public static ToastManager instance = null;
    private void Start()
    {
        instance = this;
        // 加载预制体
        toast = Resources.Load(Constant.TOAST_PREFAB_PATH) as GameObject; 
    }
    
    /// <summary>
    /// 展示Toast的方法
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="level">等级</param>
    public void ShowToast(string name,int level)
    {
        toastItem.toastText.text = Constant.TOAST_USER_TEXT+name+Constant.TOAST_RANK_TEXT+level;
        GameObject tempToast = Instantiate(toast, rectTransform);
        Destroy(tempToast, 2);
    }

}
