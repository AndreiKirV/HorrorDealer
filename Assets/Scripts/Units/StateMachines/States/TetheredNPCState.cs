using UnityEngine;

public class TetheredNPCState : State
{
    private float _offset = 0.01f;
    public TetheredNPCState(StateMachine stateMachine, MinionController unitController) : base(stateMachine, unitController)
    {
    }

    public override void Entry()
    {
        base.Entry();
        float tempOffset = Random.Range(-_offset,_offset);
        _unitController.transform.position = _unitController.CurrentFetter.transform.position + new Vector3(tempOffset,tempOffset,tempOffset);//TODO пздц - поправить
        _unitController.MB.Animator.Play("Emergence");//TODO словарь для анимашек
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}