using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SiteEnum { 
up =0,
down=1,
left =2,
right=3
}

public class PlayerWarningController : MonoBehaviour {
    [SerializeField] private Image arrowUp;
    [SerializeField] private Image arrowDown;
    [SerializeField] private Image arrowLeft;
    [SerializeField] private Image arrowRight;
    [SerializeField] private float warningTime = 1;
    [SerializeField] private int startingAlpha = 130;

    private IEnumerator ShowWarning(SiteEnum site) {
        Image siteImage = GetArrow(site);

        for (float i = startingAlpha; i >= 0 ; i--) {
            siteImage.color = new Color(siteImage.color.r, siteImage.color.g, siteImage.color.b,i/startingAlpha);
            yield return new WaitForSeconds(warningTime / startingAlpha);
        }
    }

    private Image GetArrow(SiteEnum site) {
        Image result;
        switch (site) {
            case SiteEnum.up:
                result = arrowUp;
                break;
            case SiteEnum.down:
                result = arrowDown;
                break;
            case SiteEnum.left:
                result = arrowLeft;
                break;
            case SiteEnum.right:
                result = arrowRight;
                break;
            default:
                result = arrowUp;
                break;
        }
        return result;
    }
}
