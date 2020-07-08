using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject trapLayer1;
    public GameObject trapLayer2;

    bool swap;

    private void Awake()
    {
        swap = true;
        BlackBoard.gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        trapLayer2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swap()
    {
        if (swap)
        {
            trapLayer1.SetActive(false);
            trapLayer2.SetActive(true);
            swap = false;
        }
        else
        {
            trapLayer1.SetActive(true);
            trapLayer2.SetActive(false);
            swap = true;
        }
    }
}
