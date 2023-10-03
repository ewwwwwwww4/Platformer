using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointCTRL : MonoBehaviour
{
    public static float health_point, fade_health_point;
    float fade_Timer = 0;
    public float fade_Time = 1f;
    Coroutine damage_Coroutine = null;

    private void Awake()
    {
        health_point = 100f;
        fade_health_point = 100f;

    }

    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            fade_Timer = 0;
            Damage_Once(10);

        }
        else if (Input.GetMouseButtonDown(1))

        {
            if (damage_Coroutine != null)
            {
                StopCoroutine(damage_Coroutine);
                damage_Coroutine = StartCoroutine(Damage_Over_Time(5, 2));
            }
            else
            {
                damage_Coroutine = StartCoroutine(Damage_Over_Time(5, 2)); 
            }

        }

        fade_Timer += Time.deltaTime;

        if (fade_health_point > health_point && fade_Timer > fade_Time)
        {
            fade_health_point = Mathf.Lerp(fade_health_point, health_point, Time.deltaTime * 2);
        }

    }

    public void Damage_Once(float dameage)
    {
        health_point -= dameage;
    }

    public IEnumerator Damage_Over_Time(float damage, float duration)
    {
        float timer = 0;
        while (health_point >= 0 && timer <= duration)
        {
            health_point -= damage * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
