using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountUpTimer : MonoBehaviour
{

public UnityEvent OnCountDone;
    public int CountTo;
    public int CurrentCount;
    public UnityEvent OnCount;

    public GameObject Icon;

    public bool active;
    
    public 
    // Start is called before the first frame update


    void Start()
    {
        CurrentCount = 0;
        active = false;
        StartCoroutine(DrawTimer());
    }
    public void StartCount()
    {
        active = true;
    }

        public void StopCount()
    {
        active = false;
    }
    
    public IEnumerator DrawTimer()
    {

        //yield on a new YieldInstruction that waits for N seconds.
        while (CountTo >= CurrentCount)
        {
        
           foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = CurrentCount; i > 0; i--)
            {
                // Create a Primitive square sprite
               GameObject square =   GameObject.Instantiate(Icon, transform.position, Quaternion.identity) as GameObject;
                Destroy(square.GetComponent<Collider>());
                square.transform.parent = transform;
                square.transform.position =
                    transform.position + Vector3.right * .2f * i;
                square.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            yield return new WaitForSeconds(1);
            if(active){
                OnCount.Invoke();
                CurrentCount++;
            }
            
        }
OnCountDone.Invoke();
    }
}
