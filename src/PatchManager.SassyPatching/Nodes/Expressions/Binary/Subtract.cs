﻿using PatchManager.SassyPatching.Exceptions;

namespace PatchManager.SassyPatching.Nodes.Expressions.Binary;
/// <summary>
/// Represents a binary expression which subtracts the right hand side from the left hand side
/// </summary>
public class Subtract : Binary
{
    internal Subtract(Coordinate c, Expression leftHandSide, Expression rightHandSide) : base(c, leftHandSide, rightHandSide)
    {
    }

    internal override Value GetResult(Value leftHandSide, Value rightHandSide)
    {
        if (leftHandSide.IsNumber && rightHandSide.IsNumber)
        {
            return leftHandSide.Number - rightHandSide.Number;
        }

        throw new BinaryExpressionTypeException(Coordinate,"subtract", leftHandSide.Type.ToString(),
            rightHandSide.Type.ToString());
    }

    internal override bool ShortCircuitOn(Value value) => false;

    internal override Value ShortCircuitValue => null;
}