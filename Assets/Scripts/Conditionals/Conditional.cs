using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditional : MonoBehaviour
{
	public bool visible;
	public enum Condition {
		AllEnemiesDead,
		Switch,
		Timed,
		Immediately,
		Never
	}
	public Condition appearCondition, disappearCondition;
	[Tooltip("For use with the \"Timed\" condition.")]
	public float secondsUntilAppearance, appearanceDuration;
	public Room room;

	protected AudioSource sound;
	public AudioClip appearSound, disappearSound;

	Collider2D c;
	Renderer r;

	public virtual void Start()
	{
		sound = GetComponent<AudioSource>();
		c = GetComponent<Collider2D>();
		r = GetComponent<Renderer>();
		//visible = false;
		if(!visible) c.enabled = false;
		if(!visible) r.enabled = false;
	}

	public IEnumerator Appear()
	{
		if(appearCondition == Condition.Never) yield break;
		if(visible) yield break;
		if(appearCondition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		if(appearCondition == Condition.Switch)
			yield return new WaitUntil(() => room.t_switch.active);
		if(appearCondition == Condition.Timed)
			yield return new WaitForSeconds(secondsUntilAppearance);
		sound.clip = appearSound;
		sound.Play();
		visible = true;
		c.enabled = true;
		r.enabled = true;
	}

	public IEnumerator Disappear()
	{
		if(disappearCondition == Condition.Never) yield break;
		if(!visible) yield break;
		if(disappearCondition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		if(disappearCondition == Condition.Switch)
			yield return new WaitUntil(() => room.t_switch.active);
		if(appearCondition == Condition.Timed)
			yield return new WaitForSeconds(appearanceDuration - secondsUntilAppearance);
		if(disappearSound != null)
		{
			sound.clip = disappearSound;
			sound.Play();
		}
		visible = false;
		c.enabled = false;
		r.enabled = false;
	}
}
