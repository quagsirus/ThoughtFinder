using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ThoughtBubble : MonoBehaviour
{
    private PlayerController _playerController;
    private string yapText = "";
    private bool revealingText = false;
    private Coroutine _revealCoroutine;
    [SerializeField] private TMP_Text textTMP;
    [SerializeField] private GameObject text;
    [SerializeField] private int displayDistance = 20;
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerController.transform.position) < displayDistance)
        {
            if (!revealingText)
            {
                revealingText = true;
                transform.localScale = new Vector3(0.1f, 1, 0.1f);
                _revealCoroutine = StartCoroutine(RevealText());
            }
        }
        else
        {
            if (_revealCoroutine != null) StopCoroutine(_revealCoroutine);
            transform.localScale = new Vector3(0.1f, 1, 0.1f);
            revealingText = false;
            textTMP.text = "...";
        }
        transform.LookAt(new Vector3(_playerController.transform.position.x, transform.position.y, _playerController.transform.position.z));
    }

    private IEnumerator RevealText()
    {
        while (transform.localScale.magnitude < Vector3.one.magnitude)
        {
            transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime;
            yield return null;
        }
        textTMP.text = "";
        foreach (char c in yapText)
        {
            textTMP.text += c.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public void SetYap(string yap)
    {
        yapText = yap;
    }
}
