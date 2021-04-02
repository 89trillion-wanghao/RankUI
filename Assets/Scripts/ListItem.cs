using System.Collections.Generic;
/// <summary>
/// JSON排行榜元素数据类
/// </summary>
public class ListItem
{
    public string uid { get; set; }
    public string nickName { get; set; }
    
    public int avatar { get; set; }
    public int trophy { get; set; }
    
    public string thirdAvatar { get; set; }
    
    public int onlineStatus { get; set; }
    
    public int role { get; set; }
    
    public string abb { get; set; }
}

 /// <summary>
 /// 数据集合类
 /// </summary>
public class Root
{
    public int countDown { get; set; }
    
    public List <ListItem > list { get; set; }
    
    public int seasonID { get; set; }
    
    public int selfRank { get; set; }
    
}

/// <summary>
/// 数据排序类
/// </summary>
public class IcpCupNum : IComparer<ListItem>
{
    //按奖杯排序
    public int Compare(ListItem x, ListItem y)
    {
        return y.trophy.CompareTo(x.trophy);
    }
}