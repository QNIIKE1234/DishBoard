using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDataPrefabs : MonoBehaviour
{
    public List<StageMonster> stageMonsterData = new List<StageMonster>();

    [System.Serializable]
	public class StageMonster
	{
		public string ID;
		public string Name;
        public List<CharacterDatabase> monsterData = new List<CharacterDatabase>();

	}
    // public List<CharacterDatabase> NPCData = new List<CharacterDatabase>();
    // public List<CharacterDatabase> MonsterData = new List<CharacterDatabase>();

}
