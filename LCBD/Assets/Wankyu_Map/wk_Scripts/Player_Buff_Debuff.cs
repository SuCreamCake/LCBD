// 플레이어의 버프 디버프 변수 모음 클래스? 예정.
// 지금은 1스테이지 두발서기(기믹맵)의 삶의 무게 디버프를 위해 만듦.

public class Player_Buff_Debuff
{
    public static bool WeightOfLife { get; private set; } = false;  // 디버프: '삶의 무게' (설명: 점프 시 지구력 소모량 30% 증가(합연산))
    public static void SetWeightOfLife(bool value) { WeightOfLife = value; }


}