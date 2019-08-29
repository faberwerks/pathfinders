using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.Sprites;
using Object = UnityEngine.Object;

[CustomEditor(typeof(RuleTile))]
[CanEditMultipleObjects]
public class RuleTileEditor : Editor
{
    private ReorderableList reorderableList;
    public RuleTile tile { get { return (target as RuleTile); } }

    private const float defaultElementHeight = 48.0f;
    private const float paddingBetweenRules = 13.0f;
    private const float singleLineHeight = 16.0f;
    private const float labelWidth = 53.0f;

    public void OnEnable()
    {
        if (tile.tilingRules == null)
        {
            tile.tilingRules = new List<RuleTile.TilingRule>();
        }

        reorderableList = new ReorderableList(tile.tilingRules, typeof(RuleTile.TilingRule), true, true, true, true);
        reorderableList.drawHeaderCallback = OnDrawHeader;
        reorderableList.drawElementCallback = OnDrawElement;
        reorderableList.elementHeightCallback = GetElementHeight;
        reorderableList.onReorderCallback = ListUpdated;
    }

    public override void OnInspectorGUI()
    {
        tile.defaultSprite = EditorGUILayout.ObjectField("Default Sprite", tile.defaultSprite, typeof(Sprite), false) as Sprite;
        tile.defaultColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Default Collider", tile.defaultColliderType);
        EditorGUILayout.Space();

        if (reorderableList != null && tile.tilingRules != null)
        {
            reorderableList.DoLayoutList();
        }
    }

    private void OnDrawHeader(Rect rect)
    {
        GUI.Label(rect, "Tiling Rules");
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        RuleTile.TilingRule rule = tile.tilingRules[index];

        float yPos = rect.yMin + 2.0f;
        float height = rect.height - paddingBetweenRules;
        float matrixWidth = defaultElementHeight;

        Rect inspectorRect = new Rect(rect.xMin, yPos, rect.width - matrixWidth * 2.0f - 20.0f, height);
        Rect matrixRect = new Rect(rect.xMax - matrixWidth * 2.0f - 10.0f, yPos, matrixWidth, defaultElementHeight);
        Rect spriteRect = new Rect(rect.xMax - matrixWidth - 5.0f, yPos, matrixWidth, defaultElementHeight);

        EditorGUI.BeginChangeCheck();
        RuleInspectorOnGUI(inspectorRect, rule);
        RuleMatrixOnGUI(matrixRect, rule);
        SpriteOnGUI(spriteRect, rule);
        if (EditorGUI.EndChangeCheck())
        {
            SaveTile();
        }
    }

    private float GetElementHeight(int index)
    {
        if (tile.tilingRules != null && tile.tilingRules.Count > 0)
        {
            if (tile.tilingRules[index].output == RuleTile.TilingRule.OutputSprite.Random)
            {
                return defaultElementHeight + singleLineHeight * (tile.tilingRules[index].sprites.Length + 2) + paddingBetweenRules;
            }
        }

        return defaultElementHeight + paddingBetweenRules;
    }

    private void ListUpdated(ReorderableList list)
    {
        SaveTile();
    }

    private void SaveTile()
    {
        EditorUtility.SetDirty(target);
        SceneView.RepaintAll();
    }

    private static void RuleInspectorOnGUI(Rect rect, RuleTile.TilingRule tilingRule)
    {
        float y = rect.yMin;
        EditorGUI.BeginChangeCheck();
        GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Collider");
        tilingRule.colliderType = (Tile.ColliderType)EditorGUI.EnumPopup(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.colliderType);
        y += singleLineHeight;
        GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Output");
        tilingRule.output = (RuleTile.TilingRule.OutputSprite)EditorGUI.EnumPopup(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.output);
        y += singleLineHeight;

        if (tilingRule.output == RuleTile.TilingRule.OutputSprite.Single)
        {
            GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Size");
            EditorGUI.BeginChangeCheck();
            int newLength = EditorGUI.IntField(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.sprites.Length);

            if (EditorGUI.EndChangeCheck())
            {
                Array.Resize(ref tilingRule.sprites, Math.Max(newLength, 1));
            }

            y += singleLineHeight;

            for (int i = 0; i < tilingRule.sprites.Length; i++)
            {
                tilingRule.sprites[i] = EditorGUI.ObjectField(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.sprites[i], typeof(Sprite), false) as Sprite;
                y += singleLineHeight;
            }
        }
    }
}
