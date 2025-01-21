using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ThoughtBubble : MonoBehaviour
{
    private PlayerController _playerController;
    private string yapText = "";
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject farawayText;
    [SerializeField] private int displayDistance = 20;
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        bool condition = Vector3.Distance(transform.position, _playerController.transform.position) < displayDistance;
        text.SetActive(condition);
        farawayText.SetActive(!condition);
        transform.LookAt(new Vector3(_playerController.transform.position.x, transform.position.y, _playerController.transform.position.z));
    }

    private IEnumerator RevealText()
    {
        foreach (char c in text.GetComponent<TMP_Text>().text)
        {
            yield return null;
        }
    }
    
    public void SetYap(string yap)
    {
        text.GetComponent<TMP_Text>().text = yap;
    }
}
