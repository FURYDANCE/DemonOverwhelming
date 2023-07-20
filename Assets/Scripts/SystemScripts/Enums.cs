public enum CameraControlMode
{
    followUi, followTarget
}
/// <summary>
/// 实体类型：角色，建筑，投掷物
/// </summary>
public enum EntityType
{
    character, building, missle
}
/// <summary>
/// 阵营类型：恶魔、人类
/// </summary>
public enum Camp
{
    demon, human
}
/// <summary>
/// ai类型：战士，射手
/// </summary>
public enum AiType
{
    warrior, archer
}
/// <summary>
/// 投射物移动方式：直线，抛物线，瞬移
/// </summary>
public enum MissileMoveType
{
    direct, parabola, teleport
}
/// <summary>
/// 单位移动方式：直线移动，玩家控制移动
/// </summary>
public enum Ai_MoveType
{
    directMove, playerInputMove
}
/// <summary>
/// 单位索敌方式：选取最近，
/// </summary>
public enum Ai_CheckType
{
    checkNearest,
}
/// <summary>
/// 单位追击方式：尝试靠近，
/// </summary>
public enum Ai_ChaseType
{
    tryColser,
}
/// <summary>
/// 单位攻击方式：直接进行攻击，
/// </summary>
public enum Ai_AttackType
{
    nomalAttack
}
/// <summary>
/// 存放各种枚举变量的类
/// </summary>
public class Enums { }
