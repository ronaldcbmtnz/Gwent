using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace ClassLibrary
{
  public class Lexer
{
    public static List<Token> Tokenize(string source, List<Error> compilingErrors)
    {
        var tokens = new List<Token>();
        int pos = 0;
        int line = 1, column = 1;

        char CurrentChar() => source[pos];

        while (pos < source.Length)
        {
            if (char.IsWhiteSpace(CurrentChar()))
            {
                pos++; column++;
                continue;
            }
            if (CurrentChar() == '\n')
            {
                pos++; line++; column = 1;
                continue;
            }

            foreach (var pattern in Token.Patterns)
            {
                var match = Regex.Match(source.Substring(pos), pattern.Value);
                if (match.Success)
                {
                    tokens.Add(new Token(pattern.Key, match.Value, line, column));
                    pos += match.Length;
                    column += match.Length;
                    break;
                }
            }
        }

        tokens.Add(new Token(TokenType.EOF, null, line, column));

        // Lexical Check
        foreach (var invalidToken in tokens.Where(token => token.Type == TokenType.InvalidToken))
        {
            var error = new Error(invalidToken.Line,invalidToken.Col,ErrorType.LexicalError,
                                $"Unknown Token '{invalidToken.Lexeme}'");
            
            compilingErrors.Add(error);
        }

        return tokens;
    }
}

}
