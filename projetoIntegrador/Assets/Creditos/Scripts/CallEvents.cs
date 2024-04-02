using System.Globalization;
using UnityEngine;

public class CallEvents : MonoBehaviour
{
    public GameObject cloudFight;
    private bool isActive = false;
    private float duration = 5.5f;
    private float timer = 0f;
    public void CallCloudFight()
    {
        cloudFight.SetActive(true);
        isActive = true;
        timer = 0f;
    }

    void Update()
    {
        if (isActive)
        {
            if (timer >= duration)
            {
                cloudFight.SetActive(false);
                isActive = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
