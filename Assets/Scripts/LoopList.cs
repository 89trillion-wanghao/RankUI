using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 排行榜控制层
/// </summary>
public class LoopList : MonoBehaviour
{
    public RectTransform rectTransform;
    public ScrollRect scrollRect;
    public Text rankNumText;
    public Text nameText;
    public Text cupNumText;
    public RectTransform contentRectTransform;
    
    
    private float OffsetY=15f;                      // 每个排名之间的距离
    private float itemHeight;                       // 每个排名的高度
    private List<LoopListItem> items;               // 排名元素的List集合
    private List<LoopListItemModel> models;         // 排名元素数据的List集合
    private Sprite[] sprites;                       // sprite精灵的数组
    
    /// <summary>
    /// start方法，游戏入口
    /// </summary>
    private void Start()
    {
        items = new List<LoopListItem>();
        models = new List<LoopListItemModel>();
        // 获取前三名的Sprite数组
        GetSprites();
        // 模拟数据获取
        GetJsonData();
        // 设置榜首
        SetMyLevelPanel();
        // 生成Item
        SpwanItem();
        // 设置Content尺寸
        SetContentSize();
        // 添加监听
        scrollRect.onValueChanged.AddListener(ValueChange);
    }
    
    /// <summary>
    /// 位置监听方法
    /// </summary>
    /// <param name="data">位置信息</param>
    private void ValueChange(Vector2 data)
    {
        foreach (LoopListItem item in items)
        {
            item.OnValueChange();
        }
    }
    
    /// <summary>
    /// 设置自己等级panel
    /// </summary>
    private void SetMyLevelPanel()
    {
        rankNumText.text=Constant.RANK_ONE;
        nameText.text = GameManager.istance.r.list[0].nickName;
        cupNumText.text = GameManager.istance.r.list[0].trophy.ToString();
    }
    
    /// <summary>
    /// 计算可以展示Item的数量
    /// </summary>
    /// <param name="itemHeight">每个item高度</param>
    /// <param name="offset">item间距</param>
    /// <returns>Item数量</returns>
    private int GetShowItemNum(float itemHeight,float offset)
    {
        float height = rectTransform.rect.height;
        return Mathf.CeilToInt(height/(itemHeight + offset)) + 1;
    }

    /// <summary>
    /// 生成Item
    /// </summary>
    private void SpwanItem()
    {
        LoopListItem item = Resources.Load<LoopListItem>(Constant.ITEM_PATH);
        itemHeight = item.rectTransform.rect.height;
        int num = GetShowItemNum(itemHeight,OffsetY);
        
        for (int i = 0; i < num; i++)
        {
            LoopListItem temp = Instantiate<LoopListItem>(item, contentRectTransform);
            temp.AddGetDataListener(GetData);
            temp.Init(i,OffsetY,num,sprites);
            items.Add(temp);
        }
    }

    /// <summary>
    /// 获取对象数据
    /// </summary>
    /// <param name="index">索引值</param>
    /// <returns>item数据对象</returns>
    private LoopListItemModel GetData(int index)
    {
        if(index < 0 || index>= models.Count)
            return new LoopListItemModel();
        return models[index];
    }

    /// <summary>
    /// 从json中获取数据
    /// </summary>
    private void GetJsonData()
    {
        int i =0;
        GameManager.istance.r.list.Sort(new IcpCupNum());
        foreach (var temp in GameManager.istance.r.list)
        {
            models.Add(new LoopListItemModel(Resources.Load<Sprite>(Constant.USERHEAD_SPRITES_PATH),null,
                temp.nickName,temp.trophy,i));
            i++;
        }
    }

    /// <summary>
    /// 设置Content的尺寸
    /// </summary>
    private void SetContentSize()
    {
        float y = models.Count*itemHeight + (models.Count - 1)*OffsetY;
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x,y);
    }
    
    /// <summary>
    /// 获取前三名的Sprite
    /// </summary>
    private void GetSprites()
    {
        sprites = new Sprite[6];
        sprites = Resources.LoadAll<Sprite>(Constant.RANK_SPRITES_PATH);
    }
}
