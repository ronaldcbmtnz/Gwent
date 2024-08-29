using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis;
using System.Linq;
namespace ClassLibrary
{

public partial class Parser
{
    public Parser(List<Token> tokens, List<Error> CompilingErrors)
    {
        this.tokens = tokens;
        compilingErrors = CompilingErrors;
    }

    private List<Token> tokens;
    private readonly List<Error> compilingErrors;
    private int current = 0;
    private Token currentToken => tokens[current];
    private Token previousToken => tokens[current - 1];


    // Auxiliary Methods 
    private bool Match(params TokenType[] types)
    {
        if (current >= tokens.Count)
            return false;

        foreach (var type in types)
            if (tokens[current].Type == type)
            {
                current++;
                return true;
            }

        return false;
    }

    private Token Expect(TokenType tokenType)
    {
        if (Match(tokenType))
            return previousToken;

        var syntaxError = new Error(currentToken.Line, currentToken.Col, ErrorType.SyntaxError,
                        $"{tokenType} Token Expected");
        compilingErrors.Add(syntaxError);
        return null;
    }


    private void Expect(params TokenType[] tokenTypes)
    {
        for (int i = 0; i < tokenTypes.Length; i++)
            if (!Match(tokenTypes[i]))
            {
                var syntaxError = new Error(currentToken.Line, currentToken.Col, ErrorType.SyntaxError,
                                $"{tokenTypes[i]} Token Expected");
                compilingErrors.Add(syntaxError);
                break;
            }
    }

    private Token Next(int offset = 0)
    {
        if (current + offset >= tokens.Count)
            return tokens.Last(); // Should be EOF
        else
            return tokens[current + offset];
    }
    private void ThrowError() { }
}
}