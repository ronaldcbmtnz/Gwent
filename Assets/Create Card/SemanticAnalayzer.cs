using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary;
/*
namespace ClassLibrary.SyntaxAnalysis.Visitor
{

public class SemanticAnalayzer : IVisitor
{
    public void Visit(Variable variable)
    {
        throw new NotImplementedException();
    }

    public void Visit(UnaryExpression unaryExpression)
    {
        bool operandSemanticCheck = unaryExpression.Operand.CheckSemantics(compilingErrors);

        if (unaryExpression.Operator.Type == TokenType.Minus && unaryExpression.Operand.Type != ExpressionType.Arithmetic)
        {
            Error semanticError = new Error(unaryExpression.Operator.Line, unaryExpression.Operator.Col, ErrorType.SemanticError, "Invalid Operand Type, Operand must evaluate to Number");
            compilingErrors.Add(semanticError);
            return false;
        }
        else if (unaryExpression.Operator.Type == TokenType.Not && unaryExpression.Operand.Type != ExpressionType.Boolean)
        {
            Error semanticError = new Error(unaryExpression.Operator.Line, unaryExpression.Operator.Col, ErrorType.SemanticError, "Invalid Operand Type, Operand must evaluate to Bool");
            compilingErrors.Add(semanticError);
            return false;
        }

        return operandSemanticCheck;
    }

    public void Visit(BinaryExpression binaryExpression)
    {
         bool operandsSemanticCheck = binaryExpression.Left.CheckSemantics(compilingErrors) && binaryExpression.Right.CheckSemantics(compilingErrors);

        bool thisSemanticCheck = true;
        switch (binaryExpression.Operator.Type)
        {
            case TokenType.Plus:
            case TokenType.Minus:
            case TokenType.Mul:
            case TokenType.Div:
            case TokenType.Mod:
            case TokenType.Power:
            case TokenType.Greater:
            case TokenType.GreaterEqual:
            case TokenType.Less:
            case TokenType.LessEqual:
                if (binaryExpression.Left.Type != ExpressionType.Arithmetic || binaryExpression.Right.Type != ExpressionType.Arithmetic)
                {
                    Error semanticError = new Error(binaryExpression.Operator.Line, binaryExpression.Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to Number");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;

            case TokenType.Concat:
            case TokenType.WhitespaceConcat:
                if (binaryExpression.Left.Type != ExpressionType.String || binaryExpression.Right.Type != ExpressionType.String)
                {
                    Error semanticError = new Error(binaryExpression.Operator.Line, binaryExpression.Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to String");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;
            case TokenType.And:
            case TokenType.Or:
                if (binaryExpression.Left.Type != ExpressionType.Boolean || binaryExpression.Right.Type != ExpressionType.Boolean)
                {
                    Error semanticError = new Error(binaryExpression.Operator.Line, binaryExpression.Operator.Col, ErrorType.SemanticError,
                                        "Invalid Operands Type, Both Operand must evaluate to Boolean");
                    compilingErrors.Add(semanticError);
                    thisSemanticCheck = false;
                }
                break;
            default: // If not any of above operators
                System.Console.WriteLine("Unstated operator");
                thisSemanticCheck = false;
                break;
        }

        return operandsSemanticCheck && thisSemanticCheck;
    }

    public void Visit(AssignExpression assignExpression)
    {
        throw new NotImplementedException();
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
}*/ 