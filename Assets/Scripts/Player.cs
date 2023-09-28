using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("敵人動畫")]
	public Animator aniEnemy;
	[Header("敵人老人")]
	public Enemy enemy;
	[Header("玩家動畫")]
	public Animator aniPlayer;
	[Header("玩家角色控制器")]
	public CharacterController characterPlayer;
	[Header("第三人稱控制器")]
	public ThirdPersonController thirdController;

	private void OnTriggerEnter(Collider other)
	{
		//print($"<color=#69>碰到物件：{other}</color>");

		if (other.name.Contains("過關區域")) Pass();
		if (other.name.Contains("敵人攻擊範圍")) Lose();
	}

	/// <summary>
	/// 過關方法
	/// </summary>
	private void Pass()
	{
		aniEnemy.SetBool("開關走路", false);	// 敵人動畫設為等待動畫
		enemy.enabled = false;	// 敵人系統元件 關閉
	}

	/// <summary>
	/// 失敗方法
	/// </summary>
	private void Lose()
	{
		aniPlayer.enabled = false;  // 玩家動畫系統元件 關閉
		characterPlayer.enabled = false;    // 玩家控制器元件 關閉
		thirdController.enabled = false;
	}
}
