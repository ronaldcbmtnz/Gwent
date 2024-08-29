using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis.Visitor;

namespace ClassLibrary.SyntaxAnalysis
{

public enum ExpressionType
{
    Arithmetic, Boolean, String,
}


public abstract class Expression : ASTNode
{
    public abstract ExpressionType Type { get; set; }
    public abstract object Value { get; set; }
}

public class Literal : Expression
{
    public override object Value { get; set; }
    public override ExpressionType Type { get; set; }

    public Literal(object value, ExpressionType type)
    {
        Value = value;
        Type = type;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);

    public override string ToString() => $"{Value}";


}

public class Variable : Expression
{
    public Variable(Token token)
    {
        ID = token.Lexeme;
    }

    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }
    public string ID { get; }
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}


public class UnaryExpression : Expression
{
    public Token Operator { get; set; } // Can be "!" or "-"
    public Expression Operand { get; set; }

    public UnaryExpression(Token operatorToken, Expression operand, ExpressionType type)
    {
        Operator = operatorToken;
        Operand = operand;
        Type = type;
    }

    public override object Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);

    public override string ToString() => (Value is null) ? $"({Operator.Lexeme}{Operand})" : $"{Value}";
}

public class PostfixUnaryExpression : Expression
{
    public Token Operator { get; set; } // Can be "++" or "--"
    public Expression Operand { get; set; }

    public PostfixUnaryExpression(Token operatorToken, Expression operand, ExpressionType type)
    {
        Operator = operatorToken;
        Operand = operand;
        Type = type;
    }

    public override object Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);

    public override string ToString() => (Value is null) ? $"({Operator.Lexeme}{Operand})" : $"{Value}";
}


public class BinaryExpression : Expression
{
    public Expression Left { get; set; }
    public Token Operator { get; set; } // Can be "*" or "/"
    public Expression Right { get; set; }
    public override object Value { get; set; }
    public override ExpressionType Type { get; set; }


    public BinaryExpression(Expression left, Token operatorToken, Expression right, ExpressionType type)
    {
        Left = left;
        Operator = operatorToken;
        Right = right;
        Type = type;
    }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);

    public override string ToString() => (Value is null) ? $"({Left} {Operator.Lexeme} {Right})"
                           : $"{Value}";

}


public class AssignExpression : Expression
{
    public AssignExpression(Expression Left, Token assignOp, Expression Right)
    {
        this.Left = Left;
        AssignOp = assignOp;
        this.Right = Right;
    }

    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }
    public Expression Left { get; }
    public Token AssignOp { get; }
    public Expression Right { get; }

    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}

public class PropertyCall : Expression
{
    public PropertyCall(Expression callee, Token member)
    {
        Callee = callee;
        Member = member;
    }

    public override ExpressionType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Expression Callee { get; }
    public Token Member { get; }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}
public class MethodCall : Expression
{
    public MethodCall(PropertyCall call,Expression argument = null)
    {
        Callee = call.Callee;
        ID = call.Member;
        Argument = argument;
    }

    public override ExpressionType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Expression Callee { get; }
    public Token ID { get; }
    public Expression Argument {get;}

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}
public class Indexing : Expression
{
    public Indexing(Expression callee, Token bracket, Expression argument)
    {
        Callee = callee;
        Bracket = bracket;
        Argument = argument;
        
    }

    public override ExpressionType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Expression Callee { get; }
    public Expression Argument { get; }
    public Token Bracket { get; }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}
}
