using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Yapper;

public class ThoughtBubble : MonoBehaviour
{
    private PlayerController _playerController;
    public string yapText = "";
    private bool revealingText = false;
    private Coroutine _revealCoroutine;
    [SerializeField] private TMP_Text textTMP;
    [SerializeField] private GameObject text;
    [SerializeField] private int displayDistance = 20;
    [SerializeField] private Animator animator;
    private Coroutine _colourCoroutine;
    private YapperManager _yapperManager;
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _yapperManager = GameObject.FindGameObjectWithTag("Player").GetComponent<YapperManager>();
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

    public void CheckYap()
    {
        if (_colourCoroutine != null) StopCoroutine(_colourCoroutine);
        if (_yapperManager.CorrectYapper.Speech == yapText)
        {
            _colourCoroutine = StartCoroutine(CorrectYap());
        }
        else _colourCoroutine = StartCoroutine(IncorrectYap());
    }

    private IEnumerator CorrectYap()
    {
        textTMP.color = Color.green;
        yield return new WaitForSeconds(0.8f);
        textTMP.color = Color.black;
        // do some kind of victory thing somehow
    }

    private IEnumerator IncorrectYap()
    {
        _yapperManager.incorrectGuesses++;
        textTMP.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        textTMP.color = Color.black;
    }
}