using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpawnData_.Editors
{
    [CustomEditor(typeof(GridCon))]
    public class ItemEditor : Editor
    {
        GridCon db;
        public int InsertIndex = 0;
        void OnEnable()
        {
            db = (GridCon)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Total:" + db.ItemList.Count);
            if (GUILayout.Button("AddItem"))
            {
                AddItem();
            }

            if (GUILayout.Button("InsertItem"))
            {
                InsertItem();
            }

            InsertIndex = EditorGUILayout.IntField(InsertIndex, GUILayout.Width(40));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(100));
            GUILayout.Label("CellType", GUILayout.Width(40));
            GUILayout.Label("Seed", GUILayout.Width(40));
            GUILayout.Label("Plant", GUILayout.Width(40));
            GUILayout.Label("Growing", GUILayout.Width(40));
            GUILayout.Label("Dying", GUILayout.Width(40));
            GUILayout.Label("GrowTIme", GUILayout.Width(40));
            GUILayout.Label("DieTime", GUILayout.Width(40));
            GUILayout.Label("Cash", GUILayout.Width(40));
            GUILayout.Label("Prefab", GUILayout.Width(40));
            GUILayout.EndHorizontal();
            for (int cnt = 0; cnt < db.ItemList.Count; cnt++)
            {
                GUILayout.BeginHorizontal();
                db.ItemList[cnt].Name = GUILayout.TextField(db.ItemList[cnt].Name, GUILayout.Width(100));
                db.ItemList[cnt].CellType = EditorGUILayout.IntField(db.ItemList[cnt].CellType, GUILayout.Width(40));
                db.ItemList[cnt].Seed = GUILayout.Toggle(db.ItemList[cnt].Seed,("S"),GUILayout.Width(40));
                db.ItemList[cnt].Plant = GUILayout.Toggle(db.ItemList[cnt].Plant, ("P"), GUILayout.Width(40));
                db.ItemList[cnt].Growing = GUILayout.Toggle(db.ItemList[cnt].Growing, ("G"), GUILayout.Width(40));
                db.ItemList[cnt].Dying = GUILayout.Toggle(db.ItemList[cnt].Dying, ("D"), GUILayout.Width(40));
                db.ItemList[cnt].GrowTime = EditorGUILayout.IntField(db.ItemList[cnt].GrowTime, GUILayout.Width(40));
                db.ItemList[cnt].DieTime = EditorGUILayout.IntField(db.ItemList[cnt].DieTime, GUILayout.Width(40));
                db.ItemList[cnt].Cash = EditorGUILayout.IntField(db.ItemList[cnt].Cash, GUILayout.Width(40));
                db.ItemList[cnt].Prefab = EditorGUILayout.ObjectField(db.ItemList[cnt].Prefab, typeof(GameObject), false, GUILayout.Width(120));
                if (GUILayout.Button("X"))
                {
                    RemoveItem(cnt);
                    return;
                }

                GUILayout.BeginVertical();
                if (GUILayout.Button((""), GUILayout.Width(20), GUILayout.Height(7)))
                {
                    MoveUp(cnt);
                    return;
                }

                if (GUILayout.Button((""), GUILayout.Width(20), GUILayout.Height(7)))
                {
                    MoveDown(cnt);
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);
            base.OnInspectorGUI();
        }

        void AddItem()
        {
            db.ItemList.Add(new Items());
            if(db.ItemList.Count > 1)
            {
                db.ItemList[db.ItemList.Count - 1].CellType = db.ItemList.Count -1;
            }
        }

        void RemoveItem(int index)
        {
            for (int cnt = 0; cnt < db.ItemList.Count; cnt++)
            {
                if (cnt > index)
                {
                    db.ItemList[cnt].CellType--;
                }
            }
            db.ItemList.RemoveAt(index);
        }

        void InsertItem()
        {
            db.ItemList.Insert(InsertIndex, new Items());
            db.ItemList[InsertIndex].CellType = InsertIndex;
            for (int cnt = 0; cnt < db.ItemList.Count; cnt++)
            {
                if(cnt > InsertIndex)
                {
                    db.ItemList[cnt].CellType++;
                }
            }
        }

        void MoveDown(int index)
        {
            if (index < db.ItemList.Count)
            {
                var item = db.ItemList[index];
                db.ItemList.RemoveAt(index);
                db.ItemList.Insert(index + 1, item);
                db.ItemList[index+1].CellType += 1;
                db.ItemList[index].CellType -= 1;
            }


        }

        void MoveUp(int index)
        {
            if (index > 0)
            {
                var item = db.ItemList[index];
                db.ItemList.RemoveAt(index);
                db.ItemList.Insert(index - 1, item);
                db.ItemList[index - 1].CellType -= 1;
                db.ItemList[index].CellType += 1;
            }
        }
    }
}