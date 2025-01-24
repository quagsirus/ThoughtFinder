using System.Collections.Generic;
using UnityEngine;

public class YapperManager : MonoBehaviour
{
    private List<YapperData> _allYappers = new List<YapperData>();
    public static List<Material> _materials;
    void Start()
    {
        GameObject[] yapperObjects = GameObject.FindGameObjectsWithTag("Yapper");
        foreach (GameObject yapperObj in yapperObjects)
        {
            string fullSpeech = "";
            for (int i = 0; i < 3; i++)
            {
                char c = (char)('a' + Random.Range(0,26));
                if (Random.Range(0,2) == 1) c = char.ToUpper(c);
                fullSpeech += c;
            }
            ThoughtBubble tBubble = yapperObj.GetComponentInChildren<ThoughtBubble>();
            tBubble.SetYap(fullSpeech);
            _allYappers.Add(new YapperData
            {
                Speech = fullSpeech,
                Person = yapperObj
            });
        }
    }
}

public record YapperData
{
    public string Speech;
    public GameObject Person;
}


