using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HealthBarCTRL : MonoBehaviour
{
    public Image health_image;
    public Image health_fade;

    private void Update()
    {
        health_image.fillAmount = HealthPointCTRL.health_point / 100f;
        health_fade.fillAmount = HealthPointCTRL.fade_health_point / 100f;
    }

}
