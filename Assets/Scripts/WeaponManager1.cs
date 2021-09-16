using System.Collections;
using UnityEngine;

public class WeaponManager1 : MonoBehaviour
{
    public float switchDelay = 1f;
    public GameObject[] weapon;  //������ ���� ������Ʈ �迭

    private int index = 0;  // ������ �ε���
    private bool isSwitching = false;  // �����̸� Ȯ���ϱ� ����.

    // Use this for initialization
    private void Start()
    {
        InitializeWeapon();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching) // ���콺 ���� �������� �����̰� �ƴϸ� �ε��� �ø��� ����Ī
        {
            index++;
            if (index >= weapon.Length)
                index = 0;
            StartCoroutine(SwitchDelay(index)); //IEnumerator�� �����Ű�� ���ؼ� ����ϴ� �Լ�
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !isSwitching)
        {
            index--;
            if (index < 0)
                index = weapon.Length - 1;
            StartCoroutine(SwitchDelay(index));
        }

        // weapon switching by alphanum
        for (int i = 49; i < 52; i++)  //1���� 3������ Ű�� �Է¹޾� �ش��ϴ� �ε����� ���� ����Ī
        {
            if (Input.GetKeyDown((KeyCode)i) && !isSwitching && weapon.Length > i - 49 && index != i - 49)
            {
                index = i - 49;
                StartCoroutine(SwitchDelay(index));
            }
        }
    }

    private void InitializeWeapon() //������ ���۵ɶ� �ʱ�ȭ�ϴ� �κ�.
        //0�� �ε����� ���⸸ Active�ϰ� �������� Active�� false�� ��.

    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }
        weapon[0].SetActive(true);
        index = 0;
    }

    private IEnumerator SwitchDelay(int newIndex)
//�Լ��� ����ɶ� ���ð��� �������� IEnumerator�� ����ؾ��ϴµ�
//yield ������ WaitForSeconds(��)�� ���ϰ��� �������ָ� 
//�ش� �ʸ�ŭ ��⸦ �� �� �����ٺ��� �����.
//���� isSwitching�� true�� ����� switchDelay��ŭ�� �ʸ� ��ٸ� �� �ٽ� false�� �Ǵ½�.
    {
        isSwitching = true;
        SwitchWeapons(newIndex);
        yield return new WaitForSeconds(switchDelay);
        isSwitching = false;
    }

    private void SwitchWeapons(int newIndex) // �Է¹��� �ε����� ������Ʈ�� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ��.
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }
        weapon[newIndex].SetActive(true);
    }
}