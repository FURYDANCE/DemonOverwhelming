
/// <summary>
/// ��ɫ����Ŀ���״̬
/// </summary>
public class CharacterAttackingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        //Debug.Log("���빥��״̬");
        manager.isAttacking = true;
        //ִ�й���ģ��
        manager.attackScript.Attack();
    }
    public void OnExit(CharacterStateManager manager)
    {
        manager.isAttacking = false;
        //������ڸ������飬��ʼ�����ͻع��ʼλ��
        if (manager.entity.parentSoldierGroup)
            manager.entity.parentSoldierGroup.SetUnitBackingToRelativePos();

    }
    public void OnUpdate(CharacterStateManager manager)
    {

    }
}
