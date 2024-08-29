using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassLibrary
{
  public enum ErrorType{
    LexicalError, SemanticError, SyntaxError
}

public class Error
{
    private readonly string message;
    private readonly int line;
    private readonly int col;
    private readonly ErrorType type;

    public Error( int row, int col, ErrorType type,string message )
    {
        this.message = message;
        this.line = row;
        this.col = col;
        this.type = type;
    }

    public override string ToString() => $"{type} Error at (Line  {line}, Column  {col}) : {message}";
}
}

