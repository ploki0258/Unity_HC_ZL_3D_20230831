using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
	#region 欄位
	[SerializeField, Header("移動速度"), Range(0, 10)]
	private float speed = 2.5f;
	[SerializeField, Header("攻擊冷卻"), Range(0, 10)]
	private float attackCD = 2.5f;
	[SerializeField, Header("攻擊區域")]
	private GameObject attackArea = null;
	[SerializeField, Header("啟動攻擊區域時間"), Range(0, 5), Tooltip("啟動攻擊區域時間")]
	private float showAttackAreaTime = 1.5f;
	[SerializeField, Header("啟動攻擊區域持續時間"), Range(0, 5), Tooltip("啟動攻擊區域持續時間")]
	private float showAttackAreaDurationTime = 0.5f;

	[Tooltip("AI代理器")]
	private NavMeshAgent agent; // AI代理器
	[Tooltip("目標玩家")]
	private Transform target;   // 目標玩家
	[Tooltip("動畫控制器")]
	private Animator ani;       // 動畫控制器
	private string parWalk = "開關走路";
	private string parAttack = "觸發攻擊";
	[Tooltip("可否攻擊")]
	private bool canAttack = true;
	#endregion

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

		//StartCoroutine(Test());
	}

	private void Update()
	{
		// 代理器.設定目的地(目標.座標)
		agent.SetDestination(target.position);

		//Debug.Log($"<color=#f69>距離：{agent.remainingDistance}</color>");

		// 如果 與玩家間的距離 大於 停止距離(尚未靠近玩家)
		if (agent.remainingDistance > agent.stoppingDistance)
		{
			// 播放走路動畫
			ani.SetBool(parWalk, true);
		}
		// 否則如果 與玩家間的距離 不等於 0
		else if (agent.remainingDistance != 0)
		{
			// 如果 可以攻擊的話 就啟動攻擊協同程序
			if (canAttack == true) StartCoroutine(AttackEffect());
		}
	}

	/// <summary>
	/// 攻擊效果協同程序
	/// </summary>
	/// <returns></returns>
	private IEnumerator AttackEffect()
	{
		// 不可攻擊
		canAttack = false;
		// 播放攻擊動畫
		ani.SetTrigger(parAttack);
		// 面向座標 = 玩家的座標
		Vector3 look = target.position;
		// 面向座標.y = 自己的座標.y
		look.y = transform.position.y;
		// 面向(面向座標)
		transform.LookAt(look);
		// 停止不移動
		agent.isStopped = true;
		// 等待1秒後
		yield return new WaitForSeconds(showAttackAreaTime);
		// 顯示攻擊區域
		attackArea.SetActive(true);
		// 等待0.5秒後
		yield return new WaitForSeconds(showAttackAreaDurationTime);
		// 隱藏攻擊區域
		attackArea.SetActive(false);
		// 等待動畫 (攻擊結束馬上恢復等待動畫)
		ani.SetBool(parWalk, false);
		// 等待1.5秒後
		yield return new WaitForSeconds(attackCD);
		// 可攻擊
		canAttack = true;
		// 可以追蹤
		agent.isStopped = false;
	}

	/*private IEnumerator Test()
	{
		yield return new WaitForSeconds(3);
		Debug.Log("第一行");
		yield return new WaitForSeconds(1);
		Debug.Log("第二行");
		yield return new WaitForSeconds(2);
		Debug.Log("第三行");
	}*/
}
