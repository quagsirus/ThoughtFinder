using System.Collections.Generic;
using UnityEngine;

public class YapperManager : MonoBehaviour
{
    private List<YapperInfo> _allYappers = new List<YapperInfo>();
    public static List<Material> _materials;
    void Start()
    {
        GameObject[] yapperObjects = GameObject.FindGameObjectsWithTag("Yapper");
        foreach (GameObject yapperObj in yapperObjects)
        {
            _allYappers.Add(new YapperInfo(yapperObj));
        }
    }
}

public class YapperInfo
{
    private string _speech;
    private GameObject _person;
    
    public YapperInfo(GameObject person)
    {
        string fullSpeech = "";
        for (int i = 0; i < Random.Range(4, 7); i++)
        {
            char c = (char)('a' + Random.Range(0,26));
            if (Random.Range(0,2) == 1) c = char.ToUpper(c);
            fullSpeech += c;
        }
        ThoughtBubble tBubble = person.GetComponentInChildren<ThoughtBubble>();
        tBubble.SetYap(fullSpeech);
        _speech = fullSpeech;
        _person = person;
    }
}


