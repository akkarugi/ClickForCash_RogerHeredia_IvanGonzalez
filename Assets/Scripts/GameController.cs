using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float click_points = 1.0f;
    [SerializeField] private float doubleclick_points = 1.5f;
    [SerializeField] private float delayClick = 3.0f;
    [SerializeField] private TMP_Text points_text;
    private bool double_click = false;
    public void CoinClick()
    {
        float temp_points = click_points;

        //Check if it's a double click
        if (double_click)
        {
            temp_points = doubleclick_points;
            StopCoroutine("DoubleClick");
            Debug.Log("Doble");
        }
        StartCoroutine("DoubleClick");

        //Check the points gained with the multiplicator
        temp_points *= GameManager.Instance.GetMultiplicator();
        GameManager.Instance.AddPoints(temp_points);

        //Update text
        if (points_text != null)
        {
            points_text.text = GameManager.Instance.points.ToString() + 
                "/" + GameManager.Instance.maxpoints.ToString();
        }
    }
    IEnumerator DoubleClick()
    {
        double_click = true;
        yield return new WaitForSeconds(delayClick);
        double_click = false;
    }
}
