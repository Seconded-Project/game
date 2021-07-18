using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;
    public GameObject target;   //ī�޶� ���� ���
    public float moveSpeed; //ī�޶��� �ӵ�
    private Vector3 targetPosition; //����� ���� ��ġ

    // Start is called before the first frame update
    void Start()
    {
      /*  if (instance == null)
        {
            instance = this;
            //���� �̵��ص� ī�޶� �ı���Ű�� �ʴ´�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }*/

    }

    // ī�޶�� �� �����Ӹ��� ������Ʈ �Ǿ���ϱ� ������ ���⼭
    void Update()
    {
        if (target.gameObject != null)
        {
            //this�� ������ �����ϰ� ī�޶� �ǹ��Ѵ�. ī�޶��� z���� Ÿ�ٺ��� �ָ��־�� Ÿ���� ȭ�鿡 ���� �� �ִ�.
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            //Time.deltaTime�� 1�ʿ� ����Ǵ� �������� �����̸� 1�ʿ� moveSpeed��ŭ �̵��ϰ� ���ش�. 
            //ī�޶��� ��ġ�� ��ȭ��Ų��. 
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

    }
}
