using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassLibrary 
{
  public enum TokenType
{
    // Operators
    Minus, Plus, Div, Mul,
    Not, NotEqual, Equal, EqualEqual,
    Greater, GreaterEqual, Less, LessEqual,
    And, Or, PowerOp, Mod, Increment, Decrement,

    // Concats
    Concat, WhitespaceConcat,

    // Literals
    Identifier, String, Number, Boolean,

    // Keywords
    If, Else, For, In, While,

    // Auxiliary
    LeftParen, RightParen, LeftBrace, RightBrace,LeftBracket, RightBracket,
    Comma, Dot, Semicolon, Colon, Arrow,

    // Invalid Token
    InvalidToken, InvalidString,

    // DSL
    Effect, Params, Action, Card,
    Type, Name, Faction, Power, Range,
    OnActivation, EffectInstance, Selector,
    Source, Single, Predicate, PostAction,

    // EOF
    EOF
}



public class Token
{
    public TokenType Type { get; set; }
    public string Lexeme { get; set; }
    public int Line { get; private set; }
    public int Col { get; private set; }

    public Token(TokenType type, string lexeme, int line, int col)
    {
        Type = type;
        Lexeme = lexeme;
        Line = line;
        Col = col;
    }

    public override string ToString() => $"{Type} : {Lexeme} at Line  {Line}, Col. {Col}";

    public static readonly Dictionary<TokenType, string> Patterns = new()
    {
        { TokenType.Number, @"^\d+(\.\d+)?"},
        { TokenType.String, @"^\\\""(.*?)\\\"""},
        { TokenType.Plus, @"^\+"},
        { TokenType.Increment, @"^\+\+"},
        { TokenType.Minus, @"^-"},
        { TokenType.Decrement, @"^--"},
        { TokenType.Mul, @"^\*"},
        { TokenType.Div, @"^/"},
        { TokenType.PowerOp, @"^\^"},
        { TokenType.Mod, @"^%"},
        { TokenType.Colon, @"^:"},
        { TokenType.LeftParen, @"^\("},
        { TokenType.RightParen, @"^\)"},
        { TokenType.LeftBrace, @"^{"},
        { TokenType.RightBrace, @"^}"},
        { TokenType.Semicolon, @"^;"},
        { TokenType.Comma, @"^,"},
        { TokenType.Arrow, @"^=>,"},
        { TokenType.WhitespaceConcat, @"^@@"},
        { TokenType.Concat, @"^@"},
        { TokenType.Dot, @"^\."},
        { TokenType.Equal, @"^="},
        { TokenType.Greater, @"^>"},
        { TokenType.Less, @"^<"},
        { TokenType.GreaterEqual, @"^>="},
        { TokenType.Not, @"^!"},
        { TokenType.NotEqual, @"^!="},
        { TokenType.LessEqual, @"^<="},
        { TokenType.And, @"^&&"},
        { TokenType.Or, @"^(\|\|)"},
        { TokenType.Boolean, @"^(true\b)|^(false\b)"},
        { TokenType.If, @"\bif\b"},
        { TokenType.Else, @"\belse\b"},
        { TokenType.While, @"\bwhile\b"},
        { TokenType.For, @"\bfor\b"},
        { TokenType.In, @"\bin\b"},
        { TokenType.Card, @"\bCard\b"},
        { TokenType.Effect, @"\bEffect\b"},
        { TokenType.EqualEqual ,@"^=="},
        { TokenType.PowerOp ,@"^\*\*"},
        { TokenType.Params ,@"\bParams\b"},
        { TokenType.Action ,@"\bAction\b"},
        { TokenType.Type ,@"\bType\b"},
        { TokenType.Name ,@"\bName\b"},
        { TokenType.Faction ,@"\bFaction\b"},
        { TokenType.Power ,@"\bPower\b"},
        { TokenType.Range ,@"\bRange\b"},
        { TokenType.OnActivation ,@"\bOnActivation\b"},
        { TokenType.EffectInstance ,@"\bEffectInstance\b"},
        { TokenType.Selector ,@"\bSelector\b"},
        { TokenType.Source ,@"\bSource\b"},
        { TokenType.Single ,@"\bSingle\b"},
        { TokenType.Predicate ,@"\bPredicate\b"},
        { TokenType.PostAction ,@"\bPostAction\b"},
        { TokenType.Identifier, @"^[a-zA-Z_][a-zA-Z0-9_]*" },
        { TokenType.InvalidString, @"^\\\"".*?\n"},
        { TokenType.InvalidToken, @"^." }
    };
}
}

