using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public Action<int> OnLevelUpSkillUIUpdate = null;
    public Action<int> OnLevelUpSkilButtonUpdate = null;

    public Dictionary<int, int> SkillListAndPoint = new Dictionary<int, int>();
    public int _level;
    public int _totalSkillCount = 4;
    public static int SkillPoint = 2; //1업당 2포인트.

    public void Add(int skillId)
    {
        SkillListAndPoint.Add(skillId, 0);
    }

    public void LevelUp(int level)
    {
        _level = level;     
        SkillPoint += 2;
        if (OnLevelUpSkillUIUpdate != null)
        {
            OnLevelUpSkillUIUpdate.Invoke(level);
        }
    }

    public bool SkillUp(int skillId)
    {
        if (SkillPoint <= 0)
            return false;

        SkillPoint--;
        if (GetSkillPoint(skillId) == 0)
            SkillListAndPoint.Add(skillId, 1);
        else
            SkillListAndPoint[skillId] += 1;

        if (OnLevelUpSkilButtonUpdate != null)
        {
            OnLevelUpSkilButtonUpdate.Invoke(skillId);
        }

        return true;
    }

    public int GetSkillPoint(int slotSkillId)
    {
        if (!SkillListAndPoint.ContainsKey(slotSkillId))
            return 0;

        return SkillListAndPoint[slotSkillId];
    }

}
