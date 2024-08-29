using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassLibrary.SyntaxAnalysis;
using ClassLibrary.SyntaxAnalysis.Visitor;

namespace ClassLibrary
{
   public abstract class ASTNode
{
    public abstract void Accept(IVisitor visitor);
}

public class Program : ASTNode
{
    public List<Declaration> Declarations { get; set; } = new List<Declaration>();

    public Program(List<Declaration> declarations)
    {
        Declarations = declarations;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }
}

public abstract class Declaration : ASTNode { }

public class EffectDeclaration : Declaration
{
    public string Name { get; set; }
    public Dictionary<Token, Token> Params { get; set; } = new Dictionary<Token, Token>();
    public Action Action { get; set; }

    public EffectDeclaration(string name, Dictionary<Token, Token> parameters, Action action)
    {
        Name = name;
        Params = parameters;
        Action = action;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class CardDeclaration : Declaration
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Faction { get; set; }
    public int Power { get; set; }
    public List<string> Range { get; set; } = new List<string>();
    public List<ActivationMember> OnActivation { get; set; } = new List<ActivationMember>();

    public CardDeclaration(string type, string name, string faction, int power, List<string> range, List<ActivationMember> onActivation)
    {
        Type = type;
        Name = name;
        Faction = faction;
        Power = power;
        Range = range;
        OnActivation = onActivation;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class Action : ASTNode
{
    public string Identifier1 { get; set; }
    public string Identifier2 { get; set; }
    public Block Block { get; set; }

    public Action(string identifier1, string identifier2, Block block)
    {
        Identifier1 = identifier1;
        Identifier2 = identifier2;
        Block = block;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}


public abstract class Statement : ASTNode { }

public class ExpressionStatement : Statement
{
    public Expression Expression { get; set; }

    public ExpressionStatement(Expression expression)
    {
        Expression = expression;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }
}

public class ForStatement : Statement
{
    public string VariableName { get; }
    public string CollectionName { get; }
    public Block Body { get; }

    public ForStatement(string variableName, string collectionName, Block body)
    {
        VariableName = variableName;
        CollectionName = collectionName;
        Body = body;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class WhileStatement : Statement
{
    public Expression Condition { get; set; }
    public Block Body { get; set; }

    public WhileStatement(Expression condition, Block body)
    {
        Condition = condition;
        Body = body;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }
}

public class Block : Statement
{
    public List<Statement> Statements { get; set; } = new List<Statement>();
    public Block(List<Statement> statements)
        => Statements = statements;

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}


public class FunctionCall : Statement
{
    public string FunctionName { get; set; }
    public List<Expression> Arguments { get; set; } = new List<Expression>();

    public FunctionCall(string functionName, List<Expression> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class Property : Statement
{
    public string ObjectName { get; set; }
    public string PropertyName { get; set; }

    public Property(string objectName, string propertyName)
    {
        ObjectName = objectName;
        PropertyName = propertyName;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class ActivationMember : ASTNode
{
    public EffectInstance EffectInstance { get; set; }
    public Selector Selector { get; set; }
    public PostAction PostAction { get; set; }

    public ActivationMember(EffectInstance effectInstance, Selector selector, PostAction postAction)
    {
        EffectInstance = effectInstance;
        Selector = selector;
        PostAction = postAction;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class EffectInstance : ASTNode
{
    public string Name { get; set; }
    public Dictionary<string, object> Identifiers { get; set; } = new Dictionary<string, object>();

    public EffectInstance(string name, Dictionary<string, object> identifiers)
    {
        Name = name;
        Identifiers = identifiers;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class Selector : ASTNode
{
    public string Source { get; set; }
    public bool Single { get; set; }
    public Predicate Predicate { get; set; }

    public Selector(string source, bool single, Predicate predicate)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class Predicate : ASTNode
{
    public string Identifier { get; set; }
    public Expression Expression { get; set; }

    public Predicate(string identifier, Expression expression)
    {
        Identifier = identifier;
        Expression = expression;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }

}

public class PostAction : ASTNode
{
    public string Type { get; set; }
    public Selector Selector { get; set; }

    public PostAction(string type, Selector selector)
    {
        Type = type;
        Selector = selector;
    }

    public override void Accept(IVisitor visitor)
    {
        throw new NotImplementedException();
    }
}
}

