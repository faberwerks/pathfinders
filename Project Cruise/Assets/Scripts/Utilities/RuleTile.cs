using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Tile assets which change sprites depending on specified rules.
/// </summary>
[Serializable]
[CreateAssetMenu]
public class RuleTile : TileBase
{
    public Sprite defaultSprite;
    public Tile.ColliderType defaultColliderType;

    /// <summary>
    /// Rules which determine whether a tile asset changes sprites or not.
    /// </summary>
    [Serializable]
    public class TilingRule
    {
        //public enum TransformRule { Fixed, Rotated, MirrorX, MirrorY }
        /// <summary>
        /// Determines whether to take into account the presence of a Neighbouring tile or not.
        /// </summary>
        /// DontCare    -> don't take into account
        /// This        -> take into account that it's there
        /// NotThis     -> take into account that it's not there
        public enum Neighbour { DontCare, This, NotThis }   
        public enum OutputSprite { Single, Random }         // determines how output sprite is chosen

        public Neighbour[] neighbours;          // surrounding tiles
        public Sprite[] sprites;                // output sprites
        public float perlinScale;               // degree of noise (randomness)
        //public TransformRule transformRule;
        public OutputSprite output;
        public Tile.ColliderType colliderType;

        public TilingRule()
        {
            output = OutputSprite.Single;
            neighbours = new Neighbour[8];      // top left, top mid, top right, mid left, etc.
            sprites = new Sprite[1];
            perlinScale = 0.5f;
            colliderType = Tile.ColliderType.None;

            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i] = Neighbour.DontCare;
            }
        }

    }

    [HideInInspector]
    public List<TilingRule> tilingRules;

    public override void GetTileData(Vector3Int position, ITilemap tileMap, ref TileData tileData)
    {
        tileData.sprite = defaultSprite;
        tileData.colliderType = defaultColliderType;

        if (tilingRules.Count > 1)
        {
            tileData.flags = TileFlags.LockTransform;
            tileData.transform = Matrix4x4.identity;
        }

        // go through all rules for this tile asset
        foreach (TilingRule rule in tilingRules)
        {
            Matrix4x4 transform = Matrix4x4.identity;

            if (RuleMatches(rule, position, tileMap, ref transform))
            {
                switch (rule.output)
                {
                    case TilingRule.OutputSprite.Single:
                        tileData.sprite = rule.sprites[0];
                        break;
                    // choose random sprite
                    case TilingRule.OutputSprite.Random:
                        int index = Mathf.Clamp(Mathf.RoundToInt(Mathf.PerlinNoise((position.x + 1000000.0f) * rule.perlinScale, (position.y + 1000000.0f) * rule.perlinScale) * rule.sprites.Length), 0, rule.sprites.Length - 1);
                        tileData.sprite = rule.sprites[index];
                        break;
                }

                tileData.transform = transform;
                tileData.colliderType = rule.colliderType;
                break;
            }
        }
    }

    public override void RefreshTile(Vector3Int location, ITilemap tileMap)
    {
        if (tilingRules != null && tilingRules.Count > 0)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    base.RefreshTile(location + new Vector3Int(x, y, 0), tileMap);
                }
            }
        }
        else
        {
            base.RefreshTile(location, tileMap);
        }
    }

    // TO-DO: REFACTOR TWO RULEMATCHES METHOD OVERLOADS INTO ONE RULEMATCHES METHOD

    /// <summary>
    /// Checks whether the tile matches a given rule.
    /// </summary>
    /// <param name="rule">The rule to be checked.</param>
    /// <param name="position">Position of the tile.</param>
    /// <param name="tilemap">Reference to the tilemap.</param>
    /// <returns>Whether tile matches rule or not.</returns>
    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, ref Matrix4x4 transform)
    {
        // check rule
        if (RuleMatches(rule, position, tilemap))
        {
            transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f), Vector3.one);
            return true;
        }

        #region UNNECESSARY
        // check rule against rotations of 0, 90, 180, 270
        //for (int angle = 0; angle <= (rule.transformRule == TilingRule.TransformRule.Rotated ? 270 : 0); angle += 90)
        //{
        //    if (RuleMatches(rule, position, tilemap, angle))
        //    {
        //        transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, 0.0f, -angle), Vector3.one);
        //        return true;
        //    }
        //}

        //// check rule against x-axis mirror
        //if ((rule.transformRule == TilingRule.TransformRule.MirrorX) && RuleMatches(rule, position, tilemap, true, false))
        //{
        //    transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1.0f, 1.0f, 1.0f));
        //    return true;
        //}

        //// check rule against y-axis mirror
        //if ((rule.transformRule == TilingRule.TransformRule.MirrorY) && RuleMatches(rule, position, tilemap, false, true))
        //{
        //    transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1.0f, -1.0f, 1.0f));
        //    return true;
        //}
        #endregion

        return false;
    }

    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    int index = GetIndexOfOffset(offset);
                    TileBase tile = tilemap.GetTile(position + offset);
                    if (rule.neighbours[index] == TilingRule.Neighbour.This && tile != this || rule.neighbours[index] == TilingRule.Neighbour.NotThis && tile == this)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    // TO-DO: DETERMINE DELETE OR NOT
    #region UNNECESSARY METHODS
    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    Vector3Int rotated = GetRotatedPos(offset, angle);
                    int index = GetIndexOfOffset(rotated);
                    TileBase tile = tilemap.GetTile(position + offset);
                    if (rule.neighbours[index] == TilingRule.Neighbour.This && tile != this || rule.neighbours[index] == TilingRule.Neighbour.NotThis && tile == this)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    Vector3Int mirrored = GetMirroredPos(offset, mirrorX, mirrorY);
                    int index = GetIndexOfOffset(mirrored);
                    TileBase tile = tilemap.GetTile(position + offset);
                    if (rule.neighbours[index] == TilingRule.Neighbour.This && tile != this || rule.neighbours[index] == TilingRule.Neighbour.NotThis && tile == this)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public Vector3Int GetRotatedPos(Vector3Int original, int rotation)
    {
        switch (rotation)
        {
            case 0:
                return original;
            case 90:
                return new Vector3Int(-original.y, original.x, original.z);
            case 180:
                return new Vector3Int(-original.x, -original.y, original.z);
            case 270:
                return new Vector3Int(original.y, -original.x, original.z);
        }
        return original;
    }

    public Vector3Int GetMirroredPos(Vector3Int original, bool mirrorX, bool mirrorY)
    {
        return new Vector3Int(original.x * (mirrorX ? -1 : 1), original.y * (mirrorY ? -1 : 1), original.z);
    }
    #endregion

    // TO-DO: IMPROVE SUMMARY
    /// <summary>
    /// Gets the index of a tile of a given offset.
    /// </summary>
    /// <param name="offset">Offset position of the given tile.</param>
    /// <returns></returns>
    private int GetIndexOfOffset(Vector3Int offset)
    {
        int result = offset.x + 1 + (-offset.y + 1) * 3;
        if (result >= 4)
        {
            result--;
        }
        return result;
    }
}
