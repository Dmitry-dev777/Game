using UnityEngine;
using UnityEngine.UI;

public class GoldMiner : MonoBehaviour
{
    public static int goldStack;

    private Text GoldCounter;

    void Start()
    {
        GoldCounter = GetComponent<Text>();
        goldStack = 0;
    }

    void Update()
    {
        GoldCounter.text = "" + goldStack;
    }
}
