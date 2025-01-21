using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ThoughtBubble : MonoBehaviour
{
    private PlayerController _playerController;
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
}
