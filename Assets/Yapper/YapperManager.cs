using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yapper
{
    public class YapperManager : MonoBehaviour
    {
        public YapperData CorrectYapper;
        private readonly List<YapperData> _allYappers = new();
        
        [SerializeField] private Material[] faceMaterials, shoesMaterials, skinMaterials, tShirtMaterials;
        [SerializeField] private GameObject[] hairObjects, hatObjects;

        private void Awake()
        {
            GameObject[] yapperObjects = GameObject.FindGameObjectsWithTag("Yapper");
            Material face, shoes, shirt, skin;
            GameObject hair, hat;
            foreach (GameObject yapperObj in yapperObjects)
            {
                string fullSpeech;
                while (true)
                {
                    fullSpeech = "";
                    for (int i = 0; i < 3; i++)
                    {
                        char c = (char)('a' + Random.Range(0,26));
                        if (Random.Range(0,2) == 1) c = char.ToUpper(c);
                        fullSpeech += c;
                    }
                    face = faceMaterials[Random.Range(0,faceMaterials.Length)];
                    shirt = tShirtMaterials[Random.Range(0,tShirtMaterials.Length)];
                    shoes = shoesMaterials[Random.Range(0,shoesMaterials.Length)];
                    skin = skinMaterials[Random.Range(0,skinMaterials.Length)];
                    hair = hairObjects[Random.Range(0,hairObjects.Length)];
                    hat = hatObjects[Random.Range(0,hatObjects.Length)];
                    if (!_allYappers.Any(yapper => (yapper.Face == face &&
                                                   yapper.Shoes == shoes &&
                                                   yapper.Hair == hair &&
                                                   yapper.Hat == hat &&
                                                   yapper.Shirt == shirt &&
                                                   yapper.Skin == skin) ||
                                                   yapper.Speech == fullSpeech)) break;
                }
                ThoughtBubble tBubble = yapperObj.GetComponentInChildren<ThoughtBubble>();
                tBubble.SetYap(fullSpeech);
                yapperObj.GetComponent<YapperBuilder>().BuildYapper(face, shirt, shoes, skin, hair, hat);
                _allYappers.Add(new YapperData
                {
                    Person = yapperObj,
                    Speech = fullSpeech,
                    Face = face,
                    Shirt = shirt,
                    Shoes = shoes,
                    Skin = skin,
                    Hair = hair,
                    Hat = hat,
                });
            }
            CorrectYapper = _allYappers[Random.Range(0, _allYappers.Count)];
        }
    }

    public record YapperData
    {
        public GameObject Person;
        public string Speech;
        public Material Face, Shirt, Shoes, Skin;
        public GameObject Hair, Hat;
    }
}