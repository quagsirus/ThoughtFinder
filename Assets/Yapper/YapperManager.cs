using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yapper
{
    public class YapperManager : MonoBehaviour
    {
        public YapperData CorrectYapper;
        public int incorrectGuesses = 0;
        
        private readonly List<YapperData> _allYappers = new();
        private ThoughtBubble _fountainBubble;
        
        [SerializeField] private Material[] faceMaterials, hairMaterials, shoesMaterials, skinMaterials, tShirtMaterials;
        [SerializeField] private GameObject[] hairObjects, hatObjects;

        private void Awake()
        {
            _fountainBubble = GameObject.FindWithTag("FountainBubble").GetComponent<ThoughtBubble>();
            GameObject[] yapperObjects = GameObject.FindGameObjectsWithTag("Yapper");
            Material face, hairColour, shoes, shirt, skin;
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
                    hairColour = hairMaterials[Random.Range(0, hairMaterials.Length)];
                    hat = hatObjects[Random.Range(0,hatObjects.Length)];
                    if (!_allYappers.Any(yapper => (yapper.Face == face &&
                                                   yapper.Shoes == shoes &&
                                                   yapper.Hair == hair &&
                                                   yapper.HairColour == hairColour &&
                                                   yapper.Hat == hat &&
                                                   yapper.Shirt == shirt &&
                                                   yapper.Skin == skin) ||
                                                   yapper.Speech == fullSpeech)) break;
                }
                ThoughtBubble tBubble = yapperObj.GetComponentInChildren<ThoughtBubble>();
                tBubble.SetYap(fullSpeech);
                yapperObj.GetComponent<YapperBuilder>().BuildYapper(face, hairColour, shirt, shoes, skin, hair, hat);
                _allYappers.Add(new YapperData
                {
                    Person = yapperObj,
                    Speech = fullSpeech,
                    Face = face,
                    Shirt = shirt,
                    Shoes = shoes,
                    Skin = skin,
                    Hair = hair,
                    HairColour = hairColour,
                    Hat = hat,
                });
            }
            CorrectYapper = _allYappers[Random.Range(0, _allYappers.Count)];
            _fountainBubble.SetYap(CorrectYapper.Speech);
        }
    }

    public record YapperData
    {
        public GameObject Person;
        public string Speech;
        public Material Face, HairColour, Shirt, Shoes, Skin;
        public GameObject Hair, Hat;
    }
}