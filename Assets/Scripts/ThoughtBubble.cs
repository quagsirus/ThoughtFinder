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
    [SerializeField] private Animator animator;
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
                _revealCoroutine = StartCoroutine(RevealText());
                animator.Play("FillBubble");
            }
        }
        else
        {
            if (_revealCoroutine != null) StopCoroutine(_revealCoroutine);
            revealingText = false;
            textTMP.text = "";
            animator.Play("EmptyBubble");
        }
        transform.LookAt(new Vector3(_playerController.transform.position.x, transform.position.y, _playerController.transform.position.z));
    }

    private IEnumerator RevealText()
    {
        textTMP.text = "";
        yield return new WaitForSeconds(0.8f);
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
