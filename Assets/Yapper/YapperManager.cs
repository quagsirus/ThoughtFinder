using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yapper
{
    public class YapperManager : MonoBehaviour
    {
        private readonly List<YapperData> _allYappers = new();
        public Material[] faceMaterials;
        public Material[] tShirtMaterials;
        public Material[] shoesMaterials;
        public Material[] skinMaterials;
        public GameObject[] hairObjects;
        public GameObject[] hatObjects;
        void Start()
        {
            GameObject[] yapperObjects = GameObject.FindGameObjectsWithTag("Yapper");
            Material face, shoes, shirt, skin;
            GameObject hair, hat;
            foreach (GameObject yapperObj in yapperObjects)
            {
                string fullSpeech = "";
                break;
                
                while (true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        char c = (char)('a' + Random.Range(0,26));
                        if (Random.Range(0,2) == 1) c = char.ToUpper(c);
                        fullSpeech += c;
                    }
                    face = faceMaterials[Random.Range(0,faceMaterials.Length)];
                    shirt = tShirtMaterials[Random.Range(0,tShirtMaterials.Length)];
                    shoes = shoesMaterials[Random.Range(0,shoesMaterials.Length)];
                    hair = hairObjects[Random.Range(0,hairObjects.Length)];
                    hat = hatObjects[Random.Range(0,hatObjects.Length)];
                    skin = skinMaterials[Random.Range(0,skinMaterials.Length)];
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
                _allYappers.Add(new YapperData
                {
                    Speech = fullSpeech,
                    Person = yapperObj,
                    Face = face,
                    Shirt = shirt,
                    Shoes = shoes,
                    Hair = hair,
                    Hat = hat,
                });
            }
        }
    }

    public record YapperData
    {
        public string Speech;
        public GameObject Person;
        public Material Face, Shirt, Shoes, Skin;
        public GameObject Hat, Hair;
    }
}