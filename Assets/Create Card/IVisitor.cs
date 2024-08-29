using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassLibrary.SyntaxAnalysis.Visitor
{
       public interface IVisitor
{
    void Visit(Variable variable);
    void Visit(Literal variable);
    void Visit(UnaryExpression unaryExpression);
    void Visit(PostfixUnaryExpression unaryExpression);
    void Visit(BinaryExpression binaryExpression);
    void Visit(AssignExpression assignExpression);
}

}



