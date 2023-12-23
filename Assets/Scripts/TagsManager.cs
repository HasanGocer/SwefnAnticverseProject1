using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagsManager : MonoSingleton<TagsManager>
{
    [System.Serializable]
    public class Tags
    {
        public List<string> tagNames = new List<string>();
        public List<int> tagCounts = new List<int>();
    }
    [SerializeField] Tags tags = new Tags();


    public int GetTagCount()
    {
        return tags.tagNames.Count;
    }

    public string GetTagName(int tagCount)
    {
        return tags.tagNames[tagCount];
    }

    public int GetTagCount(int tagCount)
    {
        return tags.tagCounts[tagCount];
    }
    public void AddTagCount(int tagCount, int itemCount)
    {
        tags.tagCounts[tagCount] += itemCount;
    }
}
