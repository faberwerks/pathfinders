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
        /// <summary>
        /// Determines whether to take into account the presence of a Neighbouring tile or not.
        /// </summary>
        /// DontCare    -> don't take into account
        /// This        -> take into account that it's there
        /// NotThis     -> take into account that it's not there
        /// AnotherTile -> take into account that another tile is there
        public enum Neighbour { DontCare, This, NotThis, AnotherTile }
        public enum OutputSprite { Single, Random }         // determines how output sprite is chosen

        public Neighbour[] neighbours;          // surrounding tiles
        public Sprite[] sprites;                // output sprites
        public float perlinScale;               // degree of noise (randomness)
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
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    int index = GetIndexOfOffset(offset);
                    TileBase baseTile = tilemap.GetTile(position + offset);
                    RuleTile tile = baseTile as RuleTile;
                    // check whether the currently checked tile is a rule tile
                    if (tile == null)
                    {
                        if (rule.neighbours[index] == TilingRule.Neighbour.This || rule.neighbours[index] == TilingRule.Neighbour.AnotherTile)
                        {
                            return false;
                        }

                        continue;
                    }
                    // check whether the currently checked tile is taken into account and whether it is the same rule tile instance
                    if (rule.neighbours[index] == TilingRule.Neighbour.This && !(this.IsOfSameInstance(tile)) || rule.neighbours[index] == TilingRule.Neighbour.NotThis && this.IsOfSameInstance(tile) || rule.neighbours[index] == TilingRule.Neighbour.AnotherTile && this.IsOfSameInstance(tile))
                    {
                        return false;
                    }
                }
            }
        }

        transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f), Vector3.one);
        return true;
    }

    /// <summary>
    /// Gets the index of a tile at an offset from the current tile.
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

    /// <summary>
    /// Checks whether this tile and target tile are tiles of the same Rule Tile instance.
    /// </summary>
    /// <param name="targetTile">Tile to be checked.</param>
    /// <returns></returns>
    public bool IsOfSameInstance(RuleTile targetTile)
    {
        if (targetTile != null)
        {
            if (this.defaultSprite != targetTile.defaultSprite)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
