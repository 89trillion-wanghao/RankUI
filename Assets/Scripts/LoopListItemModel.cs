using UnityEngine;
/// <summary>
/// 排行榜Model层
/// </summary>
public struct LoopListItemModel
{
    public Sprite Icon;
    public Sprite LevelImg;
    public string Describe;
    public int Cupnum;
    public int Levelnum;
    
    public LoopListItemModel(Sprite icon, Sprite levelImg,string describe,int cupnum,int levelnum)
    {
        Icon = icon;
        LevelImg = levelImg;
        Describe = describe;
        Cupnum = cupnum;
        Levelnum =levelnum;
        
    }
}
