using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 3f;
    public float waittime = 1f;
    public float basicvelocity = 1f;
    public Vector3 youtarget = new Vector3(10, 12, 0);
    public Vector3 zuotarget = new Vector3(-10, 12, 0);
    private bool wait = false;
    private bool moveright = true;
    private float alldistance;
    // Start is called before the first frame update
    void Start()
    {
        alldistance= Vector3.Distance(youtarget, zuotarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (wait)
        {
            return;
        }

        if (moveright)
        {
            float formzuo = Vector3.Distance(transform.position, zuotarget);
            float time = formzuo / alldistance;    // 比例 time：0 在左端点，1 在右端点
            float a = Mathf.Cos(time * Mathf.PI);  // 余弦函数，a：1 在左端点，-1 在右端点
            float real = Mathf.Abs(a);             // 取绝对值，real：1 在两端点，0 在中点
            float realspeed = speed * real+basicvelocity;        // 速度随位置变化，越靠近中点速度越慢，越靠近两端点速度越快
            transform.position += Vector3.right * realspeed * Time.deltaTime;
            if (transform.position.x >= youtarget.x)
            {
                moveright = false;
                StartCoroutine(waitpoint());
            }

        }
        else
        {
            float formyou = Vector3.Distance(transform.position, youtarget);
            float time = formyou / alldistance;    // 比例 time：0 在右端点，1 在左端点
            float a = Mathf.Cos(time * Mathf.PI);  // 余弦函数，a：-1 在右端点，1 在左端点
            float real = Mathf.Abs(a);             // 取绝对值，real：1 在两端点，0 在中点
            float realspeed = speed * real+basicvelocity;        // 速度随位置变化，越靠近中点速度越慢，越靠近两端点速度越快
            transform.position += Vector3.left * realspeed * Time.deltaTime;
            if (transform.position.x <= zuotarget.x)
            {
                moveright = true;
                StartCoroutine(waitpoint());
            }
        }
    }

    IEnumerator waitpoint()
    {
        wait = true;
        yield return new WaitForSeconds(waittime);
        wait = false;
    }
}
