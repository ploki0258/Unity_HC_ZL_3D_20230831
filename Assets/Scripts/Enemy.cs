using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	[SerializeField, Header("移動速度"), Range(0, 10)]
	public float speed = 2.5f;
	[SerializeField, Header("攻擊冷卻"), Range(0, 10)]
	public float attackCD = 2.5f;

	[Tooltip("AI代理器")]
	private NavMeshAgent agent; // AI代理器
	[Tooltip("目標玩家")]
	private Transform target;   // 目標玩家
	[Tooltip("動畫控制器")]
	private Animator ani;       // 動畫控制器
	private string parWalk = "開關走路";
	private string parAttack = "觸發攻擊";

	private void Awake()
	{
		// 代理器 = 取得元件<NavMeshAgent>()
		agent = GetComponent<NavMeshAgent>();
		// 動畫控制器 = 取得元件<Animator>()
		ani = GetComponent<Animator>();
		// 代理器.速度 = 移動速度
		agent.speed = speed;
		// 目標 = 遊戲物件.尋找("目標名稱").transform
		target = GameObject.Find("小柴犬").transform;
	}

	private void Update()
	{
		// 代理器.設定目的地(目標.座標)
		agent.SetDestination(target.position);

		Debug.Log($"<color=#f69>距離：{agent.remainingDistance}</color>");

		// 如果 與玩家間的距離 大於 停止距離(尚未靠近玩家)
		if (agent.remainingDistance > agent.stoppingDistance)
		{
			// 播放走路動畫
			ani.SetBool(parWalk, true);
		}
		// 否則如果 與玩家間的距離 不等於 0
		else if (agent.remainingDistance != 0)
		{
			// 播放攻擊動畫
			ani.SetTrigger(parAttack);
			// 停止不移動
			agent.isStopped = true;
		}
	}
}
