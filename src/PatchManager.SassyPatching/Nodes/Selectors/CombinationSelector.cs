﻿using PatchManager.SassyPatching.Interfaces;

namespace PatchManager.SassyPatching.Nodes.Selectors;
/// <summary>
/// Represents a selector that selects all selectables that get selected by any of the selectors contained within
/// </summary>
public class CombinationSelector : Selector
{
    /// <summary>
    /// The list of selectors to get the combination selection of
    /// </summary>
    public readonly List<Selector> Selectors;
    internal CombinationSelector(Coordinate c, Selector lhs, Selector rhs) : base(c)
    {
        Selectors = new();
        if (lhs is CombinationSelector lcs)
        {
            Selectors.AddRange(lcs.Selectors);
        }
        else
        {
            Selectors.Add(lhs);
        }

        if (rhs is CombinationSelector rcs)
        {
            Selectors.AddRange(rcs.Selectors);
        }
        else
        {
            Selectors.Add(rhs);
        }
    }

    /// <inheritdoc />
    public override List<ISelectable> SelectAll(List<ISelectable> selectables)
    {
        var result = new List<ISelectable>();
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var selector in Selectors)
        {
            result = SelectionUtilities.CombineSelections(result, selector.SelectAll(selectables));
        }
        return result;
    }

    /// <inheritdoc />
    public override List<ISelectable> SelectAllTopLevel(string type, string name, string data, out ISelectable rulesetMatchingObject)
    {
        var start = new List<ISelectable>();
        rulesetMatchingObject = null;
        foreach (var selector in Selectors)
        {
            // ReSharper disable once IdentifierTypo
            start = SelectionUtilities.CombineSelections(start,selector.SelectAllTopLevel(type, name, data, out var rsmo));
            if (rsmo != null && rulesetMatchingObject == null)
            {
                rulesetMatchingObject = rsmo;
            }
        }
        return start;
    }

    public override List<ISelectable> CreateNew(List<DataValue> rulesetArguments, out INewAsset newAsset)
    {
        var start = new List<ISelectable>();
        newAsset = null;
        foreach (var selector in Selectors)
        {
            start = SelectionUtilities.CombineSelections(start, selector.CreateNew(rulesetArguments, out var na));
            if (na != null && newAsset == null)
            {
                newAsset = na;
            }
        }

        return start;
    }
}