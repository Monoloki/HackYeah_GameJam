using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUiController : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TMP_Text fillText;


    public void StartCountdown(int time) {
        StartCoroutine("CountDown",time);
        StartCoroutine("Fill", time);
    }

    private IEnumerator CountDown(int time) {
        for (int i = time; i >= 0; i--) {
            fillText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator Fill(float time) {
        for (float i = 0; i < 360; i++) {
            fill.fillAmount = i / 360;
            yield return new WaitForSeconds((time-0.5f)/360);
        }
    }
}
