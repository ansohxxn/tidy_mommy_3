using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenSecondsImg : MonoBehaviour
{
    [SerializeField] AnimationClip animClip;
    [HideInInspector] public GameObject myGameObject;
    [HideInInspector] public Transform myTransform;

    void Start()
    {
        myGameObject = gameObject;
        myTransform = GetComponent<Transform>();
        myGameObject.SetActive(false);
    }

    public void Show_TenSecondsImg(Vector2 pos)
    {
        myTransform.position = pos;
        StartCoroutine(Hide_TenSecondsImg());
    }

    IEnumerator Hide_TenSecondsImg()
    {
        yield return Managers.Co.WaitSeconds(animClip.length);
        myGameObject.SetActive(false);
    }
}
