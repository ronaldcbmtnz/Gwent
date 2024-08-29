using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis;
namespace ClassLibrary
{

public partial class Parser
{
    // Main entry point to start parsing
    public ASTNode Parse() => ParseProgram();
    public Program ParseProgram()
    {
        List<Declaration> declarations = new();
        while (currentToken.Type != TokenType.EOF)
            declarations.Add(ParseDeclaration());

        return new Program(declarations);
    }

    private Declaration ParseDeclaration()
    {
        if (Match(TokenType.Effect))
            return ParseEffectDeclaration();
        if (Match(TokenType.Card))
            return ParseCardDeclaration();
        throw new Exception("Invalid declaration");
    }

    private EffectDeclaration ParseEffectDeclaration()
    {
        // "Effect" "{"  
        //      "Name" ":" STRING "," 
        //      "Params" ":" "{" (IDENTIFIER ":" IDENTIFIER)* "}" ","  ?Optional
        //      "Action" ":" "(" IDENTIFIER "," IDENTIFIER ")" "=>"  "{" block "}"
        // "}"

        Expect(TokenType.LeftBrace, TokenType.Name, TokenType.Colon);
        var effectName = Expect(TokenType.String).Lexeme; Expect(TokenType.Comma);

        var parameters = new Dictionary<Token, Token>();
        if (Match(TokenType.Params))
        {
            Expect(TokenType.Colon, TokenType.LeftBrace);

            while (Match(TokenType.Identifier))
            {       
                var key = previousToken;
                Expect(TokenType.Colon);
                var value = Expect(TokenType.Identifier);
                parameters.Add(key, value);

                if (Next().Type != TokenType.RightBrace)
                    Expect(TokenType.Comma);
            }
            Expect(TokenType.RightBrace, TokenType.Comma);
        }

        Expect(TokenType.Action, TokenType.Colon, TokenType.LeftParen);
        var identifier1 = Expect(TokenType.Identifier).Lexeme;
        Expect(TokenType.Comma);
        var identifier2 = Expect(TokenType.Identifier).Lexeme;
        Expect(TokenType.RightParen, TokenType.Arrow, TokenType.LeftBrace);
        var block = ParseBlock();
        Expect(TokenType.RightBrace);

        var action = new Action(identifier1, identifier2, block);

        return new EffectDeclaration(effectName, parameters, action);
    }


    public CardDeclaration ParseCardDeclaration()
    {
        // card_declaration → "Card" "{" card_body "}";
        // card_body → "Type" ":" STRING "," 
        //             "Name" ":" STRING "," 
        //             "Faction" ":" STRING "," 
        //             "Power" ":" NUMBER "," 
        //             "Range" ":" "[" STRING ("," STRING)* "]" "," 
        //             "OnActivation" ":" "[" activation_member ("," activation_member)* "]";

        Expect(TokenType.Card, TokenType.LeftBrace);
        Expect(TokenType.Type, TokenType.Colon);
        var type = Expect(TokenType.String).Lexeme;
        Expect(TokenType.Comma);

        Expect(TokenType.Name, TokenType.Colon);
        var name = Expect(TokenType.String).Lexeme;
        Expect(TokenType.Comma);

        Expect(TokenType.Faction, TokenType.Colon);
        var faction = Expect(TokenType.String).Lexeme;
        Expect(TokenType.Comma);

        Expect(TokenType.Power, TokenType.Colon);
        var power = int.Parse(Expect(TokenType.Number).Lexeme);
        Expect(TokenType.Comma);

        Expect(TokenType.Range, TokenType.Colon, TokenType.LeftBracket);
        var range = new List<string>();
        do range.Add(Expect(TokenType.String).Lexeme);
        while (Match(TokenType.Comma));

        Expect(TokenType.RightBracket, TokenType.Comma);

        Expect(TokenType.OnActivation, TokenType.Colon, TokenType.LeftBracket);
        var activationMembers = new List<ActivationMember>();
        do activationMembers.Add(ParseActivationMember());
        while (Match(TokenType.Comma));
        Expect(TokenType.RightBracket);

        return new CardDeclaration(type, name, faction, power, range, activationMembers);
    }

    private ActivationMember ParseActivationMember()
    {
        // activation_member → "{" effect_instance "," selector ("," postAction)? "}";
        Expect(TokenType.LeftBrace);
        var effectInstance = ParseEffectInstance();
        Expect(TokenType.Comma);
        var selector = ParseSelector();
        PostAction postAction = null;
        if (Match(TokenType.Comma))
            postAction = ParsePostAction();
        Expect(TokenType.RightBrace);
        return new ActivationMember(effectInstance, selector, postAction);
    }

    private EffectInstance ParseEffectInstance()
    {
        // effect_instance → "{" "Effect" ":" 
        //                      "{" "Name" ":" STRING
        //                       ("," IDENTIFIER ":" literal )*  literal → STRING | NUMBER | BOOL
        //                    "}" "," 

        Expect(TokenType.LeftBrace, TokenType.Effect, TokenType.Colon);
        Expect(TokenType.LeftBrace, TokenType.Name, TokenType.Colon);
        var effectName = Expect(TokenType.String).Lexeme;

        Dictionary<string, object> effectParams = new();
        while (Match(TokenType.Comma))
        {
            var param = Expect(TokenType.Identifier).Lexeme;
            Expect(TokenType.Colon);
            if (Match(TokenType.String, TokenType.Number, TokenType.Boolean))
                effectParams[param] = previousToken.Lexeme;
            else
                ThrowError();
        }
        return new EffectInstance(effectName, effectParams);
    }


    // selector → "{" "Selector" ":" "{" 
    //                "Source" ":" STRING "," 
    //                "Single" ":" BOOLEAN "," 
    //                "Predicate" ":" predicate ","
    //                "}" 
    //             "}" 
    private Selector ParseSelector()
    {

        Expect(TokenType.LeftBrace, TokenType.Selector, TokenType.Colon);
        Expect(TokenType.LeftBrace, TokenType.Source, TokenType.Colon);
        string source = Expect(TokenType.String).Lexeme;
        Expect(TokenType.Comma);

        Expect(TokenType.Single, TokenType.Colon);
        bool isSingle = Expect(TokenType.Boolean).Lexeme == "true";
        Expect(TokenType.Comma);

        Expect(TokenType.Predicate, TokenType.Colon);
        var predicate = ParsePredicate();
        Expect(TokenType.Comma);

        Expect(TokenType.RightBrace, TokenType.RightBrace);

        return new Selector(source, isSingle, predicate);
    }

    // predicate → "(" IDENTIFIER ")" "=>" expression;
    private Predicate ParsePredicate()
    {
        Expect(TokenType.LeftParen);
        var identifier = Expect(TokenType.Identifier).Lexeme;
        Expect(TokenType.RightParen, TokenType.Arrow);
        var expr = ParseExpression();
        return new Predicate(identifier, expr);
    }

    // postAction → "PostAction" ":" "{ "Type ":" STRING ("," selector) ? "}";
    private PostAction ParsePostAction()
    {
        Expect(TokenType.PostAction, TokenType.Colon, TokenType.LeftBrace);
        Expect(TokenType.Type, TokenType.Colon);
        string type = Expect(TokenType.String).Lexeme;

        Selector selector = null;
        if (Match(TokenType.Comma))
            selector = ParseSelector();

        return new PostAction(type, selector);
    }
}
}