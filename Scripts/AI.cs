using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject playerMesh;
    public GameObject finish;   //��������
    public Transform playerMark;    //������ ��ġ
    public Transform cubeMesh;
    public Transform cubemark;      //ť�갡 �������ϴ� ��ġ
    public Transform currentPlayerPosition; //�÷��̾��� ������ġ
    public Transform currentCubePosition;   //���� ť����ġ
    public float proximityValueX;
    public float proximityValueY;
    public float nearValue; // �÷��̾ �����̿� �ִ���
    public float cubeProximityX;
    public float cubeProximityY;
    public float nearCube;  // ť�갡 ������ �ִ���
    public float cubeMarkProximityX;
    public float cubeMarkProximityY;
    public bool playerOnMark;
    public bool cubeIsNear;
    public float speed;
    public bool Finding;
    public GameObject cube;
    public bool isX;
    public bool isY;
    public float absX;
    public float absY;


    public float timer;
    public int waitingTime;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        //Vector2 playerMark = new Vector2(-5.94f, 4.53f);
        // Vector2 cubeMark = new Vector2(-7.02f, 1.58f);
        nearValue = 0.5f;
        nearCube = 2.0f;
        speed = 1.0f;
        rigid = GetComponent<Rigidbody2D>();

        timer = 0.0f;
        waitingTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //���ΰ��� ���� ��ġ�� ����Ѵ�.
        currentPlayerPosition.transform.position = playerMesh.transform.position;
        //���ΰ��� �� ��ġ�� ���ΰ��� ������ ������ X�� �Ÿ��� ����Ѵ�.
        proximityValueX = playerMark.transform.position.x - currentPlayerPosition.transform.position.x;
        //���ΰ��� �� ��ġ�� ���ΰ��� ������ ������ Y�� �Ÿ��� ����Ѵ�.
        proximityValueY = playerMark.transform.position.y - currentPlayerPosition.transform.position.y;
        //���ΰ��� ���ΰ��� ������ �����̿� �ִ��� ����Ѵ�.
        if(proximityValueX+proximityValueY < nearValue)
        {
            playerOnMark = true;
        }

        cubeProximityX = this.transform.position.x - currentCubePosition.transform.position.x;
        cubeProximityY = this.transform.position.y - currentCubePosition.transform.position.y;

        if((Mathf.Abs(cubeProximityX) + Mathf.Abs(cubeProximityY)) < nearCube)
        {
            cubeIsNear = true;
        }
        else
        {
            cubeIsNear = false;
        }
        if(playerOnMark == true && cubeIsNear == false && Finding == false)
        {
            PositionChanging();
        }
        if(playerOnMark == true && cubeIsNear == true)
        {
            Finding = false;
        }
        cubeMarkProximityX = currentCubePosition.transform.position.x - cubemark.transform.position.x;
        cubeMarkProximityY = currentCubePosition.transform.position.y - cubemark.transform.position.y;
        absX = Mathf.Abs(cubeMarkProximityX);
        absY = Mathf.Abs(cubeMarkProximityY);

        if (Mathf.Round(absX)<Mathf.Round(absY) && cubeIsNear == true)
        {
            PushY();
        }
        if (Mathf.Round(absX) >= Mathf.Round(absY) && cubeIsNear == true)
            {
                PushX();
            }


        if (absX < 0.5f && absY < 0.5f)
        {
            speed = 0f;
            finish.SetActive(true);
        }

        else
            finish.SetActive(false);

        //��ֹ� �����ϱ�
        timer += Time.deltaTime;
        if(playerOnMark == true)
        {
            if (Physics2D.CircleCast(transform.position, 2.0f, Vector2.zero, 2.0f, LayerMask.GetMask("Box")))
            {
                if (timer > waitingTime)
                {
                    Attack();
                    timer = 0;
                }

            }
        }  
    }

    void PositionChanging()
    {
        Vector2 positionA = this.transform.position;
        Vector2 positionB = cubeMesh.transform.position;
        this.transform.position = Vector2.Lerp(positionA, positionB, Time.deltaTime * speed);
    }

    void PushX()
    {
        float thisx;
        Finding = true;
        float thisy = cubeMesh.transform.position.y;
        if (cubeMarkProximityX >= 0)
        {
            thisx = cubeMesh.transform.position.x + 1;
        }
        else
            thisx = cubeMesh.transform.position.x - 1;

        if (isX == false)
        {
            this.transform.position = new Vector2(thisx, thisy);
        }
        moveX();
    }

    void PushY()
    {
        float thisy;
        Finding = true;
        float thisx = cubeMesh.transform.position.x;
        if(cubeMarkProximityY >= 0 )
            thisy = cubeMesh.transform.position.y + 1;
        else
            thisy = cubeMesh.transform.position.y -1;
        if (isY == false)
        {
            this.transform.position = new Vector2(thisx, thisy);
        }
        moveY();
    }

    void moveX()
    {
        isX = true;
        isY = false;
        if(cubeMarkProximityX>0)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        else if(cubeMarkProximityX<0)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
    void moveY()
    {
        isY = true;
        isX = false;
        if (cubeMarkProximityY >= 0)
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        else if(cubeMarkProximityY < 0)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void Attack()
    {

        RaycastHit2D[] rayHits = Physics2D.CircleCastAll(transform.position, 2.0f, Vector2.zero, 2.0f, LayerMask.GetMask("Box"));
        foreach(RaycastHit2D hitobj in rayHits)
        {
            hitobj.transform.GetComponent<Box>().HitByAI();
        }
    }
}
