﻿namespace PatchManager.SassyPatching.Nodes.Expressions.Binary;
/// <summary>
/// Represents a binary expression that returns true if both of its children are not equal
/// </summary>
public class NotEqualTo : Binary
{
    internal NotEqualTo(Coordinate c, Expression leftHandSide, Expression rightHandSide) : base(c, leftHandSide, rightHandSide)
    {
    }
    private bool ListCompare(List<Value> leftHandSide, List<Value> rightHandSide)
    {
        if (leftHandSide.Count != rightHandSide.Count)
        {
            return true;
        }
        return leftHandSide.Where((t, index) => GetResult(t, rightHandSide[index]).Boolean).Any();
    }

    private bool DictionaryCompare(Dictionary<string, Value> leftHandSide, Dictionary<string, Value> rightHandSide)
    {
        if (leftHandSide.Count != rightHandSide.Count)
        {
            return true;
        }

        foreach (var kv in leftHandSide)
        {
            if (rightHandSide.TryGetValue(kv.Key, out var rvalue))
            {
                if (GetResult(kv.Value, rvalue).Boolean)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    internal override Value GetResult(Value leftHandSide, Value rightHandSide)
    {
        if (leftHandSide.Type != rightHandSide.Type) return true;
        
        if (leftHandSide.IsBoolean)
        {
            return leftHandSide.Boolean != rightHandSide.Boolean;
        }

        if (leftHandSide.IsNumber)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return leftHandSide.Number != rightHandSide.Number;
        }

        if (leftHandSide.IsString)
        {
            return leftHandSide.String != rightHandSide.String;
        }

        if (leftHandSide.IsList)
        {
            return ListCompare(leftHandSide.List,rightHandSide.List);
        }

        if (leftHandSide.IsDictionary)
        {
            return DictionaryCompare(leftHandSide.Dictionary, rightHandSide.Dictionary);
        }
        
        return leftHandSide.IsNone;
    }

    internal override bool ShortCircuitOn(Value value) => false;

    internal override Value ShortCircuitValue => null;
}