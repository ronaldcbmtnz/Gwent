using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis;
namespace ClassLibrary
{
public partial class Parser
{
    private Expression ParseExpression() => ParseOr();

    // Parses an Or expression
    private Expression ParseOr()
    {
        var left = ParseAnd();

        while (Match(TokenType.Or))
        {
            var op = previousToken;
            var right = ParseAnd();
            left = new BinaryExpression(left, op, right, ExpressionType.Boolean);
        }

        return left;
    }

    private Expression ParseAnd()
    {
        var left = ParseEquality();

        while (Match(TokenType.And))
        {
            var op = previousToken;
            var right = ParseEquality();
            left = new BinaryExpression(left, op, right, ExpressionType.Boolean);
        }

        return left;
    }

    // Parses an equality expression
    private Expression ParseEquality()
    {
        var left = ParseComparison();

        while (Match(TokenType.EqualEqual, TokenType.NotEqual))
        {
            var op = previousToken;
            var right = ParseComparison();
            left = new BinaryExpression(left, op, right, ExpressionType.Boolean);
        }

        return left;
    }

    // Parses a comparison expression
    private Expression ParseComparison()
    {
        var left = ParseTerm();

        while (Match(TokenType.Greater, TokenType.Less,
                     TokenType.GreaterEqual, TokenType.LessEqual))
        {
            var op = previousToken;
            var right = ParseTerm();
            left = new BinaryExpression(left, op, right, ExpressionType.Boolean);
        }

        return left;
    }

    // Parses a term expression
    private Expression ParseTerm()
    {
        var left = ParseFactor();

        while (Match(TokenType.Plus, TokenType.Minus, TokenType.WhitespaceConcat, TokenType.Concat))
        {
            var op = previousToken;
            var right = ParseFactor();
            left = new BinaryExpression(left, op, right, ExpressionType.Arithmetic);
        }

        return left;
    }

    // Parses a factor expression
    private Expression ParseFactor()
    {
        var left = ParsePower();

        while (Match(TokenType.Mul, TokenType.Div, TokenType.Mod))
        {
            var op = previousToken;
            var right = ParsePower();
            left = new BinaryExpression(left, op, right, ExpressionType.Arithmetic);
        }

        return left;
    }
    private Expression ParsePower()
    {
        var left = ParseUnary();

        if (Match(TokenType.Power))
        {
            // Power is right associative
            var op = previousToken;
            var right = ParsePower();
            left = new BinaryExpression(left, op, right, ExpressionType.Arithmetic);
        }

        return left;
    }

    // Parses a unary expression
    private Expression ParseUnary()
    {
        if (Match(TokenType.Not))
        {
            var op = previousToken;
            var operand = ParseUnary();
            return new UnaryExpression(op, operand, ExpressionType.Boolean);
        }

        else if (Match(TokenType.Minus, TokenType.Increment, TokenType.Decrement))
        {
            var op = previousToken;
            var operand = ParseCall();
            return new UnaryExpression(op, operand, ExpressionType.Arithmetic);
        }

        else
        {
            var operand = ParseCall();
            if (Match(TokenType.Increment, TokenType.Decrement))
            {
                var op = previousToken;
                return new PostfixUnaryExpression(op, operand, ExpressionType.Arithmetic);
            }
            return ParseCall();
        }
    }

    private Expression ParseCall()
    {
        if (!Match(TokenType.Identifier)) return ParsePrimary();

        Expression call = new Variable(previousToken);
        if (Match(TokenType.RightBracket))
        {
            var bracket = previousToken;
            var argument = ParseExpression();
            call = new Indexing(call, bracket, argument);
            Expect(TokenType.RightBracket);
        }

        while (Match(TokenType.Dot))
        {
            Expect(TokenType.Identifier);
            Token id = previousToken;
            call = new PropertyCall(call, id);

            if (Match(TokenType.LeftParen))
            {
                if (Match(TokenType.RightParen))
                    call = new MethodCall(call as PropertyCall);
                else
                {
                    var argument = ParseExpression();
                    call = new MethodCall(call as PropertyCall, argument);
                    Expect(TokenType.RightParen);
                }
            }
            if (Match(TokenType.RightBracket))
            {
                var argument = ParseExpression();
                call = new Indexing(call, previousToken, argument);
                Expect(TokenType.RightBracket);
            }
        }
        return call;
    }


    // Parses a primary expression
    private Expression ParsePrimary()
    {
        if (Match(TokenType.Number))
            return new Literal(int.Parse(previousToken.Lexeme), ExpressionType.Arithmetic);
        else if (Match(TokenType.String))
            return new Literal(previousToken.Lexeme[1..^1], ExpressionType.String);
        else if (Match(TokenType.Boolean))
            return new Literal(previousToken.Lexeme == "true", ExpressionType.Boolean);
        else if (Match(TokenType.Identifier))
            return new Literal(previousToken.Lexeme, ExpressionType.String);
        else if (Match(TokenType.LeftParen))
        {
            var expr = ParseExpression();
            Expect(TokenType.RightParen);
            return expr;
        }
        else
        {
            var syntaxError = new Error(currentToken.Line, currentToken.Col, ErrorType.SyntaxError,
                              $"Unexpected Token {currentToken.Lexeme}");
            compilingErrors.Add(syntaxError);
            current++;
            return null;
        }
    }
}
}