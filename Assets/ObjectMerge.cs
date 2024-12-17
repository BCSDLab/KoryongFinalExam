using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class ObjectMerge : MonoBehaviour
{
    [SerializeField] private int level = 0;

    private static int maxLevel = 8;
    private bool isMerge = false;

    [SerializeField] private GameObject nextLevelObjectPrefab;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetIsMergeTrue() { isMerge = true; }

    private void InstantiateNextLevelObject(ObjectMerge other)
    {
        SetIsMergeTrue();
        other.SetIsMergeTrue();

        //�� ��ü ������ �߰� ���� ��ǥ ���ϱ�
        Vector3 middleLocation = (this.transform.position + other.transform.position) / 2;
        Destroy(other.gameObject);

        //�� ��ü ����
        GameObject newGrade = Instantiate(nextLevelObjectPrefab, middleLocation, Quaternion.identity);
        newGrade.GetComponent<CircleCollider2D>().enabled = true;
        newGrade.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        //�� ��ü ����
        Destroy(gameObject);
    }

    private void ProcessGameClear()
    {
        Debug.Log("Game Clear!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectMerge other = collision.gameObject.GetComponent<ObjectMerge>();

        if(other != null)      //Ÿ�� üũ
        {
            if ((this.level == other.level) && (level < maxLevel + 1) && !isMerge && !other.isMerge)      //�浹 ���� üũ
            {
                if (this.transform.position.x < other.transform.position.x 
                    || this.transform.position.y < other.transform.position.y)    //�� ������Ʈ�� �غ�
                {
                    Debug.Log("3");
                    InstantiateNextLevelObject(other);
                }
            }
        }
    }
}