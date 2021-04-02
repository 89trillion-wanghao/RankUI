using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 排行榜视图层
/// </summary>
public class LoopListItem : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image icon;
    public Image levelImg;
    public Text nameText;
    public Text cupNumText;
    public Text levelnumText;
    public Button toastBtn;
    
    private int userId=-1;                          // 初始ID
    private Func<int, LoopListItemModel> GetData;   // 数据回调
    private float offset;                           // 偏移量
    private int showItemNum;                        // 可展示Item数量
    private LoopListItemModel model;                // item元素数据
    private RectTransform content;                  // 父物体content
    private Sprite[] sprites;                       // 前三名Sprite数组
    
    /// <summary>
    /// Item初始化方法
    /// </summary>
    /// <param name="id">Item ID</param>
    /// <param name="offsetY">偏移量</param>
    /// <param name="showNum">可展示数量</param>
    /// <param name="spritesArray">Sprite数组</param>
    public void Init(int id,float offsetY,int showNum,Sprite[] spritesArray)
    {
        content = (RectTransform)rectTransform.parent; 
        showItemNum = showNum;
        offset = offsetY;
        sprites = spritesArray;
        ChangeId(id);
        toastBtn.onClick.AddListener(OnToastBtnClick);
        
    }
    
    /// <summary>
    /// 获取数据的监听方法
    /// </summary>
    /// <param name="getData">回调方法</param>
    public void AddGetDataListener(Func<int, LoopListItemModel> getData)
    {
        GetData = getData;
    }
    
    /// <summary>
    /// 滑动监听
    /// </summary>
    public void OnValueChange()
    {
        int startId;
        int endId;
        UpdateIdRange(out startId,out endId);
        JudgeSelfId(startId, endId);
    }

    /// <summary>
    /// 更新ID范围
    /// </summary>
    /// <param name="startId">起始ID</param>
    /// <param name="endId">末尾ID</param>
    private void UpdateIdRange(out int startId,out int endId)
    {
        startId = Mathf.FloorToInt(content.anchoredPosition.y/(rectTransform.rect.height + offset));
        endId = startId + showItemNum - 1;
    }

    /// <summary>
    /// 判断ID区间
    /// </summary>
    /// <param name="startId">起始ID</param>
    /// <param name="endId">末尾ID</param>
    private void JudgeSelfId(int startId, int endId)
    {
        if (userId < startId)
        {
            ChangeId(endId);
        }
        else if (userId > endId)
        {
            ChangeId(startId);
        }
    }

    /// <summary>
    /// 改变ID
    /// </summary>
    /// <param name="id">ItemID</param>
    private void ChangeId(int id)
    {
        if (userId != id && JudgeIdValid(id))
        {
            userId = id;
            model = GetData(id);
            icon.sprite = model.Icon;
            nameText.text = model.Describe;
            cupNumText.text = ""+model.Cupnum;
            if (model.Levelnum <= 2)
            {
                levelImg.gameObject.SetActive(true);
                levelImg.sprite = sprites[model.Levelnum+3];
                levelImg.SetNativeSize();
            }
            else
            {
                levelImg.gameObject.SetActive(false);
                levelnumText.text =  (model.Levelnum+1).ToString();
            }
            
            SetPos();
        }
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    private void SetPos()
    {
        rectTransform.anchoredPosition = new Vector2(0, - userId * (rectTransform.rect.height + offset));
    }

    /// <summary>
    /// 判断ID
    /// </summary>
    /// <param name="id">ItemID</param>
    /// <returns></returns>
    private bool JudgeIdValid(int id)
    {
        return !GetData(id).Equals(new LoopListItemModel());
    }
    
    /// <summary>
    /// Toast监听方法
    /// </summary>
    private void OnToastBtnClick()
    {
        ToastManager.instance.ShowToast(model.Describe,model.Levelnum+1);
    }
}
