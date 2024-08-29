using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis;
namespace ClassLibrary
{

public partial class Parser
{
    // Statement → expression_statement | for_loop | while_loop ;
    private Statement ParseStatement()
    {
        if (currentToken.Type == TokenType.Identifier
            || currentToken.Type == TokenType.Increment
            || currentToken.Type == TokenType.Decrement)
            return ParseExpressionStatement();
        if (currentToken.Type == TokenType.For)
            return ParseForStatement();
        if (currentToken.Type == TokenType.While)
            return ParseWhileStatement();
        throw new Exception("Invalid Statement");
    }


    private Statement ParseExpressionStatement(){
        var expr = ParseExpression();
        Expect(TokenType.Semicolon);
        return new ExpressionStatement(expr);
    }


    // for_loop → "for" IDENTIFIER "in" IDENTIFIER "{" block "}"
    private Statement ParseForStatement()
    {
        Expect(TokenType.For);
        var identifier = Expect(TokenType.Identifier).Lexeme;
        Expect(TokenType.In);
        var collection = Expect(TokenType.Identifier).Lexeme;
        Expect(TokenType.LeftBrace);
        var body = ParseBlock();
        Expect(TokenType.RightBrace);
        return new ForStatement(identifier, collection, body);
    }

    // while_loop → "while" "("expression")" "{" block "}";
    private Statement ParseWhileStatement()
    {
        Expect(TokenType.While, TokenType.LeftParen);
        var expr = ParseExpression();
        Expect(TokenType.RightParen, TokenType.LeftBrace);
        var body = ParseBlock();
        Expect(TokenType.RightBrace);
        return new WhileStatement(expr, body);
    }

    private Statement ParseAssignmentStatement()
    {
        throw new NotImplementedException();
    }

    private Block ParseBlock()
    {
        var statements = new List<Statement>();
        while (currentToken.Type != TokenType.RightBrace && currentToken.Type != TokenType.EOF)
            statements.Add(ParseStatement());
        return new Block(statements);
    }
}
}
