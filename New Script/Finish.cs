using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadScene�� ����ϴµ� �ʿ�

public class Finish : MonoBehaviour
{
    //��ǥ ������ ������ �� ��ȯ
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("��");
            SceneManager.LoadScene("Ai-2 jh");
        }

    }
}
