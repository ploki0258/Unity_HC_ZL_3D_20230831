using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	[SerializeField, Header("���ʳt��"), Range(0, 10)]
	public float speed = 2.5f;
	[SerializeField, Header("�����N�o"), Range(0, 10)]
	public float attackCD = 2.5f;

	[Tooltip("AI�N�z��")]
	private NavMeshAgent agent; // AI�N�z��
	[Tooltip("�ؼЪ��a")]
	private Transform target;   // �ؼЪ��a
	[Tooltip("�ʵe���")]
	private Animator ani;       // �ʵe���
	private string parWalk = "�}������";
	private string parAttack = "Ĳ�o����";

	private void Awake()
	{
		// �N�z�� = ���o����<NavMeshAgent>()
		agent = GetComponent<NavMeshAgent>();
		// �ʵe��� = ���o����<Animator>()
		ani = GetComponent<Animator>();
		// �N�z��.�t�� = ���ʳt��
		agent.speed = speed;
		// �ؼ� = �C������.�M��("�ؼЦW��").transform
		target = GameObject.Find("�p���").transform;
	}

	private void Update()
	{
		// �N�z��.�]�w�ت��a(�ؼ�.�y��)
		agent.SetDestination(target.position);

		Debug.Log($"<color=#f69>�Z���G{agent.remainingDistance}</color>");

		// �p�G �P���a�����Z�� �j�� ����Z��(�|���a�񪱮a)
		if (agent.remainingDistance > agent.stoppingDistance)
		{
			// ���񨫸��ʵe
			ani.SetBool(parWalk, true);
		}
		// �_�h�p�G �P���a�����Z�� ������ 0
		else if (agent.remainingDistance != 0)
		{
			// ��������ʵe
			ani.SetTrigger(parAttack);
			// �������
			agent.isStopped = true;
		}
	}
}
