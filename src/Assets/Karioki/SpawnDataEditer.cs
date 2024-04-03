using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static SpawnClass;
using UnityEditor.PackageManager.UI;
using Unity.VisualScripting;
using UnityEngine.Profiling;
using System.Diagnostics.Tracing;
public class SpawnDataEditer : EditorWindow
{
    [MenuItem("Window/OriginalEditorPanels/SpawnDataEditer", priority = 0)]
    static public void CreateWindow()
    {
        EditorWindow.GetWindow<SpawnDataEditer>();
    }

    [SerializeField]
    private SpawnClass BaseData;
    [SerializeField]
    private string BaseDataPath;

    private int Slider_List;
    private Vector2 scrollPosition;
    private bool ChangeWindow = false;
    private void OnEnable()
    {
        var defaultData = AssetDatabase.LoadAssetAtPath<SpawnClass>("Assets/Karioki/ClassData/SpawnClass.asset");
        this.BaseData = defaultData.Clone();
        this.BaseDataPath = AssetDatabase.GetAssetPath(defaultData);
    }
    private void OnGUI()
    {
        if (BaseData == null) this.BaseData = AssetDatabase.LoadAssetAtPath<SpawnClass>(this.BaseDataPath).Clone();
        using (new EditorGUILayout.VerticalScope(GUILayout.MaxHeight(50f)))
        {
            using (new EditorGUILayout.HorizontalScope()) 
            {
                EditorGUILayout.LabelField("ステージ選択", GUILayout.MaxWidth(100f), GUILayout.MaxHeight(20f));
                Slider_List = (int)EditorGUILayout.Slider(Slider_List, 0, this.BaseData._spawnData.Length - 1,
                         GUILayout.MaxWidth(160f), GUILayout.MaxHeight(20f));
                if (GUILayout.Button("Window切り替え", GUILayout.MaxWidth(150f), GUILayout.MaxHeight(20f)))
                {
                    ChangeWindow = !ChangeWindow;
                }
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Enemyを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Enemy");
                    int Enemy_Length = this.BaseData._spawnData[Slider_List]._enemyObject.Length;

                    System.Array.Resize(ref this.BaseData._spawnData[Slider_List]._enemyObject, Enemy_Length + 1);

                    //this.BaseData._spawnData[Slider_List]._enemyObject[Enemy_Length] = new GameObject(null);
                   
                    for (int i = 0; i < this.BaseData._spawnData[Slider_List]._spawnLateData.Length; i++)
                    {
                        System.Array.Resize(ref this.BaseData._spawnData[Slider_List]._spawnLateData[i].SpawnLate, Enemy_Length + 1);
                        this.BaseData._spawnData[Slider_List]._spawnLateData[i].SpawnLate = new int[Enemy_Length + 1];

                    }
                }

                EditorGUILayout.LabelField(":|:", GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));

                if (GUILayout.Button("Lateを追加", GUILayout.MaxWidth(120f), GUILayout.MaxHeight(20f)))
                {
                    Undo.RecordObject(BaseData, "Add Late");
                    int Late_Length = this.BaseData._spawnData[Slider_List]._spawnLateData.Length;



                    System.Array.Resize(ref this.BaseData._spawnData[Slider_List]._spawnLateData, Late_Length + 1);

                    this.BaseData._spawnData[Slider_List]._spawnLateData[Late_Length] = new SpawnClass.SpawnData.SpawnLateData()
                    {
                        IntarvalTime = 0,
                        SpawnLate = new int[this.BaseData._spawnData[Slider_List]._enemyObject.Length],
                    };

                    for (int i = 0; i < this.BaseData._spawnData[Slider_List]._enemyObject.Length; i++)
                    {
                        this.BaseData._spawnData[Slider_List]._spawnLateData[i].SpawnLate[i] = 0;
                    }
                }

                EditorGUILayout.LabelField(":|:", GUILayout.MaxWidth(20f), GUILayout.MaxHeight(20f));








                GUILayout.FlexibleSpace();

                if (GUILayout.Button("元に戻す", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    this.BaseData = AssetDatabase.LoadAssetAtPath<SpawnClass>(this.BaseDataPath).Clone();
                    EditorGUIUtility.editingTextField = false;
                }

                if (GUILayout.Button("保存", GUILayout.MaxWidth(60f), GUILayout.MaxHeight(20f)))
                {
                    var data = AssetDatabase.LoadAssetAtPath<SpawnClass>(this.BaseDataPath);
                    EditorUtility.CopySerialized(this.BaseData, data);
                    EditorUtility.SetDirty(data);
                    AssetDatabase.SaveAssets();
                }
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            using (var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition))
            {
                scrollPosition = scroll.scrollPosition;

                if (ChangeWindow) 
                {
                    var selected = this.BaseData._spawnData[Slider_List]._enemyObject;
                    if (0 < selected.Length)
                        for (int i = 0; i < selected.Length; i++)
                        {
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                                selected[i] = (GameObject)EditorGUILayout.ObjectField(selected[i], typeof(GameObject));
                            }
                        }
                }

                else {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("  ", GUILayout.MaxWidth(30f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                        EditorGUILayout.LabelField("TimeCount", GUILayout.MaxWidth(70f));
                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                    }

                    if (0 < this.BaseData._spawnData[Slider_List]._spawnLateData.Length)
                        for (int i = 0; i < this.BaseData._spawnData[Slider_List]._spawnLateData.Length; i++)
                        {
                            using (new EditorGUILayout.HorizontalScope())
                            {
                                var selected = this.BaseData._spawnData[Slider_List]._spawnLateData[i];
                                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                selected.IntarvalTime = EditorGUILayout.FloatField(selected.IntarvalTime, GUILayout.MaxWidth(70f));
                                EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                float SUM = 0f;

                                if (0 < this.BaseData._spawnData[Slider_List]._enemyObject.Length)
                                    for (int j = 0; j < this.BaseData._spawnData[Slider_List]._enemyObject.Length; j++)
                                    {
                                        selected.SpawnLate[j] = EditorGUILayout.IntField(selected.SpawnLate[j], GUILayout.MaxWidth(40f));
                                        SUM += selected.SpawnLate[j];
                                        EditorGUILayout.LabelField("|", GUILayout.MaxWidth(10f));
                                    }
                                EditorGUILayout.LabelField("合計", GUILayout.MaxWidth(50f));
                                EditorGUILayout.LabelField(SUM.ToString(), GUILayout.MaxWidth(60f));
                            }
                        }
                }
                
            }
        }

        if (Event.current.type == EventType.DragUpdated)
        {
            if (DragAndDrop.objectReferences != null &&
                DragAndDrop.objectReferences.Length > 0 &&
                DragAndDrop.objectReferences[0] is SpawnClass)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            Undo.RecordObject(this, "Change SpawnClass");
            this.BaseData = ((SpawnClass)DragAndDrop.objectReferences[0]).Clone();
            this.BaseDataPath = DragAndDrop.paths[0];
            DragAndDrop.AcceptDrag();
            Event.current.Use();
        }
        if (DragAndDrop.visualMode == DragAndDropVisualMode.Copy)
        {
            var rect = new Rect(Vector2.zero, this.position.size);
            var bgColor = Color.white * new Color(1f, 1f, 1f, 0.2f);
            EditorGUI.DrawRect(rect, bgColor);
            EditorGUI.LabelField(rect, "ここにアイテムデータをドラッグ＆ドロップしてください", ("D&D"));
        }
    }
   
    public void AddDataToMenu(GenericMenu menu)
    {
    menu.AddItem(new GUIContent("Original Menu"), false, () => Debug.Log("Press Menu!"));
    }
}
