using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassLibrary.SyntaxAnalysis.Visitor
{

public class Evaluator : IVisitor
{
    public void Visit(UnaryExpression unaryExpression)
    {
        if (unaryExpression.Operator.Type == TokenType.Not)
            unaryExpression.Value = !(bool)unaryExpression.Operand.Value!;
        else if (unaryExpression.Operator.Type == TokenType.Minus)
            unaryExpression.Value = -(int)unaryExpression.Operand.Value!;
    }

    public void Visit(BinaryExpression binaryExpression)
    {
        var Left = binaryExpression.Left;
        var Right = binaryExpression.Right;

        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
                binaryExpression.Value = (int)Left.Value! + (int)Right.Value!;
                break;
            case TokenType.Minus:
                binaryExpression.Value = (int)Left.Value! - (int)Right.Value!;
                break;
            case TokenType.Mul:
                binaryExpression.Value = (int)Left.Value! * (int)Right.Value!;
                break;
            case TokenType.Mod:
                binaryExpression.Value = (int)Left.Value! % (int)Right.Value!;
                break;
            case TokenType.Div:
                if ((int)Right.Value! == 0)
                {
                    var error = new Error(binaryExpression.Operator.Line, binaryExpression.Operator.Col, ErrorType.SemanticError,
                                    "Division by Zero Attempted");
                    System.Console.WriteLine(error);
                    return;
                }
                binaryExpression.Value = (int)Left.Value! / (int)Right.Value!;
                break;
            case TokenType.PowerOp:
                binaryExpression.Value = (int)Math.Pow((int)Left.Value!, (int)Right.Value!);
                break;
            case TokenType.Equal:
                binaryExpression.Value = Left.Value! == Right.Value!;
                break;
            case TokenType.NotEqual:
                binaryExpression.Value = Left.Value! != Right.Value!;
                break;
            case TokenType.Greater:
                binaryExpression.Value = (int)Left.Value! > (int)Right.Value!;
                break;
            case TokenType.GreaterEqual:
                binaryExpression.Value = (int)Left.Value! >= (int)Right.Value!;
                break;
            case TokenType.Less:
                binaryExpression.Value = (int)Left.Value! < (int)Right.Value!;
                break;
            case TokenType.LessEqual:
                binaryExpression.Value = (int)Left.Value! <= (int)Right.Value!;
                break;
            case TokenType.WhitespaceConcat:
                binaryExpression.Value = (string)Left.Value! + " " + (string)Right.Value!;
                break;
            case TokenType.Concat:
                binaryExpression.Value = (string)Left.Value! + (string)Right.Value!;
                break;
            case TokenType.Or:
                binaryExpression.Value = (bool)Left.Value! || (bool)Right.Value!;
                break;
            case TokenType.And:
                binaryExpression.Value = (bool)Left.Value! && (bool)Right.Value!;
                break;
        }
    }

    public void Visit(Variable variable)
    {

    }

    public void Visit(AssignExpression assignExpression)
    {

    }


    public void Visit(Literal variable)
    {
        throw new NotImplementedException();
    }

    public void Visit(PostfixUnaryExpression unaryExpression)
    {
        throw new NotImplementedException();
    }

}
}