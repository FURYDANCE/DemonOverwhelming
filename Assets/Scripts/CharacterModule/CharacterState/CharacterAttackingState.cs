
/// <summary>
/// 角色攻击目标的状态
/// </summary>
public class CharacterAttackingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        //Debug.Log("进入攻击状态");
        manager.isAttacking = true;
        //执行攻击模块
        manager.attackScript.Attack();
    }
    public void OnExit(CharacterStateManager manager)
    {
        manager.isAttacking = false;
        //如果存在父级兵组，则开始让阵型回归初始位置
        if (manager.entity.parentSoldierGroup)
            manager.entity.parentSoldierGroup.SetUnitBackingToRelativePos();

    }
    public void OnUpdate(CharacterStateManager manager)
    {

    }
}
