using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text Counter;
    public Text ResultText;
    public GameObject ResultGame;

    public void UpdateCounterBack(float value)
    {
        Counter.text = Mathf.RoundToInt(value).ToString();
    }

    public void ShowResult(string text)
    {
        var resultBox = Instantiate(ResultGame, transform.position, transform.rotation, transform);
        var resultText = resultBox.GetComponent<Text>().text = text;
    }
}
