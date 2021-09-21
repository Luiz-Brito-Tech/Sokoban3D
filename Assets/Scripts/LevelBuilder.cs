using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement //define cada item do level ao associar um caractere (#, por exemplo) a um prefab
{ 
    public string m_Character;
    public GameObject m_Prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
            return levelElement.m_Prefab;
        else
            return null;
    }

    public void NextLevel()
    {
        m_CurrentLevel++;
        Debug.Log(m_CurrentLevel);
        Debug.Log(GetComponent<Levels>().m_Levels.Count);
        if (m_CurrentLevel == GetComponent<Levels>().m_Levels.Count)
        {
            m_CurrentLevel = 0;//Volta para o primeiro level
        }
        Debug.Log(m_CurrentLevel);
    }

    public void Build()
    {
        m_Level = GetComponent<Levels>().m_Levels[m_CurrentLevel];
        Debug.Log(m_CurrentLevel);
        //Coordenadas offset para que o centro do level fique em 0,0
        int startx = -m_Level.Width / 2;//Salva a posição x inicial já que esta precisa ser resetada a cada fileira/linha(row)
        int x = startx;
        int z = m_Level.Height / 2;
        foreach (var row in m_Level.m_Rows)
        {
            foreach (var ch in row)
            {
                GameObject prefab = GetPrefab(ch);
                if(prefab)
                {
                    Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
                }
                x++;
            }
            z--;
            x = startx;
        }
        Debug.Log(GetComponent<Levels>().m_Levels.Count);
    }
}
