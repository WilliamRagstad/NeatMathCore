namespace NeatMathCore.Math.Models
{
    interface IExpression
    {
        public IExpression Evaluate(Variables.VariableCollection variables);
        public string ToStringExpression();
        public string ToString();

        #region Operations

        public static IExpression operator +(IExpression lhs, IExpression rhs)
        {
            if (lhs is Expressions.ConstantExpression && rhs is Expressions.ConstantExpression)
                return new Expressions.ConstantExpression(((Expressions.ConstantExpression)lhs).Value + ((Expressions.ConstantExpression)rhs).Value);
            else
                return new Expressions.AdditionExpression(lhs, rhs);
        }
        public static IExpression operator -(IExpression lhs, IExpression rhs)
        {
            if (lhs is Expressions.ConstantExpression && rhs is Expressions.ConstantExpression)
                return new Expressions.ConstantExpression(((Expressions.ConstantExpression)lhs).Value - ((Expressions.ConstantExpression)rhs).Value);
            else
                return new Expressions.SubtractionExpression(lhs, rhs);
        }
        public static IExpression operator /(IExpression lhs, IExpression rhs)
        {
            if (lhs is Expressions.ConstantExpression && rhs is Expressions.ConstantExpression)
                return new Expressions.ConstantExpression(((Expressions.ConstantExpression)lhs).Value / ((Expressions.ConstantExpression)rhs).Value);
            else
                return new Expressions.DivisionExpression(lhs, rhs);
        }
        public static IExpression operator *(IExpression lhs, IExpression rhs)
        {
            if (lhs is Expressions.ConstantExpression && rhs is Expressions.ConstantExpression)
                return new Expressions.ConstantExpression(((Expressions.ConstantExpression)lhs).Value * ((Expressions.ConstantExpression)rhs).Value);
            else
                return new Expressions.MultiplicationExpression(lhs, rhs);
        }

        #endregion
    }
}
