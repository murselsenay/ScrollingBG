using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public static Controller instance;
    [Header("Background Objects")]
    public List<GameObject> bg = new List<GameObject>();
    public GameObject bgContainer;
    public GameObject pieceOfBg;
    private int bgCount = 2;
    private float yOffset = 0;

    [Header("Background Sprites")]
    public Sprite blue;
    public Sprite green;
    internal bool canSwitchColor = false;
    internal int bgOrder = 0;

    [Header("Timer")]
    public Text timerText;
    public int countDown = 10;
    public Button resetBtn;
    internal int resetBtnStatus;

    [Header("Spawn Object")]
    public GameObject spawnObject;
    public Transform spawnPosition;


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateBg();
        ResetBtnDisabled();
        timerText.text = countDown.ToString();
        StartCoroutine(Timer(countDown));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTimer()
    {

        timerText.text = countDown.ToString();
        ResetBtnDisabled();
        bgOrder = 0;
        StartCoroutine(Timer(countDown));
    }

    public void CreateBg()
    {
        for (int i = 0; i < bgCount; i++)
        {
            bg.Add(Instantiate(pieceOfBg, new Vector2(0f, 0f + yOffset), Quaternion.identity));
            float height = bg[i].GetComponent<BoxCollider2D>().bounds.size.y;
            yOffset += height;
        }

    }

    public IEnumerator Timer(int _countDown)
    {
        _countDown = countDown;
        while (true)
        {
            if (_countDown >= 0)
            {
                yield return new WaitForSeconds(0.5f);
                timerText.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                timerText.gameObject.SetActive(false);
                _countDown -= 1;
                timerText.text = _countDown.ToString();
                canSwitchColor = false;
            }
            else
            {

                countDown = _countDown;
                break;
            }
        }


    }
    public void ResetBtnEnabled()
    {
        resetBtn.interactable = true;

    }
    public void ResetBtnDisabled()
    {
        resetBtn.interactable = false;

    }
    public void SwitchColor(Sprite _sprite, int _order, GameObject _targetBG)
    {
        _targetBG.GetComponent<SpriteRenderer>().sprite = _sprite;
    }
    public void Spawn()
    {
        GameObject spawnedObject = Instantiate(spawnObject, spawnPosition.position, spawnObject.transform.rotation);
        spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x, spawnedObject.transform.position.y, -1f);
    }
}
