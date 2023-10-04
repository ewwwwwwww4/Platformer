using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] bool isArrive;

    // Update is called once per frame
   private  void Update()
    {

        if (isArrive)
        {
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
            if (transform.position.x == b.position.x)
            {
                isArrive = !isArrive;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
            if (transform.position.x == a.position.x)
            {
                isArrive = !isArrive;
            }
        }
        
    }
}
