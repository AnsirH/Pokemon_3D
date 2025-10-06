using Pokemon3D.Enum;
using Pokemon3D.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem
{
    public class BattleTextManager : Singleton<BattleTextManager>
    {
        private Dictionary<BattleTextType, string> textDictionary = new();

        private void Start()
        {
            LoadTexts();
        }

        void LoadTexts()
        {
            TextAsset csvFile = Resources.Load<TextAsset>("BattleTexts");
            string[] lines = csvFile.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var cols = lines[i].Split(',');
                if (System.Enum.TryParse(cols[0], out BattleTextType type))
                {
                    textDictionary[type] = cols[1];
                }
            }
        }

        public string Get(BattleTextType type)
        {
            if (textDictionary.TryGetValue(type, out string result))
                return result;
            else
                return null;
        }
    }
}